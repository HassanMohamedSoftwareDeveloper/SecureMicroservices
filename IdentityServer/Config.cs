// Ignore Spelling: Api

using IdentityModel;
using IdentityServer4;
using IdentityServer4.EntityFramework.Entities;
//using IdentityServer4.Models;
using IdentityServer4.Test;
using System.Security.Claims;
using static IdentityModel.OidcConstants;

namespace IdentityServer;

public static class Config
{
    public static IEnumerable<Client> Clients => new Client[]
    {
        new Client()
        {
            ClientId="movieClient",
            AllowedGrantTypes=new List<ClientGrantType>{
                new ClientGrantType
                {
                    GrantType=GrantTypes.ClientCredentials
                }

            },
            ClientSecrets =new List<ClientSecret>
            {
                new ClientSecret{
                    Value="secret".ToSha256()
                }
            },
            AllowedScopes= new List<ClientScope>{
                new ClientScope
                {
                    Scope="movieAPI"
                }
                 }
        },

         new Client()
         {
            ClientId="movie_mvc_client",
            ClientName="Movies MVC Web App",
            AllowedGrantTypes=new List<ClientGrantType>{
                new ClientGrantType
                {
                    GrantType=  GrantTypes.ClientCredentials
                },
                new ClientGrantType
                {
                    GrantType=GrantTypes.AuthorizationCode
                }

            },
            RequirePkce=false,
            AllowRememberConsent=false,
            RedirectUris=new List<ClientRedirectUri>
            {new ClientRedirectUri
            {
                RedirectUri= "https://localhost:7244/signin-oidc"
            }

            },
            PostLogoutRedirectUris=new  List<ClientPostLogoutRedirectUri>
            {new ClientPostLogoutRedirectUri
            {
                PostLogoutRedirectUri= "https://localhost:7244/signout-callback-oidc"
            }

            },
             ClientSecrets =new List<ClientSecret>
            {
                new ClientSecret{
                    Value="secret".ToSha256()
                }
            },
            AllowedScopes=new List<ClientScope>
            {
                new ClientScope{Scope= IdentityServerConstants.StandardScopes.OpenId},
                new ClientScope{Scope= IdentityServerConstants.StandardScopes.Profile},
                new ClientScope{Scope= IdentityServerConstants.StandardScopes.Address},
                new ClientScope{Scope= IdentityServerConstants.StandardScopes.Email},
                new ClientScope{Scope= "roles"},
                new ClientScope{Scope= "movieAPI"},

            },
        },
    };
    public static IEnumerable<ApiScope> ApiScopes => new ApiScope[]
    {
        new ApiScope{
            Name="movieAPI",
            DisplayName= "Movie API"
        }
    };
    public static IEnumerable<ApiResource> ApiResources => Array.Empty<ApiResource>();
    public static IEnumerable<IdentityResource> IdentityResources => new IdentityResource[] {
        new IdentityResource
        {
            Name = IdentityServerConstants.StandardScopes.OpenId,
                DisplayName = "Your user identifier",
                Required = true,
                UserClaims=new List<IdentityResourceClaim>
                {
                    new IdentityResourceClaim
                    {
                        Type= JwtClaimTypes.Subject
                    }
                }
        },
          new IdentityResource
        {
            Name = IdentityServerConstants.StandardScopes.Profile,
                DisplayName = "User profile",
                Description = "Your user profile information (first name, last name, etc.)",
                Emphasize = true,
                UserClaims=new List<IdentityResourceClaim>
                {
                    new IdentityResourceClaim{ Type=JwtClaimTypes.Name },
                    new IdentityResourceClaim{ Type=JwtClaimTypes.FamilyName },
                    new IdentityResourceClaim{ Type=JwtClaimTypes.GivenName },
                    new IdentityResourceClaim{ Type=JwtClaimTypes.MiddleName },
                    new IdentityResourceClaim{ Type=JwtClaimTypes.NickName },
                    new IdentityResourceClaim{ Type=JwtClaimTypes.PreferredUserName},
                    new IdentityResourceClaim{ Type=JwtClaimTypes.Profile},
                    new IdentityResourceClaim{ Type=JwtClaimTypes.Picture},
                    new IdentityResourceClaim{ Type=JwtClaimTypes.WebSite},
                    new IdentityResourceClaim{ Type=JwtClaimTypes.Gender},
                    new IdentityResourceClaim{ Type=JwtClaimTypes.BirthDate},
                    new IdentityResourceClaim{ Type=JwtClaimTypes.ZoneInfo},
                    new IdentityResourceClaim{ Type=JwtClaimTypes.Locale},
                    new IdentityResourceClaim{ Type=JwtClaimTypes.UpdatedAt},
                }
        },
                 new IdentityResource
        {
           Name = IdentityServerConstants.StandardScopes.Address,
                DisplayName = "Your postal address",
                Emphasize = true,
                UserClaims=new List<IdentityResourceClaim>
                {
                    new IdentityResourceClaim{ Type=JwtClaimTypes.Address },
                }
        },
                             new IdentityResource
        {
           Name = IdentityServerConstants.StandardScopes.Email,
                DisplayName = "Your email address",
                Emphasize = true,
                UserClaims=new List<IdentityResourceClaim>
                {
                    new IdentityResourceClaim{ Type=JwtClaimTypes.Email },
                    new IdentityResourceClaim{ Type=JwtClaimTypes.EmailVerified },
                }
        },
         new IdentityResource
        {
           Name = "roles",
                DisplayName = "Your role(s)",
                Emphasize = true,
                UserClaims=new List<IdentityResourceClaim>
                {
                    new IdentityResourceClaim{ Type=JwtClaimTypes.Role },
                }
        },
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
