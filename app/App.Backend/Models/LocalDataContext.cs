
namespace App.Backend.Models
{
    using Lands.Domain;

    public class LocalDataContext: DataContext
    {
        public System.Data.Entity.DbSet<User> Users { get; set; }

        public System.Data.Entity.DbSet<Lands.Domain.UserType> UserTypes { get; set; }
    }
}