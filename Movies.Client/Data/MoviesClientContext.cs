using Microsoft.EntityFrameworkCore;

namespace Movies.Client.Data;

public class MoviesClientContext : DbContext
{
    public MoviesClientContext(DbContextOptions<MoviesClientContext> options)
        : base(options)
    {
    }

    public DbSet<Models.Movie> Movie { get; set; } = default!;
}
