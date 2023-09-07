// Ignore Spelling: Api
using Movies.Client.Models;
using Newtonsoft.Json;

namespace Movies.Client.ApiServices;

public class MovieApiService : IMovieApiService
{
    #region Fields :
    private readonly IHttpClientFactory _httpClientFactory;
    #endregion

    #region CTORS :
    public MovieApiService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }
    #endregion

    #region Methods :
    public async Task<IEnumerable<Movie>> GetMoviesAsync()
    {

        // 1- Get Token from IS
        // 2- Send request to protected API
        // 3- De-serialize Object 

        var httpClient = _httpClientFactory.CreateClient("MovieAPIClient");

        var request = new HttpRequestMessage(HttpMethod.Get, "/api/movies/");
        var response = await httpClient
             .SendAsync(request, HttpCompletionOption.ResponseHeadersRead)
             .ConfigureAwait(false);

        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();
        List<Movie> movies = JsonConvert.DeserializeObject<List<Movie>>(content);

        return movies;


        //var apiClientCredentials = new ClientCredentialsTokenRequest
        //{
        //    Address = "https://localhost:7077/connect/token",
        //    ClientId = "movieClient",
        //    ClientSecret = "secret",
        //    Scope = "movieAPI"
        //};

        //var client = new HttpClient();

        //var disco = await client.GetDiscoveryDocumentAsync("https://localhost:7077/");
        //if (disco.IsError) return Enumerable.Empty<Movie>();

        //var tokenResponse = await client.RequestClientCredentialsTokenAsync(apiClientCredentials);
        //if (tokenResponse.IsError) return Enumerable.Empty<Movie>();

        //var apiClient = new HttpClient();

        //apiClient.SetBearerToken(tokenResponse.AccessToken);

        //var response = await apiClient.GetAsync("https://localhost:7189/api/movies");
        //response.EnsureSuccessStatusCode();

        //var content = await response.Content.ReadAsStringAsync();
        //List<Movie> movies = JsonConvert.DeserializeObject<List<Movie>>(content);

        //return movies;
    }
    public Task<Movie> GetMovieAsync(int id)
    {
        throw new NotImplementedException();
    }
    public Task<Movie> CreateMovieAsync(Movie movie)
    {
        throw new NotImplementedException();
    }
    public Task<Movie> UpdateMovieAsync(Movie movie)
    {
        throw new NotImplementedException();
    }
    public Task DeleteMovieAsync(int id)
    {
        throw new NotImplementedException();
    }
    #endregion
}
