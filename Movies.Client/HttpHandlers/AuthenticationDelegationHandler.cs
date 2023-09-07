using IdentityModel.Client;

namespace Movies.Client.HttpHandlers;

public sealed class AuthenticationDelegationHandler : DelegatingHandler
{
    #region Fields :
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ClientCredentialsTokenRequest _tokenRequest;
    #endregion

    #region CTORS :
    public AuthenticationDelegationHandler(IHttpClientFactory httpClientFactory, ClientCredentialsTokenRequest tokenRequest)
    {
        _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
        _tokenRequest = tokenRequest ?? throw new ArgumentNullException(nameof(tokenRequest));
    }
    #endregion

    #region Methods :

    protected override HttpResponseMessage Send(HttpRequestMessage request,
                                        CancellationToken cancellationToken)
    {
        var token = GetTokenAsync(cancellationToken).Result;
        request.SetBearerToken(token);

        return base.Send(request, cancellationToken);
    }
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
                                                           CancellationToken cancellationToken)
    {
        var token = await GetTokenAsync(cancellationToken);
        request.SetBearerToken(token);
        return await base.SendAsync(request, cancellationToken);
    }
    #endregion

    #region Helpers :
    private async Task<string> GetTokenAsync(CancellationToken cancellationToken)
    {
        var client = _httpClientFactory.CreateClient("IDPClient");

        var tokenResponse = await client.RequestClientCredentialsTokenAsync(_tokenRequest, cancellationToken: cancellationToken);
        if (tokenResponse.IsError) throw new HttpRequestException(tokenResponse.Error);

        if (string.IsNullOrWhiteSpace(tokenResponse.AccessToken)) throw new HttpRequestException("Invalid Token");

        return tokenResponse.AccessToken;
    }
    #endregion
}
