using B2B.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace B2B.Application.Interfaces;

public interface ICustomerRepository
{
    Task<ActionResult<Customer>> Get(Guid id);

    Task<List<Customer>> GetAll();

    Task<ActionResult<Customer>> Post(Customer customer);

    Task<ActionResult> Update(Guid id, Customer updatedCustomer);

    Task<ActionResult> Delete(Guid id);
}