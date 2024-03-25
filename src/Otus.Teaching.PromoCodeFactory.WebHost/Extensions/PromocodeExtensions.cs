using Otus.Teaching.PromoCodeFactory.Core.Domain.PromoCodeManagement;
using Otus.Teaching.PromoCodeFactory.WebHost.Models.RequestModels;
using Otus.Teaching.PromoCodeFactory.WebHost.Models.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Otus.Teaching.PromoCodeFactory.WebHost.Extensions
{
    public static class PromocodeExtensions
    {
        public static PromoCodeShortResponse ToShortResponse(this PromoCode promoCode)
            => new()
            {
                Id = promoCode.Id,
                Code = promoCode.Code,
                BeginDate = promoCode.BeginDate.ToString("yyyy-MM-dd"),
                EndDate = promoCode.EndDate.ToString("yyyy-MM-dd"),
                PartnerName = promoCode.PartnerName,
                ServiceInfo = promoCode.ServiceInfo
            };

        public static PromoCode ToPromoCode(this GivePromoCodeRequest request, Preference preference, IEnumerable<Customer> customers)
        {
            var promocode = new PromoCode()
            {
                Id = Guid.NewGuid(),
                PartnerName = request.PartnerName,
                Code = request.PromoCode,
                ServiceInfo = request.ServiceInfo,
                BeginDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(30),
                Preference = preference,
                PreferenceId = preference.Id
            };

            promocode.Customers = customers.Select(c => new PromoCodeCustomer(promocode, c)).ToList();

            return promocode;
        }
    }
}
