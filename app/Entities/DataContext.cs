using Microsoft.EntityFrameworkCore;
using System.Configuration;

namespace Entities
{

    public class DataContext: DbContext
    {

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
        }
    }
}
