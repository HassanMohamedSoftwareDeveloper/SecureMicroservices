using IdentityServer;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServerHost.Quickstart.UI;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Reflection;

var migrationsAssembly = typeof(Program).GetTypeInfo().Assembly.GetName().Name;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();

builder.Services
    .AddIdentityServer()
    .AddConfigurationStore(options =>
    {
        options.ConfigureDbContext = b => b.UseSqlServer(builder.Configuration.GetConnectionString("ISDBContext"),
            sql => sql.MigrationsAssembly(migrationsAssembly));
    })
    .AddOperationalStore(options =>
     {
         options.ConfigureDbContext = b => b.UseSqlServer(builder.Configuration.GetConnectionString("ISDBContext"),
             sql => sql.MigrationsAssembly(migrationsAssembly));
     })
    .AddTestUsers(TestUsers.Users)
    .AddDeveloperSigningCredential();
//.AddInMemoryClients(Config.Clients)
//.AddInMemoryIdentityResources(Config.IdentityResources)
//.AddInMemoryApiResources(Config.ApiResources)
//.AddInMemoryApiScopes(Config.ApiScopes)

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
app.UseStaticFiles();
app.UseRouting();
app.UseIdentityServer();
app.UseAuthentication();
app.UseAuthorization();

app.MapDefaultControllerRoute();
InitializeDatabase(app);
app.Run();

partial class Program
{

    private static void InitializeDatabase(IApplicationBuilder app)
    {
        try
        {
            using var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope();

            serviceScope.ServiceProvider.GetRequiredService<PersistedGrantDbContext>().Database.Migrate();

            var context = serviceScope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();
            context.Database.Migrate();
            if (!context.Clients.Any())
            {
                foreach (var client in Config.Clients)
                {
                    context.Clients.Add(client);
                }
                context.SaveChanges();
            }

            if (!context.IdentityResources.Any())
            {
                foreach (var resource in Config.IdentityResources)
                {
                    context.IdentityResources.Add(resource);
                }
                context.SaveChanges();
            }

            if (!context.ApiScopes.Any())
            {
                foreach (var resource in Config.ApiScopes)
                {
                    context.ApiScopes.Add(resource);
                }
                context.SaveChanges();
            }
        }
        catch (Exception ex)
        {
            Debug.Fail(ex.Message);
        }

    }
}

