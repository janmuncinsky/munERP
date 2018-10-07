namespace MunCode.Core.Reflection.Scanning
{
    using System.Reflection;

    using MunCode.Core.Guards;

    public class AssemblyScanResult
    {
        public AssemblyScanResult(Assembly assembly)
        {
            Guard.NotNull(assembly, nameof(assembly));
            this.Assembly = assembly;
        }

        public Assembly Assembly { get; }
    }
}