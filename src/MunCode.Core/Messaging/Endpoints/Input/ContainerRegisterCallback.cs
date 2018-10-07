namespace MunCode.Core.Messaging.Endpoints.Input
{
    using System;

    public delegate void ContainerRegisterCallback(Type serviceType, Type implementationType);

    public delegate object ContainerResolveCallback(Type serviceType);
}