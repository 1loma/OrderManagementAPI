using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Order_Management_API.Data;
using Order_Management_API.Models;

namespace Order_Management_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly OrderManagementAPIDbContext _context;

        public CustomerController(OrderManagementAPIDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Customer>>> GetCustomers()
        {
            return Ok(await _context.Customers.ToListAsync());
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<List<Customer>>> GetCustomersAndOrderInfo(int id)
        {
            var customer = await _context.Customers
                .Include(customer => customer.Orders)
                .Where(customer => customer.Id == id)
                .Select(customer => new
                {
                    id = customer.Id,
                    name = customer.FullName,
                    email = customer.Email,
                    phone = customer.Phone,
                    order = customer.Orders.Where(order => order.CustomerId == id)
                }).FirstOrDefaultAsync();

            return Ok(customer);
        }


        [HttpPost]
        public async Task<IActionResult> AddCustomer(Customer customer)
        {
            if (customer == null)
                return BadRequest();

            await _context.Customers.AddAsync(customer);
            await _context.SaveChangesAsync();

            return NoContent();
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCustomer(int id, Customer c)
        {
            if (c== null)
                return BadRequest();
            var customer = await _context.Customers.FirstOrDefaultAsync(x => x.Id == id);
            if (customer == null)
                return NotFound();

            customer.FullName = c.FullName;
            customer.Email = c.Email;
            customer.Phone = c.Phone;

            await _context.SaveChangesAsync();
            return Ok(customer);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            var customer = await _context.Customers.FirstOrDefaultAsync(c => c.Id == id);
            if (customer == null)
                return NotFound();

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();

            return NoContent();
        }




    }
}
