using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebShop13kVizsga.Persistence
{
    public class DataDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Worker> Workers { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DataDbContext(DbContextOptions<DataDbContext> options) : base(options) { }
    }

    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public string Address { get; set; }

        public int Phone { get; set; }
        public string Role { get; set; } = "User";
    }

    public class Worker
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int WorkerId { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        public string Role { get; set; } = "Worker";

        public int Phone { get; set; }
    }

    public class Category
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CategoryId { get; set; } 
        public string CategoryName { get; set; }
        public List<Item> Items { get; set; } = new();
    }

    public class Item
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ItemId { get; set; }

        public int CategoryId { get; set; }

        [Required]
        public string ItemName { get; set; }

        public int Quantity { get; set; }

        public string Description { get; set; }

        public int Price { get; set; }
    }

    public class Cart
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CartId { get; set; }

        [Required]
        [ForeignKey("User")]
        public int UserId { get; set; }

        [Required]
        [ForeignKey("Item")]
        public int ItemId { get; set; }

        public string ItemName { get; set; }

        public int Quantity { get; set; }

        public int Price { get; set; }
    }

    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderId { get; set; }

        [Required]
        [ForeignKey("Cart")]
        public int CartId { get; set; }

        [Required]
        [ForeignKey("User")]
        public int UserId { get; set; }

        public int TotalPrice { get; set; }

        public string TargetAddress { get; set; }

        public int TargetPhone { get; set; }

        public DateTimeOffset Date { get; set; }

        public OrderStatus Status { get; set; } = OrderStatus.ItemQuantityOnHold;

    }

    public enum OrderStatus
    {
        ItemQuantityOnHold,
        DataConfirmed,
        PaymentPending,
        PaymentSuccess,
        OrderConfirmed
    }
}
