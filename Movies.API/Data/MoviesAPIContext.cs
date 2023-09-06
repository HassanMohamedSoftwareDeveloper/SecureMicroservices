using Microsoft.EntityFrameworkCore;

namespace Movies.API.Data;

public class MoviesAPIContext : DbContext
{
    public MoviesAPIContext(DbContextOptions<MoviesAPIContext> options)
        : base(options)
    {
    }

    public DbSet<Model.Movie> Movie { get; set; } = default!;
}
