﻿using System;

namespace Otus.Teaching.PromoCodeFactory.WebHost.Models.ResponseModels
{
    public class CustomerShortResponse
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public bool IsDeleted { get; set; }
    }
}