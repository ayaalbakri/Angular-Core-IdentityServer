using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Security.Claims;

namespace IdentityApp
{
    public class Config
    {
        // scopes define the resources in your system
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                rolesResource,
                new IdentityResource(ClaimTypes.Role, new[] { "Admin" })

            };
        }
        private static IdentityResource rolesResource = new IdentityResource
        {
            Name = "roles",
            DisplayName = "Your Roles",
            Description = "Allow the service access to your user roles.",
            UserClaims = new[] { JwtClaimTypes.Role, ClaimTypes.Role },
            ShowInDiscoveryDocument = true,
            Required = true,
            Emphasize = true
        };


        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource("apiApp", "My API",new []{ ClaimTypes.Role , JwtClaimTypes.Role}),
                 //new ApiResource("Role", "Admin"),
                 //new  ApiResource("apiApp", "My API",new []{ ClaimTypes.Role}),
                 //new ApiResource("Rolesz", "My Roles", new[] { ClaimTypes.Role , JwtClaimTypes.Role})
            };
        }

        // clients want to access resources (aka scopes)
        public static IEnumerable<Client> GetClients(IConfiguration configuration)
        {
            // client credentials client
            return new List<Client>
            {
                new Client
                {
                    ClientId = "clientApp",

                    // no interactive user, use the clientid/secret for authentication
                    AllowedGrantTypes = GrantTypes.ClientCredentials,

                    // secret for authentication
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },

                    // scopes that client has access to
                    AllowedScopes = { "apiApp", "roles" }
                },

                // OpenID Connect implicit flow client (MVC)
                new Client
                {
                    ClientId = "mvc",
                    ClientName = "MVC Client",
                    AllowedGrantTypes = GrantTypes.Implicit,

                    RequireConsent = true,

                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },
                    AllowAccessTokensViaBrowser = true,
                   // AllowedCorsOrigins = true,
                    AlwaysSendClientClaims = true,
                    AlwaysIncludeUserClaimsInIdToken = true,
                    RedirectUris = { $"{configuration["ClientAddress"]}/signin-oidc" },
                    PostLogoutRedirectUris = { $"{configuration["ClientAddress"]}/signout-callback-oidc" },

                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "apiApp",
                         "roles",

                    },
                    AllowOfflineAccess = true
                },


                new Client
                {
                    ClientId = "Aya",
                    ClientName = "Aya Client",
                    AllowedGrantTypes = GrantTypes.HybridAndClientCredentials,

                    RequireConsent = true,

                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },
                    AllowAccessTokensViaBrowser = true,
                   // AllowedCorsOrigins = true,
                    AlwaysSendClientClaims = true,
                    AlwaysIncludeUserClaimsInIdToken = true,
                    RedirectUris = { $"{configuration["ClientAddress"]}/signin-oidc" },
                    PostLogoutRedirectUris = { $"{configuration["ClientAddress"]}/signout-callback-oidc" },

                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "apiApp",
                         "roles",

                    },
                    AllowOfflineAccess = true
                },
                // OpenID Connect implicit flow client (Angular)
                new Client
                {
                    ClientId = "ng",
                    ClientName = "Angular Client",
                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowAccessTokensViaBrowser = true,
                    RequireConsent = true,
                    //AllowAccessTokensViaBrowser = true,
                   // AllowedCorsOrigins = true,
                    AlwaysSendClientClaims = true,
                    AlwaysIncludeUserClaimsInIdToken = true,
                    RedirectUris = { $"{configuration["ClientAddress"]}/" },
                    PostLogoutRedirectUris = { $"{configuration["ClientAddress"]}/home" },
                    AllowedCorsOrigins = { configuration["ClientAddress"] },

                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "apiApp",
                        "roles"

                    },

                }

            };
        }
    }
}