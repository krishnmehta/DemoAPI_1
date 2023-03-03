using customerDemoWebAPI.Data;
using customerDemoWebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;

namespace customerDemoWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly CustomerDbContext _customerDbContext;
        public CustomersController(CustomerDbContext customerDbContext)
        {
            _customerDbContext = customerDbContext;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllCustomers()
        {
            var customers = await _customerDbContext.Customers.ToListAsync();
            return Ok(customers);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomer(int id)
        {
            var customer = await _customerDbContext.Customers.FirstOrDefaultAsync(x=>x.id == id);
            if(customer== null)
            {
                return NotFound("Customer Not Found");
            }
            return Ok(customer);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCustomer(Customer customer)
        {
            await _customerDbContext.Customers.AddAsync(customer);
            await _customerDbContext.SaveChangesAsync();
            return Ok(customer);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCustomer(Customer customer)
        {
            var customerInList = await _customerDbContext.Customers.FindAsync(customer.id);
            if (customerInList == null)
            {
                return NotFound("Invalid ID");
            }
            customerInList.FirstName = customer.FirstName;
            customerInList.LastName = customer.LastName;
            customerInList.City = customer.City;

            await _customerDbContext.SaveChangesAsync();
            return Ok(customerInList);

        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            var customer = await _customerDbContext.Customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound("Invalid Detail");
            }
            _customerDbContext.Customers.Remove(customer);
            await _customerDbContext.SaveChangesAsync();

            return Ok(customer);
        }
    }
}