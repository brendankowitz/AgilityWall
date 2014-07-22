using System;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using Caliburn.Micro;

namespace AgilityWall.Core.Infrastructure
{
    public static class SetPropertiesFromParameterObject
    {
        public static void SetPropertiesFromNavigationParameter(this PropertyChangedBase viewModel, object parameterObject)
        {
            if (parameterObject == null) return;
            var vmType = viewModel.GetType();
            var asExpando = parameterObject as ExpandoObject;
            if (asExpando != null)
            {
                foreach (var item in asExpando)
                {
                    SetProperty(viewModel, item.Key, item.Value, vmType);
                }
            }
            else
            {
                foreach (var prop in parameterObject.GetType().GetRuntimeProperties())
                {
                    SetProperty(viewModel, prop.Name, prop.GetValue(parameterObject), vmType);
                }
            }
        }

        private static void SetProperty(PropertyChangedBase viewModel, string propertyName, object parameterObject, Type vmType)
        {
            var onViewModel = vmType.GetRuntimeProperty(propertyName);
            if (onViewModel != null)
                onViewModel.SetValue(viewModel, parameterObject);
            else
                throw new Exception(string.Format("Could not find property '{0}' on  '{1}", propertyName, vmType.Name));
        }
    }
}