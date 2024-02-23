using MyBrandNewCv.Common.Models;
using MyBrandNewCV.Pages;
using MyBrandNewCV.Services.Interfaces;

namespace MyBrandNewCV.Services
{
    public class ProjectService : IProjectService
    {
        private readonly HttpClient _client;

        public ProjectService(IHttpClientFactory factory)
        {
            _client = factory.CreateClient("api");
        }
        public async Task AddAsync(Project project)
        {
            try
            {
                await _client.PostAsJsonAsync("api/projects", project);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error adding project: {ex.Message}");
            }

        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                await _client.DeleteAsync($"api/projects/{id}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting skill: {ex.Message}");
            }

        }

        public async Task<IEnumerable<Project>> GetAllAsync()
        {
            return await _client.GetFromJsonAsync<IEnumerable<Project>>("api/projects");
        }

        public async Task<Project> GetByIdAsync(int id)
        {
            return await _client.GetFromJsonAsync<Project>($"/api/projects/{id}");
        }

        public async Task UpdateAsync(Project project)
        {
            try
            {
                await _client.PutAsJsonAsync($"api/projects/{project.Id}", project);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error updating project: {ex.Message}");

            }
        }
    }
    
}
