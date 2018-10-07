namespace MunCode.Core.Controllers
{
    using System;

    using Microsoft.AspNetCore.Mvc.ApplicationModels;

    public class FirstGenericArgumentNamingConventionAttribute : Attribute, IControllerModelConvention
    {
        public void Apply(ControllerModel controller)
        {
            var entityType = controller.ControllerType.GetGenericArguments()[0];
            controller.ControllerName = entityType.Name;
        }
    }
}