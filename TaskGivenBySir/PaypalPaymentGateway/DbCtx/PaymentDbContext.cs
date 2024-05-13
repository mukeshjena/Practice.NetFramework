using Microsoft.EntityFrameworkCore;
using PayPalGateway.Models;

namespace PayPalGateway.DbCtx
{
    public class PaymentDbContext : DbContext 
    {
        public PaymentDbContext(DbContextOptions<PaymentDbContext> options) : base(options)
        {
            
        }

        public DbSet<PaymentDetails> PaymentDetails { get; set; }
    }
}
