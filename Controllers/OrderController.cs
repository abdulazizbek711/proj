using AutoMapper;
using MarketApi.Data;
using MarketApi.Dtos;
using MarketApi.Interfaces;
using MarketApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace MarketApi.Controllers

{
    [Route("api/[controller]")]
    [ApiController]

    public class OrderController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IOrderRepository _orderRepository;
        private readonly IUserRepository _userRepository;
        private readonly DataContext _context;

        public OrderController(IMapper mapper, IOrderRepository orderRepository,IUserRepository userRepository, DataContext context)
        {
            _mapper = mapper;
            _orderRepository = orderRepository;
            _userRepository = userRepository;
            _context = context;
        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Order>))]
        public IActionResult GetOrders()
        {
            var orders = _orderRepository.GetOrders();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(orders);
        }

        

        [HttpPost("{Order_number}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateOrder([FromQuery]int User_ID, [FromQuery]int Product_ID, [FromBody] OrderDto orderCreate)
        {
            if (orderCreate == null)
                return BadRequest(ModelState);

            // Check if the review name already exists
            var order = _orderRepository.GetOrders()
                .Where(c => c.Order_number.ToString().ToUpper() == orderCreate.Order_number.ToString().ToUpper())
                .FirstOrDefault();
            if (order != null)
            {
                ModelState.AddModelError("", "Order already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            

            // Check if the Piece with the provided PieceId exists


            var orderMap = _mapper.Map<Order>(orderCreate);
            orderMap.User = _userRepository.GetUser(User_ID);




            if (!_orderRepository.CreateOrder(orderMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully Created");
        }

        [HttpPut("{Order_number}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]

        public IActionResult UpdateOrder(int Order_number, [FromBody] OrderDto updatedOrder)
        {
            if (updatedOrder == null)
                return BadRequest(ModelState);
            if (Order_number != updatedOrder.Order_number)
                return BadRequest(ModelState);
            if (!_orderRepository.OrderExists(Order_number))
                return NotFound();
            if (!ModelState.IsValid)
                return BadRequest();
            var orderMap = _mapper.Map<Order>(updatedOrder);
            if (!_orderRepository.UpdateOrder(orderMap))
            {
                ModelState.AddModelError("", "Something went wrong updating order");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{Order_number}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteOrder(int Order_number)
        {
            if (!_orderRepository.OrderExists(Order_number))
                return NotFound();
            var orderToDelete = _orderRepository.GetOrder(Order_number);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (!_orderRepository.DeleteOrder(orderToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting order");
            }

            return NoContent();

        }

    }
}