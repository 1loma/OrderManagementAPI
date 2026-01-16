using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Order_Management_API.Data;
using Order_Management_API.Models;

namespace Order_Management_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderDetailController : ControllerBase
    {
        private readonly OrderManagementAPIDbContext _context;

        public OrderDetailController(OrderManagementAPIDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<ActionResult<List<OrderDetail>>> GetAllOrders()
        {
            return Ok(await _context.OrderDetails.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDetail>> GetOrderDetailsAndOrderInfo(int id)
        {
            var order = await _context.OrderDetails
                .Where(od => od.Id == id)
                .Include(od => od.Order)
                .Select(x => new
                {
                    id = x.Id,
                    odrerId = x.OrderId,
                    order = x.Order,
                    price = x.TotalPrice,
                    address = x.ShippingAddress

                }).FirstOrDefaultAsync();

            if (order == null)
                return NotFound();

            return Ok(order);
        }



        [HttpPost]
        public async Task<IActionResult> AddOrderDetails(OrderDetail orderDetail)
        {
            if (orderDetail == null)
                return BadRequest();

            await _context.OrderDetails.AddAsync(orderDetail);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrderDetail(int id, OrderDetail orderDetail)
        {
            if (orderDetail == null)
                return BadRequest();
            var orderInfo = await _context.OrderDetails.FirstOrDefaultAsync(x => x.Id == id);
            if (orderInfo == null)
                return BadRequest();

            orderInfo.OrderId = orderDetail.OrderId;
            orderInfo.TotalPrice = orderDetail.TotalPrice;
            orderInfo.ShippingAddress = orderDetail.ShippingAddress;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrderDetail(int id)
        {
            var orderDetail = await _context.OrderDetails.FirstOrDefaultAsync(x => x.Id == id);
            if (orderDetail == null)
                return NotFound();

            _context.OrderDetails.Remove(orderDetail);
            await _context.SaveChangesAsync();

            return NoContent();
        }

    }
}
