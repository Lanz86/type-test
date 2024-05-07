using Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace Api.Infrastructure.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<Question> Questions => Set<Question>();
}