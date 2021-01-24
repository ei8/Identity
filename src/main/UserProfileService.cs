using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;
using works.ei8.Identity.Models;

namespace works.ei8.Identity
{
    internal class UserProfileService : IProfileService
    {
        protected UserManager<ApplicationUser> _userManager;

        // https://stackoverflow.com/questions/44761058/how-to-add-custom-claims-to-access-token-in-identityserver4

        public UserProfileService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            //>Processing
            var user = await _userManager.GetUserAsync(context.Subject);
            var claims = await _userManager.GetClaimsAsync(user);

            context.IssuedClaims.AddRange(claims);
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            //>Processing
            var user = await _userManager.GetUserAsync(context.Subject);

            context.IsActive = true; // TODO: (user != null) && user.IsActive;
        }
    }
}