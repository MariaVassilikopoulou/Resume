using Microsoft.EntityFrameworkCore;
using MyBrandNewCV.Api.Services.Interfaces;
using MyBrandNewCv.Common.Models;
using MyBrandNewCV.DataAccess;
using MyBrandNewCv.Common.Interfaces;

namespace MyBrandNewCV.Api.Services
{
    public class SkillRepository : ISkillRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public SkillRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Skill> GetByIdAsync(int id)
        {
            return await _dbContext.Skills.FindAsync(id);
        }

        public async Task<IEnumerable<Skill>> GetAllAsync()
        {
            return await _dbContext.Skills.ToListAsync();
        }

        public async Task AddAsync(Skill skill)
        {
            _dbContext.Skills.Add(skill);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Skill skill)
        {
            _dbContext.Update(skill);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {


            var skillToDelete = await _dbContext.Skills.FindAsync(id);
            if (skillToDelete != null)
            {
                _dbContext.Skills.Remove(skillToDelete);
                await _dbContext.SaveChangesAsync();
            }

        }
     


}




}

