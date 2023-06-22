using CarRentX.Aspect.Interceptors;
using Castle.DynamicProxy;
using FluentValidation;

namespace CarRentX.Aspect.Validation
{
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
	public class ValidationAspect : MethodInterception
	{
		// Aspect sınıfı, MethodInterception sınıfından türetilir.
		// Bu, metot çağrılmadan önce ve sonra yapılacak işlemleri sağlar.

		private Type _validatorType;

		public ValidationAspect(Type validatorType)
		{
			// Validator sınıfının doğruluğunu kontrol etmek için defensive coding uygulanır.
			// Eğer validatorType bir IValidator'den türetilmiyorsa bir hata fırlatılır.

			if (!typeof(IValidator).IsAssignableFrom(validatorType))
			{
				throw new ArgumentException("Bu bir doğrulama sınıfı değil", nameof(validatorType));
			}

			_validatorType = validatorType;
		}
		protected override void OnBefore(IInvocation invocation)
		{
			// Metot çağrılmadan önce yapılacak işlemler burada tanımlanır.
			var validator = (IValidator)Activator.CreateInstance(_validatorType);
			// _validatorType'tan bir instance oluşturulur ve IValidator'a dönüştürülür.

			var entityType = _validatorType.BaseType.GetGenericArguments()[0];
			// _validatorType'ın BaseType'ından generic argümanları alır ve ilk argümanı alır.
			// Bu, doğrulama yapılacak nesnenin tipini temsil eder.

			var entities = invocation.Arguments.Where(t => t != null && t.GetType() == entityType);
			// invocation.Arguments, metoda geçirilen argümanları içerir.
			// Bu satır, entityType'a eşit olan argümanları seçer.

			foreach (var entity in entities)
			{
				// ValidationTool sınıfı kullanılarak doğrulama işlemi gerçekleştirilir.
				// Her bir nesne için doğrulama yapılır.
				ValidationTool.Validate(validator, entity);
			}
		}
	}
}

