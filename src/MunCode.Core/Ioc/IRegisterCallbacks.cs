namespace MunCode.Core.Ioc
{
    using MunCode.Core.Messaging.Endpoints.Input;

    public interface IRegisterCallbacks
    {
        ContainerRegisterCallback SingletonRegisterCallback { get; }

        ContainerRegisterCallback CallScopeRegisterCallback { get; }
    }
}