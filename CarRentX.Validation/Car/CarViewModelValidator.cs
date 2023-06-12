using CarRentX.ViewModel.Car;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentX.Validation.Car
{
	//ToDO: statik mesajlar appsettingsten okunmalıdır
	public class CarViewModelValidator : AbstractValidator<CarViewModel>
	{
		public CarViewModelValidator()
		{
			RuleFor(x => x.Name).MinimumLength(2).WithMessage("Araç adı iki karakterden büyük olmalıdır");
			RuleFor(x => x.DailyPrice).GreaterThan(0).WithMessage("Günlük araba fiyatı 0'dan büyük olmalıdır");
		}
	}
}
