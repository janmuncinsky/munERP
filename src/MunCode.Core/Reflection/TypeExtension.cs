namespace MunCode.Core.Reflection
{
    using System;

    using MunCode.Core.Guards;

    public static class TypeExtension
    {
        public static Type GetTypeDefinition(this Type type)
        {
            Guard.NotNull(type, nameof(type));
            return type.IsGenericType ? type.GetGenericTypeDefinition() : type;
        }
    }
}