namespace MunCode.Core.Reflection.Scanning
{
    using System;

    using MunCode.Core.Guards;

    public class TypeScanResult
    {
        public TypeScanResult(Type type)
        {
            Guard.NotNull(type, nameof(type));
            this.Type = type;
        }

        public Type Type { get; }
    }
}