// Ignore Spelling: Api

using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System.Security.Claims;

namespace IdentityServer;

public static class Config
{
    public static IEnumerable<Client> Clients => new Client[]
    {
        new Client()
        {
            ClientId="movieClient",
            AllowedGrantTypes=GrantTypes.ClientCredentials,
            ClientSecrets =
            {
                new Secret("secret".Sha256())
            },
            AllowedScopes={ "movieAPI" }
        },

         new Client()
         {
            ClientId="movie_mvc_client",
            ClientName="Movies MVC Web App",
            AllowedGrantTypes=GrantTypes.Hybrid,
            RequirePkce=false,
            AllowRememberConsent=false,
            RedirectUris=new List<string>()
            {
                "https://localhost:7244/signin-oidc"
            },
            PostLogoutRedirectUris=new List<string>()
            {
                "https://localhost:7244/signout-callback-oidc"
            },
            ClientSecrets =
            {
                new Secret("secret".Sha256())
            },
            AllowedScopes=new List<string>
            {
                IdentityServerConstants.StandardScopes.OpenId,
                IdentityServerConstants.StandardScopes.Profile,
                IdentityServerConstants.StandardScopes.Address,
                IdentityServerConstants.StandardScopes.Email,
                "roles",
                "movieAPI",
            }
        },
    };
    public static IEnumerable<ApiScope> ApiScopes => new ApiScope[]
    {
        new ApiScope("movieAPI","Movie API")
    };
    public static IEnumerable<ApiResource> ApiResources => new ApiResource[] { };
    public static IEnumerable<IdentityResource> IdentityResources => new IdentityResource[] {
        new IdentityResources.OpenId(),
        new IdentityResources.Profile(),
        new IdentityResources.Address(),
        new IdentityResources.Email(),
        new IdentityResource("roles","Your role(s)",new List<string>{ "role"})
    };
    public static List<TestUser> TestUsers => new()
    {
        new TestUser
        {
            SubjectId=Guid.NewGuid().ToString(),
            Username="admin",
            Password="admin",
            Claims=new List<Claim>
            {
                new Claim(JwtClaimTypes.GivenName,"admin"),
                new Claim(JwtClaimTypes.FamilyName,"mohamed")
            }
        },
    };
}
