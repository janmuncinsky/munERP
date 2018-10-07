namespace MunCode.Core.Design.Patterns.Gof.Singleton
{
    using System;

    public abstract class Singleton<TSingleton>
        where TSingleton : class
    {
        private static readonly Lazy<TSingleton> InstanceInternal = new Lazy<TSingleton>(() => (TSingleton)Activator.CreateInstance(typeof(TSingleton), true));

        protected Singleton()
        {
        }

        public static TSingleton Instance => InstanceInternal.Value;
    }
}
