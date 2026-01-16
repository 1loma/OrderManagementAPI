using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Order_Management_API.Data;
using Order_Management_API.Models;

namespace Order_Management_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly OrderManagementAPIDbContext _context;

        public OrderController(OrderManagementAPIDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<ActionResult<List<Order>>> GetAllOrders()
        {
            return Ok(await _context.Orders.ToListAsync());
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrderCustomerAndOrderDetailsInfo(int id)
        {
            var orderInfo = await _context.Orders
                .Include(c => c.Customer)
                .Include(od => od.OrderDetail)
                .Where(o => o.Id == id)
                .Select(x => new
                {
                    id = x.Id,
                    orderData = x.OrderDate,
                    customerId = x.CustomerId,
                    customer = x.Customer,
                    orderDetails = x.OrderDetail
                }).FirstOrDefaultAsync();

            return Ok(orderInfo);
        }



        [HttpPost]
        public async Task<IActionResult> AddOrder(Order order)
        {
            if (order == null)
                return BadRequest();

            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();

            return NoContent();
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrder(int id, Order newOrder)
        {
            if (newOrder == null)
                return BadRequest();
            var order = await _context.Orders.FirstOrDefaultAsync(x => x.Id == id);
            if (order == null)
                return NotFound();

            order.OrderDate = newOrder.OrderDate;
            order.CustomerId = newOrder.CustomerId;

            await _context.SaveChangesAsync();


            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(x => x.Id == id);
            if (order == null)
                return NotFound();

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();

            return Ok(order);
        }




    }
}
