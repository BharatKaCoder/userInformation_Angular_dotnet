using Microsoft.EntityFrameworkCore;
using userInformation_Angular_dotnet.Models;

namespace userInformation_Angular_dotnet
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Registration> UserListTable { get; set; }

    }
}
