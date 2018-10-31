namespace MunCode.Core.Ioc
{
    using System.Reflection;

    using Castle.Facilities.TypedFactory;

    public class ByArgumentComponentSelector : DefaultTypedFactoryComponentSelector
    {
        protected override string GetComponentName(MethodInfo method, object[] arguments)
        {
            if (arguments.Length == 1 && arguments[0] is string s && !string.IsNullOrEmpty(s))
            {
                return s;
            }

            return base.GetComponentName(method, arguments);
        }
    }
}