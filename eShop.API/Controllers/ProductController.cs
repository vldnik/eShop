using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Business.Abstraction;
using Business.Models;
using Microsoft.AspNetCore.Mvc;

namespace eShop.API.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ICrudInterface<ProductDto> _productService;

        public ProductController(ICrudInterface<ProductDto> productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetAll()
            => Ok(await _productService.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetById(int id)
            => Ok(await _productService.GetByIdAsync(id));

        [HttpPost]
        public async Task<ActionResult> Add([FromBody] ProductDto productDto)
        {
            try
            {
                await _productService.AddAsync(productDto);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

            return CreatedAtAction(nameof(Add), new { productDto.Id }, productDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, ProductDto productDto)
        {
            try
            {
                await _productService.UpdateAsync(id, productDto);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _productService.DeleteByIdAsync(id);

            return Ok();
        }
    }
}