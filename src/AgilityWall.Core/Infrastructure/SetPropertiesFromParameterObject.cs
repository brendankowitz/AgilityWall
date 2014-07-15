using System;
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
            foreach (var prop in parameterObject.GetType().GetTypeInfo().DeclaredProperties)
            {
                var onViewModel = vmType.GetRuntimeProperty(prop.Name);
                if(onViewModel != null)
                    onViewModel.SetValue(viewModel, prop.GetValue(parameterObject));
                else
                    throw new Exception(string.Format("Could not find property '{0}' on  '{1}", prop.Name, vmType.Name));
            }
        }
    }
}