using Otus.Teaching.PromoCodeFactory.Core.Domain.PromoCodeManagement;
using Otus.Teaching.PromoCodeFactory.WebHost.Models.ResponseModels;

namespace Otus.Teaching.PromoCodeFactory.WebHost.Extensions
{
    public static class PreferenceExtensions
    {
        public static PreferenceResponse ToResponse(this Preference preference)
            => new()
            {
                Id = preference.Id,
                Name = preference.Name
            };
    }
}
