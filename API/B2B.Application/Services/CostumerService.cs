using System.Net.Http.Json;
using B2B.Application.Interfaces;
using B2B.Domain.Entities;

namespace B2B.Application.Services
{
    public class CustomerService : ICustomerService
        {
            private readonly HttpClient httpClient;
            private readonly string URLBASE = "https://localhost:7063/v1/customers";

            public CustomerService(HttpClient httpClient)
            {
                this.httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            }

            public async Task<Customer> Post(Customer model)
            {
                try
                {
                    var response = await httpClient.PostAsJsonAsync(URLBASE, model);

                    if (response.IsSuccessStatusCode)
                    {
                        return await response.Content.ReadFromJsonAsync<Customer>();
                    }
                    return null;
                }
                catch (HttpRequestException ex)
                {
                    throw new Exception(ex.Message);
                }
            }

            public async Task Delete(Guid id)
            {
                try
                {
                    var response = await httpClient.DeleteAsync($"{URLBASE}/{id}");
                    if (!response.IsSuccessStatusCode)
                    {
                        throw new Exception("Nao foi possivel excluir cliente.");
                    }
                }
                catch (HttpRequestException ex)
                {
                    throw new Exception(ex.Message);
                }
            }

            public async Task<List<Customer>> GetAll()
            {
                try
                {
                    var response = await httpClient.GetAsync(URLBASE);

                    if (response.IsSuccessStatusCode)
                    {
                        return await response.Content.ReadFromJsonAsync<List<Customer>>();
                    }

                    return new List<Customer>();
                }
                catch (HttpRequestException ex)
                {
                    throw new Exception(ex.Message);
                }
            }

            public async Task<Customer> Get(Guid id)
            {
                try
                {
                    var response = await httpClient.GetAsync($"{URLBASE}/{id}");

                    if (response.IsSuccessStatusCode)
                    {
                        return await response.Content.ReadFromJsonAsync<Customer>();
                    }

                    return null;
                }
                catch (HttpRequestException ex)
                {
                    throw new Exception(ex.Message);
                }
            }

            public async Task Update(Guid id, Customer model)
            {
                try
                {
                    var response = await httpClient.PutAsJsonAsync($"{URLBASE}/{id}", model);

                    if (!response.IsSuccessStatusCode)
                    {
                        throw new Exception($"Failed to update customer. Status code: {response.StatusCode}");
                    }
                }
                catch (HttpRequestException ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }
    }
