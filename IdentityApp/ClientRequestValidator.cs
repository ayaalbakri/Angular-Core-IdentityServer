using IdentityServer4.Validation;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IdentityApp
{
    public class PoopRequestValidator : ICustomTokenRequestValidator
    {
        public async Task ValidateAsync(CustomTokenRequestValidationContext context)
        {
            var client = context.Result.ValidatedRequest.Client;

            // we want to add custom claims to our "poop" client
            if (client.ClientName == "Aya Client")
            {
                // do some validation, etc.
                //var result = await DoSomeProcessingAsync(client);
                //

                // }

                // get list of custom claims we want to add
                var claims = new List<Claim>
                        {
            new Claim(ClaimTypes.Role, "Admin")
                             };

                // add it
                claims.ToList().ForEach(u => context.Result.ValidatedRequest.ClientClaims.Add(u));

                // don't want it to be prefixed with "client_" ? we change it here (or from global settings)
                context.Result.ValidatedRequest.Client.ClientClaimsPrefix = "";
            }
        }
    }
}
