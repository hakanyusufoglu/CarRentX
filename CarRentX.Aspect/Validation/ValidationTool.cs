using FluentValidation;

namespace CarRentX.Aspect.Validation
{
	public static class ValidationTool
	{
		// Bu sınıf, FluentValidation kütüphanesini kullanarak nesne doğrulama işlemlerini gerçekleştirmek için yardımcı bir yöntem sağlar.
		// Validate yöntemi, bir doğrulayıcı (validator) ve doğrulanacak nesneyi alır.
		public static void Validate(IValidator validator, object entity)
		{
			// Bir doğrulama bağlamı (ValidationContext) oluşturulur ve doğrulanacak nesne ile ilişkilendirilir.
			var context = new ValidationContext<object>(entity);

			// Doğrulayıcıyı kullanarak bağlamı doğrular ve sonucu elde eder.
			var result = validator.Validate(context);
			// Sonuç geçerli değilse, bir ValidationException istisnası oluşturulur ve hata listesi ile birlikte fırlatılır.
			if (!result.IsValid)
			{
				throw new ValidationException(result.Errors);
			}
		}
	}
}
