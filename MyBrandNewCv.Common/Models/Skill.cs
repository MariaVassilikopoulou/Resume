using System.ComponentModel.DataAnnotations.Schema;

namespace MyBrandNewCv.Common.Models
{
    public class Skill
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int YearsOfExperience { get; set; }
        public string SkillLevel { get; set; }

        public string UserId { get; set; }

    }
}
