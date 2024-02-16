using B2B.Application.Interfaces;
using B2B.Domain.Entities;
using B2B.Infrastructure.Data.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace B2B.Infrastructure.Data.Repositories;

public class CustomerRepository : ICustomerRepository
{
    private readonly DataContext _context;

    public CustomerRepository(DataContext context)
    {
        _context = context;
    }

    public async Task<ActionResult<Customer>> Get(Guid id)
    {
        try
        {
            var customer = await _context.Customers.FindAsync(id);

            if (customer == null) return new NotFoundResult();

            return customer;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<List<Customer>> GetAll()
    {
        try
        {
            return await _context.Customers.ToListAsync();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<ActionResult<Customer>> Post(Customer customer)
    {
        try
        {
            _context.Add(customer);
            await _context.SaveChangesAsync();

            return customer;
        }
        catch (Exception ex)
        {
            throw new Exception("Erro ao criar cliente." + ex.Message);
        }
    }


    public async Task<ActionResult> Update(Guid id, Customer updatedCustomer)
    {
        try
        {
            var customer = _context.Customers.FirstOrDefault(u => u.ID == id);

            customer = updatedCustomer;

            _context.Customers.Update(customer);

            await _context.SaveChangesAsync();

            return new OkResult();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<ActionResult> Delete(Guid id)
    {
        try
        {
            var customer = await _context.Customers.FindAsync(id);

            if (customer == null) return new NotFoundResult();

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();

            return new NoContentResult();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}