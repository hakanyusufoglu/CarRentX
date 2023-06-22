using Castle.DynamicProxy;
using System.Reflection;

namespace CarRentX.Aspect.Interceptors
{
	public class AspectInterceptorSelector : IInterceptorSelector
	{
		// Bu sınıf, IInterceptorSelector arayüzünü uygular.
		// SelectInterceptors metodu, bir tür, bir metot ve mevcut interceptor'ları alır ve interceptor'ları seçmek için kullanılır.
		public IInterceptor[] SelectInterceptors(Type type, MethodInfo method, IInterceptor[] interceptors)
		{
			// type değişkeninin tüm MethodInterceptionBaseAttribute özniteliklerini alır ve listeye dönüştürür.
			var classAttributes = type.GetCustomAttributes<MethodInterceptionBaseAttribute>(true).ToList();

			// method değişkeninin ismine göre ilgili metodu bulur ve bu metoda uygulanan MethodInterceptionBaseAttribute özniteliklerini alır.
			var methodAttributes = type.GetMethod(method.Name).GetCustomAttributes<MethodInterceptionBaseAttribute>(true);

			// classAttributes listesine methodAttributes listesini ekler.
			classAttributes.AddRange(methodAttributes);

			// classAttributes listesini önceliklerine göre sıralar ve diziye dönüştürerek geri döner.
			return classAttributes.OrderBy(x => x.Priority).ToArray();
		}
	}
}
