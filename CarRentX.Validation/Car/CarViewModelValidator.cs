using CarRentX.ViewModel.Car;
using FluentValidation;

namespace CarRentX.Validation.Car
{
	//ToDO: statik mesajlar appsettingsten okunmalıdır
	public class CarViewModelValidator : AbstractValidator<CarViewModel>
	{
		public CarViewModelValidator()
		{	
			RuleFor(c => c.Name).NotEmpty();
			RuleFor(x => x.Name).MinimumLength(2).WithMessage("Araç adı iki karakterden büyük olmalıdır");
			RuleFor(x => x.DailyPrice).GreaterThan(0).WithMessage("Günlük araba fiyatı 0'dan büyük olmalıdır");
		}
	}
}
