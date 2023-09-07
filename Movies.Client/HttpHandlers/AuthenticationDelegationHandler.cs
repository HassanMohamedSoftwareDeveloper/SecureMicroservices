using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace Movies.Client.HttpHandlers;

public sealed class AuthenticationDelegationHandler : DelegatingHandler
{
    #region Fields :
    //private readonly IHttpClientFactory _httpClientFactory;
    private readonly IHttpContextAccessor _httpContextAccessor;
    // private readonly ClientCredentialsTokenRequest _tokenRequest;
    #endregion

    #region CTORS :
    public AuthenticationDelegationHandler(/*IHttpClientFactory httpClientFactory,*/ IHttpContextAccessor httpContextAccessor/*, ClientCredentialsTokenRequest tokenRequest*/)
    {
        // _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
        _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        //_tokenRequest = tokenRequest ?? throw new ArgumentNullException(nameof(tokenRequest));
    }
    #endregion

    #region Methods :

    protected override HttpResponseMessage Send(HttpRequestMessage request,
                                        CancellationToken cancellationToken)
    {
        var token = GetTokenAsync().Result;
        request.SetBearerToken(token);

        return base.Send(request, cancellationToken);
    }
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
                                                           CancellationToken cancellationToken)
    {
        var token = await GetTokenAsync();
        request.SetBearerToken(token);
        return await base.SendAsync(request, cancellationToken);
    }
    #endregion

    #region Helpers :
    private async Task<string> GetTokenAsync()
    {
        var token = await _httpContextAccessor.HttpContext.GetTokenAsync(OpenIdConnectParameterNames.AccessToken);
        if (string.IsNullOrWhiteSpace(token)) throw new HttpRequestException("Invalid Token");
        return token;
        //var client = _httpClientFactory.CreateClient("IDPClient");

        //var tokenResponse = await client.RequestClientCredentialsTokenAsync(_tokenRequest, cancellationToken: cancellationToken);
        //if (tokenResponse.IsError) throw new HttpRequestException(tokenResponse.Error);

        //if (string.IsNullOrWhiteSpace(tokenResponse.AccessToken)) throw new HttpRequestException("Invalid Token");

        //return tokenResponse.AccessToken;
    }
    #endregion
}
