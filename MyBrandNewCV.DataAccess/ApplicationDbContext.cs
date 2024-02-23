using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyBrandNewCv.Common.Models;
using MyBrandNewCV.DataAccess.Models;

namespace MyBrandNewCV.DataAccess
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {


        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
         
            
                //optionsBuilder.UseSqlServer(" Data Source = (localdb)\\MSSQLLocalDB;Initial Catalog = RobinsCV; Integrated Security = True; Connect Timeout = 30; Encrypt=False;Trust Server Certificate=False;Application Intent = ReadWrite; Multi Subnet Failover=False\r\n");
                optionsBuilder.UseSqlServer("DefaultConnection");
        }
       
        public DbSet<Skill> Skills { get; set; }
        public DbSet<Project> Projects { get; set; }


    }
    }

