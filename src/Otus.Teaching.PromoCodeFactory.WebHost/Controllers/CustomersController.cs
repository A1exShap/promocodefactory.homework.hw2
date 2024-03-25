using Microsoft.AspNetCore.Mvc;
using Otus.Teaching.PromoCodeFactory.Core.Abstractions.Repositories;
using Otus.Teaching.PromoCodeFactory.Core.Domain.PromoCodeManagement;
using Otus.Teaching.PromoCodeFactory.WebHost.Extensions;
using Otus.Teaching.PromoCodeFactory.WebHost.Models.RequestModels;
using Otus.Teaching.PromoCodeFactory.WebHost.Models.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Otus.Teaching.PromoCodeFactory.WebHost.Controllers
{
    /// <summary>
    /// Клиенты
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CustomersController
        : ControllerBase
    {
        private readonly IRepository<Customer> _customerRepository;
        private readonly IRepository<Preference> _preferenceRepository;

        public CustomersController(IRepository<Customer> customerRepository, IRepository<Preference> preferenceRepository)
        {
            _customerRepository = customerRepository;
            _preferenceRepository = preferenceRepository;
        }

        /// <summary>
        /// Получение всех клиентов
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerShortResponse>>> GetCustomersAsync()
        {
            var result = (await _customerRepository.GetAllAsync()).Select(x => x.ToShortResponse()).ToList();

            return Ok(result);
        }
        
        /// <summary>
        /// Получение клиента по идентификатору
        /// </summary>
        /// <param name="id">ИД клиента</param>
        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerResponse>> GetCustomerAsync(Guid id)
        {
            var result = await _customerRepository.GetByIdAsync(id);

            return Ok(result.ToResponse());
        }
        
        /// <summary>
        /// Создание клиента
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> CreateCustomerAsync(CreateOrEditCustomerRequest request)
        {
            var preferences = await GetPreferencesAsync(request.PreferenceIds);

            var customer = request.ToCustomer(preferences);

            await _customerRepository.CreateAsync(customer);

            return Ok();
        }

        /// <summary>
        /// Редактирование клиента
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> EditCustomersAsync(Guid id, CreateOrEditCustomerRequest request)
        {
            var customer = await _customerRepository.GetByIdAsync(id);

            if (customer is null)
                return NotFound();

            var preferences = await GetPreferencesAsync(request.PreferenceIds);

            request.ToCustomer(preferences, customer);

            await _customerRepository.UpdateAsync(customer);

            return Ok();
        }

        /// <summary>
        /// Удаление клиента
        /// </summary>
        [HttpDelete]
        public async Task<IActionResult> DeleteCustomer(Guid id)
        {
            var customer = await _customerRepository.GetByIdAsync(id);

            if (customer == null)
                return NotFound();

            await _customerRepository.DeleteAsync(customer);

            return Ok();
        }

        private Task<IEnumerable<Preference>> GetPreferencesAsync(IEnumerable<Guid> ids)
            => ids != null && ids.Any()
                ? _preferenceRepository.GetByIdsAsync(ids.ToArray())
                : Task.FromResult(Enumerable.Empty<Preference>());
    }
}