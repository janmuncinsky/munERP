namespace MunCode.Core.Ioc.Castle.Tests.Fakes
{
    public class FakeDecorator<T> : IFake<T>
    {
        public FakeDecorator(IFake<T> fake)
        {
        }
    }
}