using CarRentX.ViewModel.Car;
using CarRentX.ViewModel.Rental;
using FluentValidation;

namespace CarRentX.Validation.Rental
{
	public class RentalViewModelValidator: AbstractValidator<RentalViewModel>
	{
		public RentalViewModelValidator()
		{
		}
	}
}
