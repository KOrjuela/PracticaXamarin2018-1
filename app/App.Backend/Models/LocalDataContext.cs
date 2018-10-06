using Lands.Domain;

namespace App.Backend.Models
{

    public class LocalDataContext: DataContext
    {
        public System.Data.Entity.DbSet<User> Users { get; set; }
    }
}