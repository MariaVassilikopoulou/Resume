using Microsoft.AspNetCore.Identity;
using MyBrandNewCv.Common.Models;

namespace MyBrandNewCV.DataAccess.Models
{
    public class UserModel : IdentityUser
    {
        

        public string FirstName { get; set; }
        public string LastName { get; set; }
      
        public List<Skill> Skills { get; set; }
        public List<Project> Projects { get; set; }
    }
}
