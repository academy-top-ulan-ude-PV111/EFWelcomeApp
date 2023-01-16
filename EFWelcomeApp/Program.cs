using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFWelcomeApp
{
    public class Country
    {
        public int Id { get; set; }
        public string Title { set; get; }
    }
    public class Company
    {
        public int Id { get; set; }
        public string Title { set; get; }
        public Country Country { set; get; } 
    }
    public class Employe
    {
        public int Id { get; set; }

        //[Column("UserName")]
        public string? Name { get; set; }
        public int? Age { set; get; }
        public Company? Company { set; get; }

    }

    public class Product
    {
        public int Id { get; set; }
        public string Title { set; get; }
    }

    

    public class UserContext : DbContext
    {
        public DbSet<Employe> Employes { get; set; }
        public DbSet<Product> Products { get; set; }
        public UserContext()
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=ShopDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            using(UserContext context = new())
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                //User user = new User() { Name = "Bob", Age = 34 };
                //context.Users.Add(user);
                //user = new User() { Name = "Joe", Age = 21 };
                //context.Users.Add(user);

                context.SaveChanges();
            }

            //using(UserContext context = new())
            //{
            //    List<User> users = context.Users.ToList();
            //    foreach(var user in users)
            //        Console.WriteLine($"{user.Id} : {user.Name} ({user.Age})");
            //}
        }
    }
}