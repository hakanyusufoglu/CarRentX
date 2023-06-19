using CarRentX.Manager.Abstact;
using CarRentX.ViewModel.Rental;
using Microsoft.AspNetCore.Mvc;

namespace CarRentX.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class RentalsController : ControllerBase
	{
		private readonly IRentalManager _rentalManager;

		public RentalsController(IRentalManager rentalManager)
		{
			_rentalManager = rentalManager;
		}
		[HttpGet]
		public IActionResult GetAll()
		{
			var result = _rentalManager.GetAll();
			if (!result.Status)
				return BadRequest(result);
			return Ok(result);
		}
		[HttpPost("AddAsync")]
		public async Task<IActionResult> AddAsync(RentalViewModel rentalViewModel)
		{
			var result = await _rentalManager.AddAsync(rentalViewModel);
			if (!result.Status)
				return BadRequest(result);
			return Created(string.Empty, result);
		}
	
	}
}
