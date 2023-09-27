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
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly DataContext _context;

        public ProductController(IProductRepository productRepository, IMapper mapper, DataContext context)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _context = context;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Product>))]
        public IActionResult GetProducts()
        {
            var products = _productRepository.GetProducts();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(products);
        }

        

        [HttpPost("{Product_ID}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateProduct([FromBody] ProductDto productCreate)
        {
            if (productCreate == null)
                return BadRequest(ModelState);

            // Check if the review name already exists
            var product = _productRepository.GetProducts()
                .Where(c => c.Product_type.Trim().ToUpper() == productCreate.Product_type.TrimEnd().ToUpper())
                .FirstOrDefault();
            if (product != null)
            {
                ModelState.AddModelError("", "Product already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Check if the Piece with the provided PieceId exists


            var productMap = _mapper.Map<Product>(productCreate);




            if (!_productRepository.CreateProduct(productMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully Created");
        }

        [HttpPut("{Product_ID}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]

        public IActionResult UpdateProduct(int Product_ID, [FromBody] ProductDto updatedProduct)
        {
            if (updatedProduct == null)
                return BadRequest(ModelState);
            if (Product_ID != updatedProduct.Product_ID)
                return BadRequest(ModelState);
            if (!_productRepository.ProductExists(Product_ID))
                return NotFound();
            if (!ModelState.IsValid)
                return BadRequest();
            var productMap = _mapper.Map<Product>(updatedProduct);
            if (!_productRepository.UpdateProduct(productMap))
            {
                ModelState.AddModelError("", "Something went wrong updating product");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{Product_ID}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteProduct(int Product_ID)
        {
            if (!_productRepository.ProductExists(Product_ID))
                return NotFound();
            var productToDelete = _productRepository.GetProduct(Product_ID);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (!_productRepository.DeleteProduct(productToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting product");
            }

            return NoContent();

        }
    }
}