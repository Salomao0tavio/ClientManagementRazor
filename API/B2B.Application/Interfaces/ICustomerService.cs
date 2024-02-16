using B2B.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace B2B.Application.Interfaces;

public interface ICustomerService
{
    const string URLBASE = "https://localhost:7063/v1/customers";

    Task<Customer> Post(Customer model);

    Task Delete(Guid id);

    Task<List<Customer>> GetAll();

    Task<Customer> Get(Guid id);

    Task Update(Guid id, Customer model);
}