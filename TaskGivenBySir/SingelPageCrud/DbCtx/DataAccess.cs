using CrudUsingCore.Models;
using Microsoft.EntityFrameworkCore;

namespace CrudUsingCore.DbCtx
{
    public class DataAccess : DbContext
    {
        public DataAccess(DbContextOptions<DataAccess> options) : base(options)
        {
            
        }
        
        public DbSet<Student> Students { get; set; }
    }
}
