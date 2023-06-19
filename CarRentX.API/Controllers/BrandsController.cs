using CarRentX.Manager.Abstact;
using CarRentX.Manager.Concrete;
using CarRentX.ViewModel;
using CarRentX.ViewModel.Customer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarRentX.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class BrandsController : ControllerBase
	{
		private readonly IBrandManager _brandManager;

		public BrandsController(IBrandManager brandManager)
		{
			_brandManager = brandManager;
		}

		[HttpGet]
		public IActionResult GetAll()
		{
			var result = _brandManager.GetAll();
			if (!result.Status)
				return BadRequest(result);
			return Ok(result);
		}
		[HttpGet("GetByIdAsync")]
		public async Task<IActionResult> GetByIdAsync(int id)
		{
			var result = await _brandManager.GetByIdAsync(id);
			if (!result.Status)
				return BadRequest(result);
			return Ok(result);
		}
		[HttpPost("AddAsync")]
		public async Task<IActionResult> AddAsync(BrandViewModel brandViewModel)
		{
			var result = await _brandManager.AddAsync(brandViewModel);
			if (!result.Status)
				return BadRequest(result);
			return Created(string.Empty, result);
		}
		[HttpPut("Update")]
		public IActionResult Update(BrandViewModel brandViewModel)
		{
			var result = _brandManager.Update(brandViewModel);
			if (!result.Status)
				return BadRequest(result);
			return NoContent();
		}
		[HttpDelete("RemoveAsync")]
		public async Task<IActionResult> RemoveAsync(int id)
		{
			var result = await _brandManager.RemoveAsync(id);
			if (!result.Status)
				return BadRequest(result);
			return NoContent();
		}
		[HttpPost("Remove")]
		public IActionResult Remove(BrandViewModel brandViewModel)
		{
			var result = _brandManager.Remove(brandViewModel);
			if (!result.Status)
				return BadRequest(result);
			return NoContent();
		}
	}
}
