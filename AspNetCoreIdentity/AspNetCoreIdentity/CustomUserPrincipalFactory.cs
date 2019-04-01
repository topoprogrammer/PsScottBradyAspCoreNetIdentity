using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCoreIdentity
{
    public class CustomUserPrincipalFactory
        : UserClaimsPrincipalFactory<CustomUser>

    {
        public CustomUserPrincipalFactory(UserManager<CustomUser> userManager, IOptions<IdentityOptions> optionsAccessor) :
            base(userManager, optionsAccessor)
        {
        }

        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(CustomUser user)
        {
            var identity = await base.GenerateClaimsAsync(user);
            identity.AddClaim(new Claim("locale", user.Locale));
            return identity;
        }
    }
}
