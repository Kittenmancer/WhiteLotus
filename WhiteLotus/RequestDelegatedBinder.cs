using System.Web.Mvc;
using Autofac;

namespace WhiteLotus
{
    public class RequestDelegatedBinder<T> : IModelBinder
    {

        private readonly IContainer _provider;

        public RequestDelegatedBinder(IContainer provider)
        {
            _provider = provider;
        }

        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var binder = (IModelBinder)DependencyResolver.Current.GetService<T>();

            //var binder = (IModelBinder) _provider.Resolve<T>();
            return binder.BindModel(controllerContext, bindingContext);
        }
    }
}