// Ignore Spelling: Api
using Movies.Client.Models;
using Newtonsoft.Json;
using System.Text;

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

        var httpClient = _httpClientFactory.CreateClient("MovieAPIClient");

        var request = new HttpRequestMessage(HttpMethod.Get, "/api/movies/");
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

        var request = new HttpRequestMessage(HttpMethod.Get, $"/api/movies/{id}");
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
        var request = new HttpRequestMessage(HttpMethod.Post, $"/api/movies/")
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
        var request = new HttpRequestMessage(HttpMethod.Put, $"/api/movies/{movie.Id}")
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
        var request = new HttpRequestMessage(HttpMethod.Delete, $"/api/movies/{id}");
        var response = await httpClient
             .SendAsync(request, HttpCompletionOption.ResponseHeadersRead)
             .ConfigureAwait(false);

        response.EnsureSuccessStatusCode();
    }
    #endregion


}
