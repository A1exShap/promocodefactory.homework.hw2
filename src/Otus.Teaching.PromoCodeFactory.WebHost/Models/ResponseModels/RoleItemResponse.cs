using System;

namespace Otus.Teaching.PromoCodeFactory.WebHost.Models.ResponseModels
{
    public class RoleItemResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }
    }
}