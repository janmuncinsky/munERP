namespace MunCode.Core.Ioc
{
    using System;

    [Serializable]
    public class InstallationAssembliesNotFoundException : Exception
    {
        public InstallationAssembliesNotFoundException(string assemblyName) 
            : base($"Cannot find any installation assembly with suffix - '{assemblyName}'")
        {
        }
    }
}