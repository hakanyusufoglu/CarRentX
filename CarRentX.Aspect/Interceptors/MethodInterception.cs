using Castle.DynamicProxy;

namespace CarRentX.Aspect.Interceptors
{
	public abstract class MethodInterception : MethodInterceptionBaseAttribute
		{
			// OnBefore: Metot çağrılmadan önce yapılacak işlemleri içeren sanal yöntem
			protected virtual void OnBefore(IInvocation invocation) { }

			// OnAfter: Metot çağrıldıktan sonra yapılacak işlemleri içeren sanal yöntem
			protected virtual void OnAfter(IInvocation invocation) { }

			// OnException: Metot çağrılırken hata oluştuğunda yapılacak işlemleri içeren sanal yöntem
			protected virtual void OnException(IInvocation invocation, System.Exception e) { }

			// OnSuccess: Metot başarıyla tamamlandığında yapılacak işlemleri içeren sanal yöntem
			protected virtual void OnSuccess(IInvocation invocation) { }

			public override void Intercept(IInvocation invocation)
			{
				var isSuccess = true;

				OnBefore(invocation); // Metot çağrılmadan önce yapılacak işlemler

				try
				{
					invocation.Proceed(); // Asıl metot çağrılır
				}
				catch (Exception e)
				{
					isSuccess = false;
					OnException(invocation, e); // Hata durumunda yapılacak işlemler
					throw; // Hatanın fırlatılması
				}
				finally
				{
					if (isSuccess)
					{
						OnSuccess(invocation); // Başarılı durumda yapılacak işlemler
					}
				}

				OnAfter(invocation); // Metot çağrıldıktan sonra yapılacak işlemler
			}
		}
	}