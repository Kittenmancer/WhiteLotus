using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WhiteLotus.Models.Entities;
using NHibernate;

namespace WhiteLotus
{
    public class NHibernateEntityBinder : DefaultModelBinder
    {
        private readonly ISession _session;

        public NHibernateEntityBinder(ISession session)
        {
            _session = session;
        }

        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            Debug.WriteLine("Binding");
            return ShouldUseEntityBinding(bindingContext) ?
                PerformEntityBinding(controllerContext, bindingContext) :
                base.BindModel(controllerContext, bindingContext);
        }

        protected override void OnPropertyValidated(ControllerContext controllerContext, ModelBindingContext bindingContext, PropertyDescriptor propertyDescriptor, object value)
        {
            var propertyMetadata = bindingContext.PropertyMetadata[propertyDescriptor.Name];
            propertyMetadata.Model = value;
            var modelStateKey = CreateSubPropertyName(bindingContext.ModelName, propertyMetadata.PropertyName);

            // If we couldn't bind the value (because it was null and the property is a non-nullable
            // value type), then we need to find its required validator and let it run so that it can
            // generate the correct required message for us.
            if (bindingContext.ModelState.IsValidField(modelStateKey) &&
                    value == null &&
                    !TypeAllowsNullValue(propertyDescriptor.PropertyType))
            {
                var setMessage = false;
                ModelValidator requiredValidator = ModelValidatorProviders.Providers.GetValidators(propertyMetadata, controllerContext).FirstOrDefault(v => v.IsRequired);
                if (requiredValidator != null)
                {
                    foreach (var validationResult in requiredValidator.Validate(bindingContext.Model))
                    {
                        setMessage = true;
                        bindingContext.ModelState.AddModelError(modelStateKey, validationResult.Message);
                    }
                }

                // We always want to make sure there's a message
                if (!setMessage)
                {
                    bindingContext.ModelState.AddModelError(modelStateKey, "VALUE REQUIRED");
                }
            }
        }

        protected bool TypeAllowsNullValue(Type type)
        {
            return (!type.IsValueType || Nullable.GetUnderlyingType(type) != null);
        }

        object PerformEntityBinding(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var method = controllerContext.HttpContext.Request.HttpMethod;
            var actionName = controllerContext.RouteData.GetRequiredString("action");

            // For actions accessed via an HTTP GET
            // And also Delete/Remove actions accessed via POST
            // we want to fetch the appropriate entity from the route data - no need to do form binding
            if (method == "GET" || actionName.StartsWith("Delete") || actionName.StartsWith("Remove"))
            {
                return PerformFetchingFromRouteData(controllerContext, bindingContext);
            }

            if (method == "POST")
            {
                // For edit methods we want to fetch from the form post
                if (actionName.StartsWith("Edit") || actionName.StartsWith("Change"))
                {
                    bindingContext.ModelMetadata.Model = PerformFetchingFromForm(controllerContext, bindingContext);
                }

                return base.BindModel(controllerContext, bindingContext);
            }

            return null;
        }

        object PerformFetchingFromForm(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            // Assume that all entities have an Id property of type Long.
            const string primaryKeyName = "Id";

            // Try with a prefix
            var key = CreateSubPropertyName(bindingContext.ModelName, primaryKeyName);
            var propertyBinder = Binders.GetBinder(typeof(long));

            var innerBindingContext = new ModelBindingContext
            {
                ModelMetadata = ModelMetadataProviders.Current.GetMetadataForType(() => 0, typeof(int)),
                ModelName = key,
                ModelState = bindingContext.ModelState,
                ValueProvider = bindingContext.ValueProvider,
                FallbackToEmptyPrefix = true
            };
            var id = propertyBinder.BindModel(controllerContext, innerBindingContext) as int?;

            // Try without a prefix
            if (id == null)
            {
                innerBindingContext.ModelName = primaryKeyName;
                id = propertyBinder.BindModel(controllerContext, innerBindingContext) as int?;
            }

            return id == null ? null : LoadFromDatabase(bindingContext.ModelType, id.Value);
        }

        object PerformFetchingFromRouteData(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            int value;
            try
            {
                value = Convert.ToInt32(controllerContext.RouteData.GetRequiredString("Id"));
            }
            catch
            {
                return null;
            }

            return LoadFromDatabase(bindingContext.ModelType, value);
        }

        /// <summary>
        /// Determine if this binder should be used for the given request. Returns
        /// true if the class is one of our models, otherwise false.
        /// </summary>
        static bool ShouldUseEntityBinding(ModelBindingContext context)
        {
            var ns = context.ModelType.Namespace;
            var entityNs = typeof(Booking).Namespace;
            return ns == entityNs;
        }

        /// <summary>
        /// Attempts to load a single item from the database of the given type, based
        /// on the provided primary key.
        /// </summary>
        object LoadFromDatabase(Type type, object id)
        {
            return _session.Get(type, id);
        }
    }
}