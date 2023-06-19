using CarRentX.Manager.Abstact;
using CarRentX.Manager.Concrete;
using CarRentX.ViewModel.Car;
using CarRentX.ViewModel.Customer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarRentX.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CustomersController : ControllerBase
	{
		private readonly ICustomerManager _customerManager;

		public CustomersController(ICustomerManager customerManager)
		{
			_customerManager = customerManager;
		}

		[HttpGet]
		public IActionResult GetAll()
		{
			var result = _customerManager.GetAll();
			if (!result.Status)
				return BadRequest(result);
			return Ok(result);
		}
		[HttpGet("GetByIdAsync")]
		public async Task<IActionResult> GetByIdAsync(int id)
		{
			var result = await _customerManager.GetByIdAsync(id);
			if (!result.Status)
				return BadRequest(result);
			return Ok(result);
		}
		[HttpPost("AddAsync")]
		public async Task<IActionResult> AddAsync(CustomerViewModel customerViewModel)
		{
			var result = await _customerManager.AddAsync(customerViewModel);
			if (!result.Status)
				return BadRequest(result);
			return Created(string.Empty, result);
		}
		[HttpPut("Update")]
		public IActionResult Update(CustomerViewModel customerViewModel)
		{
			var result = _customerManager.Update(customerViewModel);
			if (!result.Status)
				return BadRequest(result);
			return NoContent();
		}
		[HttpDelete("RemoveAsync")]
		public async Task<IActionResult> RemoveAsync(int id)
		{
			var result = await _customerManager.RemoveAsync(id);
			if (!result.Status)
				return BadRequest(result);
			return NoContent();
		}
		[HttpPost("Remove")]
		public IActionResult Remove(CustomerViewModel customerViewModel)
		{
			var result = _customerManager.Remove(customerViewModel);
			if (!result.Status)
				return BadRequest(result);
			return NoContent();
		}
	}
}
