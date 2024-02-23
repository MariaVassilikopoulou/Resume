using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MyBrandNewCV.Contexts
{
    public class UserModelContext : IdentityDbContext
    {
        public UserModelContext(DbContextOptions<UserModelContext> options) : base(options) { }

    }
}
