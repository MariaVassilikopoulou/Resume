using MyBrandNewCV.Services.Interfaces;
using MyBrandNewCv.Common.Models;

namespace MyBrandNewCV.Services
{
    public class SkillService : ISkillService
    {
        private readonly HttpClient _client;

        public SkillService(IHttpClientFactory factory)
        {
            _client = factory.CreateClient("api");
        }

        public async Task<Skill> GetByIdAsync(int id)
        {
            return await _client.GetFromJsonAsync<Skill>($"/api/skills/{id}");
        }

        public async Task<IEnumerable<Skill>> GetAllAsync()
        {
            return await _client.GetFromJsonAsync<IEnumerable<Skill>>("api/skills");
        }

        public async Task AddAsync(Skill skill)
        {
            try
            {
                await _client.PostAsJsonAsync("api/skills", skill);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error adding skill: {ex.Message}");
            }

        }

        public async Task UpdateAsync(Skill skill)
        {
            try
            {
                await _client.PutAsJsonAsync($"api/skills/{skill.Id}", skill);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error updating skill: {ex.Message}");

            }

        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                await _client.DeleteAsync($"api/skills/{id}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting skill: {ex.Message}");
            }


        }
    }
}
    






