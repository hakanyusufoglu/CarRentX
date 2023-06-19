using CarRentX.Manager.Abstact;
using CarRentX.ViewModel;
using CarRentX.ViewModel.Customer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarRentX.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ColorsController : ControllerBase
	{
		private readonly IColorManager _colorManager;

		public ColorsController(IColorManager colorManager)
		{
			_colorManager = colorManager;
		}

		[HttpGet]
		public IActionResult GetAll()
		{
			var result = _colorManager.GetAll();
			if (!result.Status)
				return BadRequest(result);
			return Ok(result);
		}
		[HttpGet("GetByIdAsync")]
		public async Task<IActionResult> GetByIdAsync(int id)
		{
			var result = await _colorManager.GetByIdAsync(id);
			if (!result.Status)
				return BadRequest(result);
			return Ok(result);
		}
		[HttpPost("AddAsync")]
		public async Task<IActionResult> AddAsync(ColorViewModel colorViewModel)
		{
			var result = await _colorManager.AddAsync(colorViewModel);
			if (!result.Status)
				return BadRequest(result);
			return Created(string.Empty, result);
		}
		[HttpPut("Update")]
		public IActionResult Update(ColorViewModel colorViewModel)
		{
			var result = _colorManager.Update(colorViewModel);
			if (!result.Status)
				return BadRequest(result);
			return NoContent();
		}
		[HttpDelete("RemoveAsync")]
		public async Task<IActionResult> RemoveAsync(int id)
		{
			var result = await _colorManager.RemoveAsync(id);
			if (!result.Status)
				return BadRequest(result);
			return NoContent();
		}
		[HttpPost("Remove")]
		public IActionResult Remove(ColorViewModel colorViewModel)
		{
			var result = _colorManager.Remove(colorViewModel);
			if (!result.Status)
				return BadRequest(result);
			return NoContent();
		}
	}
}
