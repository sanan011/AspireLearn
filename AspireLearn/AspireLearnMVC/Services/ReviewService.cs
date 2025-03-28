using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using AspireLearnMVC.Models; // ✅ Use ReviewModel from MVC

namespace AspireLearnMVC.Services
{
    public class ReviewService
    {
        private readonly HttpClient _httpClient;

        public ReviewService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<ReviewModel>> GetReviewsByCourseIdAsync(int courseId)
        {
            return await _httpClient.GetFromJsonAsync<List<ReviewModel>>($"https://localhost:7285/api/reviews/{courseId}");
        }

        public async Task<bool> AddReviewAsync(ReviewModel review)
        {
            var response = await _httpClient.PostAsJsonAsync("https://localhost:7285/api/reviews", review);
            return response.IsSuccessStatusCode;
        }
    }
}
