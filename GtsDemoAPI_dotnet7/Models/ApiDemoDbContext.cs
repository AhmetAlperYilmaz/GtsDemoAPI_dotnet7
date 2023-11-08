using Microsoft.EntityFrameworkCore;

namespace GtsDemoAPI_dotnet7.Models
{
    public class ApiDemoDbContext: DbContext
    {
        public ApiDemoDbContext (DbContextOptions<ApiDemoDbContext> options) : base(options)
        { 
            // Database Connection Context
        }

        public DbSet<Users> users { get; set; }
        public DbSet<Members> members { get; set; }
    }
}
