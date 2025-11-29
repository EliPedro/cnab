using Microsoft.EntityFrameworkCore;

namespace EP.AspireApp.Cnab.AppHost.Database;

public class ApplicationDbContext(DbContextOptions options) : DbContext(options)
{
}

