using LoginReg.Models;
using Microsoft.EntityFrameworkCore;

namespace LoginReg
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions options) : base(options){}
        public DbSet<MainUser> Users {get; set;}
    }
}