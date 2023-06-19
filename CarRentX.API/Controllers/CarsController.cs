using CarRentX.Manager.Abstact;
using CarRentX.Manager.Concrete;
using CarRentX.ViewModel.Car;
using CarRentX.ViewModel.Customer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace CarRentX.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CarsController : ControllerBase
	{
		private readonly ICarManager _carManager;

		public CarsController(ICarManager carManager)
		{
			_carManager = carManager;
		}
		[HttpGet]
		public IActionResult GetAll()
		{
			var result = _carManager.GetAll();
			if (!result.Status)
				return BadRequest(result);
			return Ok(result);
		}
		[HttpGet("GetByIdAsync")]
		public async Task<IActionResult> GetByIdAsync(int id)
		{
			var result = await _carManager.GetByIdAsync(id);
			if (!result.Status)
				return BadRequest(result);
			return Ok(result);
		}
		[HttpPost("AddAsync")]
		public async Task<IActionResult> AddAsync(CarViewModel carViewModel)
		{
			var result = await _carManager.AddAsync(carViewModel);
			if (!result.Status)
				return BadRequest(result);
			return Created(string.Empty, result);
		}
		[HttpPut("Update")]
		public IActionResult Update(CarViewModel carViewModel)
		{
			var result = _carManager.Update(carViewModel);
			if(!result.Status)
				return BadRequest(result);
			return NoContent();
		}
		[HttpDelete("RemoveAsync")]
		public async Task<IActionResult> RemoveAsync(int id)
		{
			var result = await _carManager.RemoveAsync(id);
			if (!result.Status)
				return BadRequest(result);
			return NoContent();
		}
		[HttpPost("Remove")]
		public IActionResult Remove(CarViewModel carViewModel)
		{
			var result = _carManager.Remove(carViewModel);
			if (!result.Status)
				return BadRequest(result);
			return NoContent();
		}
	}
}
