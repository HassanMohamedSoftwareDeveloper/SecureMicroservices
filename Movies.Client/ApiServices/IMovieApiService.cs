// Ignore Spelling: Api

using Movies.Client.Models;

namespace Movies.Client.ApiServices;

public interface IMovieApiService
{
    Task<IEnumerable<Movie>> GetMoviesAsync();
    Task<Movie> GetMovieAsync(int id);
    Task<Movie> CreateMovieAsync(Movie movie);
    Task<Movie> UpdateMovieAsync(Movie movie);
    Task DeleteMovieAsync(int id);
    Task<UserInfoViewModel> GetUserInfoAsync();
}
