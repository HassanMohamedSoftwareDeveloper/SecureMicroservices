// Ignore Spelling: Api
using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Movies.Client.Models;
using Newtonsoft.Json;
using System.Text;

namespace Movies.Client.ApiServices;

public class MovieApiService : IMovieApiService
{
    #region Fields :
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IHttpContextAccessor _httpContextAccessor;
    #endregion

    #region CTORS :
    public MovieApiService(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor)
    {
        _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
        _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
    }
    #endregion

    #region Methods :
    public async Task<IEnumerable<Movie>> GetMoviesAsync()
    {

        var httpClient = _httpClientFactory.CreateClient("MovieAPIClient");

        var request = new HttpRequestMessage(HttpMethod.Get, "/movies");
        var response = await httpClient
             .SendAsync(request, HttpCompletionOption.ResponseHeadersRead)
             .ConfigureAwait(false);

        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();
        var movies = JsonConvert.DeserializeObject<List<Movie>>(content);

        return movies;
    }
    public async Task<Movie> GetMovieAsync(int id)
    {
        var httpClient = _httpClientFactory.CreateClient("MovieAPIClient");

        var request = new HttpRequestMessage(HttpMethod.Get, $"/movies/{id}");
        var response = await httpClient
             .SendAsync(request, HttpCompletionOption.ResponseHeadersRead)
             .ConfigureAwait(false);

        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();
        var movie = JsonConvert.DeserializeObject<Movie>(content);

        return movie;
    }
    public async Task<Movie> CreateMovieAsync(Movie movie)
    {
        var httpClient = _httpClientFactory.CreateClient("MovieAPIClient");
        var request = new HttpRequestMessage(HttpMethod.Post, $"/movies")
        {
            Content = new StringContent(JsonConvert.SerializeObject(movie), Encoding.UTF8, "application/json")
        };
        var response = await httpClient
             .SendAsync(request, HttpCompletionOption.ResponseHeadersRead)
             .ConfigureAwait(false);

        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();
        var movieRes = JsonConvert.DeserializeObject<Movie>(content);

        return movieRes;
    }
    public async Task<Movie> UpdateMovieAsync(Movie movie)
    {
        var httpClient = _httpClientFactory.CreateClient("MovieAPIClient");
        var request = new HttpRequestMessage(HttpMethod.Put, $"/movies/{movie.Id}")
        {
            Content = new StringContent(JsonConvert.SerializeObject(movie), Encoding.UTF8, "application/json")
        };
        var response = await httpClient
             .SendAsync(request, HttpCompletionOption.ResponseHeadersRead)
             .ConfigureAwait(false);

        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();
        var movieRes = JsonConvert.DeserializeObject<Movie>(content);

        return movieRes;
    }
    public async Task DeleteMovieAsync(int id)
    {
        var httpClient = _httpClientFactory.CreateClient("MovieAPIClient");
        var request = new HttpRequestMessage(HttpMethod.Delete, $"/movies/{id}");
        var response = await httpClient
             .SendAsync(request, HttpCompletionOption.ResponseHeadersRead)
             .ConfigureAwait(false);

        response.EnsureSuccessStatusCode();
    }

    public async Task<UserInfoViewModel> GetUserInfoAsync()
    {
        var idpClient = _httpClientFactory.CreateClient("IDPClient");
        var metaDataResponse = await idpClient.GetDiscoveryDocumentAsync();

        if (metaDataResponse.IsError)
            throw new HttpRequestException("Something went wrong while requesting the access token");


        var token = await _httpContextAccessor.HttpContext.GetTokenAsync(OpenIdConnectParameterNames.AccessToken);
        if (string.IsNullOrWhiteSpace(token))
            throw new ArgumentException("Invalid access token");

        var userInfoResponse = await idpClient.GetUserInfoAsync(new UserInfoRequest
        {
            Address = metaDataResponse.UserInfoEndpoint,
            Token = token,
        });

        if (userInfoResponse.IsError)
            throw new HttpRequestException("Something went wrong while getting user info");

        var userInfoDictionary = new Dictionary<string, string>();
        foreach (var claim in userInfoResponse.Claims)
        {
            userInfoDictionary.Add(claim.Type, claim.Value);
        }
        return new UserInfoViewModel(userInfoDictionary);
    }
    #endregion


}
