using Microsoft.EntityFrameworkCore;

namespace DFF.Freedom.EntityFrameworkCore
{
    public static class FreedomDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<FreedomDbContext> builder, string connectionString)
        {
            builder.UseSqlServer(connectionString);
        }
    }
}