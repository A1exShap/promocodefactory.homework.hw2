using System;

namespace Otus.Teaching.PromoCodeFactory.WebHost.Models.ResponseModels
{
    public class EmployeeShortResponse
    {
        public Guid Id { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }
    }
}