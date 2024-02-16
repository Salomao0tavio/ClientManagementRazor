using System.Net;
using B2B.API.Controllers.Sharred;
using B2B.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using B2B.Application.Interfaces;

namespace B2B.API.Controllers
{
    [ApiController]
    [Route("v1/customers")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerRepository _CustomerRepository;

        public CustomerController(ICustomerRepository CustomerService)
        {
            _CustomerRepository = CustomerService ?? throw new ArgumentNullException(nameof(CustomerService));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Customer), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(CustomProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Customer>> Get(Guid id)
        {
            try
            {
                var customer = await _CustomerRepository.Get(id);

                if (customer.Value == null)
                {
                    var notFoundDetails = new CustomProblemDetails(HttpStatusCode.NotFound, Request, "Cliente nao encontrado");
                    return NotFound(notFoundDetails);
                }

                return customer;
            }
            catch (Exception ex)
            {
                var badRequestDetails = new CustomProblemDetails(HttpStatusCode.BadRequest, Request, "Um erro foi encontrado ao salvar mudanças.", new List<string> { ex.Message });
                return BadRequest(badRequestDetails);
            }

        }

        [HttpGet]
        [ProducesResponseType(typeof(List<Customer>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<Customer>>> GetAll()
        {
            try
            {
                var customers = await _CustomerRepository.GetAll();
                return customers;
            }
            catch (Exception ex)
            {
                var internalServerErrorDetails = new CustomProblemDetails(HttpStatusCode.InternalServerError, Request, "Erro interno servidor", new List<string> { ex.Message });
                return StatusCode(500, internalServerErrorDetails);
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(Customer), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(CustomProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Customer>> Post([FromBody] Customer customer)
        {
            try
            {
                var createdCustomer = await _CustomerRepository.Post(customer);

                if (createdCustomer.Value == null)
                {
                    var internalServerErrorDetails = new CustomProblemDetails(HttpStatusCode.InternalServerError, Request, "Erro interno servidor", new List<string> { "Erro ao criar cliente." });
                    return StatusCode(500, internalServerErrorDetails);
                }              
               
                return CreatedAtAction(nameof(Get), new { id = createdCustomer.Value.ID }, createdCustomer.Value);
            }
            catch (Exception ex)
            {
                var badRequestDetails = new CustomProblemDetails(HttpStatusCode.BadRequest, Request, "Dados inválidos", new List<string> { ex.Message });
                return BadRequest(badRequestDetails);
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(CustomProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Update(Guid id, [FromBody] Customer updatedCustomer)
        {
            try
            {
                var result = await _CustomerRepository.Update(id, updatedCustomer);
                return result;
            }
            catch (Exception ex)
            {
                var badRequestDetails = new CustomProblemDetails(HttpStatusCode.BadRequest, Request, "Dados invalidos", new List<string> { ex.Message });
                return BadRequest(badRequestDetails);
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(CustomProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Delete(Guid id)
        {
            try
            {
                var result = await _CustomerRepository.Delete(id);

                if (result is NotFoundResult)
                {
                    var notFoundDetails = new CustomProblemDetails(HttpStatusCode.NotFound, Request, "Cliente nao encontrado");
                    return NotFound(notFoundDetails);
                }

                return result;
            }
            catch (Exception ex)
            {
                var internalServerErrorDetails = new CustomProblemDetails(HttpStatusCode.InternalServerError, Request, "Erro interno servidor", new List<string> { ex.Message });
                return StatusCode(500, internalServerErrorDetails);
            }
        }
    }
}
