﻿using DirectBookingOne.Api.Models;
using DirectBookingOne.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace DirectBookingOne.Api.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductService _productService;

        public ProductController(ProductService productService) =>
            _productService = productService;

        [HttpGet]
        public async Task<List<Product>> Get() =>
            await _productService.GetAsync();

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Product>> Get(string id)
        {
            var product = await _productService.GetAsync(id);

            if (product is null) return NotFound();

            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> Post(Product model)
        {
            await _productService.CreatAsync(model);

            return CreatedAtAction(nameof(Get), new { id = model.Id }, model);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, Product model)
        {
            var product = await _productService.GetAsync(id);

            if (product is null) return NotFound();

            model.Id = product.Id;

            await _productService.UpdateAsync(id, model);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var product = await _productService.GetAsync(id);

            if (product is null) return NotFound();

        await _productService.RemoveAsync(id);

            return NoContent();
    
    }



}
}
