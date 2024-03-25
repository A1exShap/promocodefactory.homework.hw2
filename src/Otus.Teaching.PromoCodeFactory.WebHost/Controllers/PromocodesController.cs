using Microsoft.AspNetCore.Mvc;
using Otus.Teaching.PromoCodeFactory.Core.Abstractions.Repositories;
using Otus.Teaching.PromoCodeFactory.Core.Domain.PromoCodeManagement;
using Otus.Teaching.PromoCodeFactory.WebHost.Extensions;
using Otus.Teaching.PromoCodeFactory.WebHost.Models.RequestModels;
using Otus.Teaching.PromoCodeFactory.WebHost.Models.ResponseModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Otus.Teaching.PromoCodeFactory.WebHost.Controllers
{
    /// <summary>
    /// Промокоды
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]
    public class PromocodesController
        : ControllerBase
    {
        private readonly IRepository<PromoCode> _promocodesRepository;
        private readonly IRepository<Preference> _preferencesRepository;
        private readonly IRepository<Customer> _customersRepository;

        public PromocodesController(IRepository<PromoCode> promocodesRepository
                                  , IRepository<Preference> preferencesRepository
                                  , IRepository<Customer> customersRepository)
        {
            _promocodesRepository = promocodesRepository;
            _preferencesRepository = preferencesRepository;
            _customersRepository = customersRepository;
        }

        /// <summary>
        /// Получить все промокоды
        /// </summary>
        /// <returns>Список промокодов</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PromoCodeShortResponse>>> GetPromocodesAsync()
        {
            var promoCodes = (await _promocodesRepository.GetAllAsync()).Select(x => x.ToShortResponse());

            return Ok(promoCodes);
        }
        
        /// <summary>
        /// Создать промокод и выдать его клиентам с указанным предпочтением
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> GivePromoCodesToCustomersWithPreferenceAsync(GivePromoCodeRequest request)
        {
            var preference = await _preferencesRepository.FirstOrDefaultAsync(x => x.Name.Equals(request.Preference));

            if (preference is null)
                return NotFound();

            var customers = await _customersRepository
                .GetByPredicate(d => d.Preferences.Any(x =>
                    x.Preference.Id == preference.Id));

            var promoCode = request.ToPromoCode(preference, customers);

            await _promocodesRepository.CreateAsync(promoCode);

            return Ok();
        }
    }
}