using MCBA_Admin.Data;
using MCBA_Admin.Models;
using Microsoft.EntityFrameworkCore;
using System.Net.Http;


namespace MCBA_Admin.Services;

public class AdminService : IAdminService
{
    private readonly HttpClient _httpClient;
    public AdminService(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri("http://localhost:7016/");
    }

    public async Task<List<Customer>> GetAllCutomersAsync()
    {
        var response = await _httpClient.GetAsync("api/v1/customers");
        if (response.IsSuccessStatusCode)
        {
            var customers = await response.Content.ReadFromJsonAsync<List<Customer>>();
            return customers;
        }
        else
        {
            // Handle error cases
            throw new Exception("Failed to retrieve customers.");
        }
    }
}