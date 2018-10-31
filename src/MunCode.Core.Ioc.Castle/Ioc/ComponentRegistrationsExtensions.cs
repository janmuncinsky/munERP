namespace MunCode.Core.Ioc
{
    using Castle.MicroKernel.Registration;

    public static class ComponentRegistrationsExtensions
    {
        public static ComponentRegistration<object> NamedIfNotEmpty(this ComponentRegistration<object> componentRegistration, string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                return componentRegistration.Named(name);
            }

            return componentRegistration;
        }
    }
}