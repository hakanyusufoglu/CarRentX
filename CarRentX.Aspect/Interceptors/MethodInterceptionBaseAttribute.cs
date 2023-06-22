using Castle.DynamicProxy;

namespace CarRentX.Aspect.Interceptors
{
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
	public abstract class MethodInterceptionBaseAttribute : Attribute, IInterceptor
	{
		public int Priority { get; set; }

		// Intercept: Metotların takip edildiği ve işlemlerin yönlendirildiği ana yöntem
		public virtual void Intercept(IInvocation invocation)
		{
			// Bu sınıf varsayılan olarak boştur ve türetilen sınıflar tarafından ezilebilir.
			
		}
	}
}
