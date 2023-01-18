using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
//using Microsoft.IdentityModel.Protocols;
//using System.ComponentModel;
//using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;

namespace EFWelcomeApp
{
    //public class Country
    //{
    //    public int Id { get; set; }
    //    public string Title { set; get; }
    //}

    //[Table("Companies")]
    public class Company
    {
        public int Id { get; set; }
        public string Title { set; get; }

        public Company(string title = "Noname")
        {
            Title = title;
        }
        public ICollection<Employe> Employes { get; set; } = new List<Employe>();

        //public Country Country { set; get; }
    }
    //[PrimaryKey("PK_Passport", nameof(NumberPassport), nameof(SeriesPassport))]
    
    //[Index("Company", "Name", IsUnique = true, Name = "I_Company_Name")]
    public class Employe
    {
        public int Id { get; set; }

        //[Column("Id")]
        //[Key]
        [Required]
        public string Passport { get; set; }

        //[Column("EmployeName")]
        //[Required]
        public string? Name { get; set; }
        public int? Age { set; get; }
        public int CompanyId { set; get; }
        public Company? Company { set; get; }

        public Employe(string? name, int? age)
        {
            Name = name;
            Age = age;
        }

        public Employe() : this("Anonim", 0){ }
    }

    public class Product
    {
        int id;
        string name;
        decimal price;
        private string articul;

        public int Id => id;
        public string Title => name;
        public decimal Price => price;
        public Product(string title, string articul, decimal price)
        {
            this.name = title;
            this.price = price;
            this.articul = articul;
        }

        public override string ToString()
        {
            return $"{name} [{articul}] : {price}";
        }
    }

    

    public class UserContext : DbContext
    {
        public DbSet<Employe> Employes { get; set; }
        public DbSet<Product> Products { get; set; }
        public UserContext()
        {
            //Database.EnsureDeleted();
            //Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=ShopDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Product>().Property("Id").HasField("id");
            modelBuilder.Entity<Product>().Property("Title").HasField("name");
            modelBuilder.Entity<Product>().Property("Price").HasField("price");
            modelBuilder.Entity<Product>().Property("articul");

            modelBuilder.Entity<Employe>()
                        .Property(e => e.Name)
                        .HasColumnName("EmployeName");

            modelBuilder.Entity<Company>()
                        .ToTable("Companies");

            modelBuilder.Entity<Employe>()
                        .Property(e => e.Name)
                        .IsRequired();

            //modelBuilder.Entity<Employe>()
            //            .HasKey(e => e.Passport)
            //            .HasName("PK_My_Employes");
            //modelBuilder.Entity<Employe>()
            //            .HasKey(e => new { e.NumberPassport, e.SeriesPassport })
            //            .HasName("PK_Passport");

            //modelBuilder.Entity<Employe>()
            //            .HasAlternateKey(e => e.Passport);

            modelBuilder.Entity<Employe>()
                        //.HasIndex(e => e.Name)
                        .HasIndex(e => new { e.CompanyId, e.Name })
                        .IsUnique()
                        .HasDatabaseName("I_Company_Name");
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            
            using(UserContext context = new())
            {
                
                //context.Database.EnsureDeleted();
                //context.Database.EnsureCreated();
                
                
                Company comp1 = new() { Title="Yandex" };
                Company comp2 = new() { Title = "Mail Group" };

                /*
                Employe employe1 = new() { Name = "Max", Age = 24, Company = comp1, Passport="222" };
                context.Employes.Add(employe1);
                Employe employe2 = new() { Name = "Tom", Age = 22, Company = comp2, Passport = "456" };
                context.Employes.Add(employe2);
                
                
                Employe employe3 = new() { Name = "Tim", Age = 41, Company = comp1, Passport = "789" };
                context.Employes.Add(employe3);
                Employe employe4 = new() { Name = "Leo", Age = 19, Company = comp2, Passport = "111" };
                context.Employes.Add(employe4);
                */

                Product product = new("Phone", "P1523hjO", 15000.0m);
                context.Products.Add(product);


                context.SaveChanges();
            }
            

            using (UserContext context = new())
            {
                //List<Employe> users = context.Employes.ToList();
                //foreach (var user in users)
                //    Console.WriteLine($"{user.Id} : {user.Name} ({user.Age})");

                //var emploeyes1 = context.Employes.Where(e => e.Company.Id == 1);
                //foreach(var empl in emploeyes1)
                //    Console.WriteLine($"{empl.Id} : {empl.Name} ({empl.Age})");

                
                foreach(var product in context.Products)
                    Console.WriteLine(product);
            }
        }
    }
}