using Microsoft.EntityFrameworkCore;
using MyBrandNewCv.Common.Interfaces;
using MyBrandNewCv.Common.Models;
using MyBrandNewCV.Api.Services.Interfaces;
using MyBrandNewCV.DataAccess;

namespace MyBrandNewCV.Api.Services
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ProjectRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task AddAsync(Project project)
        {
            _dbContext.Projects.Add(project);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var projectToDelete = await _dbContext.Projects.FindAsync(id);
            if (projectToDelete != null)
            {
                _dbContext.Projects.Remove(projectToDelete);
                await _dbContext.SaveChangesAsync();
            }
        }



        public async Task<IEnumerable<Project>>GetAllAsync()
        {
            return await _dbContext.Projects.ToListAsync();
        }


       

        public async Task<Project>GetByIdAsync(int id)
        {
            return await _dbContext.Projects.FindAsync(id);
        }



       public async Task UpdateAsync(Project project)
       {
           _dbContext.Update(project);
           await _dbContext.SaveChangesAsync();
       }
    }
}
