using Otus.Teaching.PromoCodeFactory.Core.Domain.PromoCodeManagement;
using Otus.Teaching.PromoCodeFactory.WebHost.Models.RequestModels;
using Otus.Teaching.PromoCodeFactory.WebHost.Models.ResponseModels;
using System.Collections.Generic;
using System.Linq;

namespace Otus.Teaching.PromoCodeFactory.WebHost.Extensions
{
    public static class CustomerExtensions
    {
        public static CustomerShortResponse ToShortResponse(this Customer customer)
            => new()
            {
                Id = customer.Id,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Email = customer.Email,
                IsDeleted = customer.IsDeleted
            };

        public static CustomerResponse ToResponse(this Customer customer)
            => new()
            {
                Id = customer.Id,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Email = customer.Email,
                IsDeleted = customer.IsDeleted,
                Preferences = customer.Preferences.Select(x => new PreferenceResponse()
                {
                    Id = x.PreferenceId,
                    Name = x.Preference.Name
                }).ToList()
            };

        public static Customer ToCustomer(this CreateOrEditCustomerRequest request, IEnumerable<Preference> preferences, Customer customer = null)
        {
            customer ??= new();

            customer.Email = request.Email;
            customer.FirstName = request.FirstName;
            customer.LastName = request.LastName;
            customer.IsDeleted = request.IsDeleted;

            if (preferences is null || !preferences.Any())
                return customer;

            customer.Preferences?.Clear();

            customer.Preferences = preferences.Select(x => new CustomerPreference()
            {
                Customer = customer,
                Preference = x
            }).ToList();

            return customer;
        }
    }
}
