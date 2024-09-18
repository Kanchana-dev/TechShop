//using Microsoft.EntityFrameworkCore;

//namespace Shopping_App.Data
//{
//	public class ProductDataContext: DbContext
//	{
//		protected readonly IConfiguration Configuration;

//		public ProductDataContext(IConfiguration configuration)
//		{
//			Configuration = configuration;
//		}

//		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//		{
//			optionsBuilder.UseSqlite(Configuration.GetConnectionString("ProductDB"));
//		}

//		public DbSet<Product> Products { get; set; }

//		protected override void OnModelCreating(ModelBuilder modelBuilder)
//		{
//			modelBuilder.Entity<Product>().ToTable("Product");

//			modelBuilder.Entity<Product>()
//				.HasData(
//					new Product { Id = 1, Name = "Laptop" },
//					new Product { Id = 2, Name = "Desktop" }
//					);

//		}
//	}
//}
