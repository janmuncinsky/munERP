namespace MunCode.Core.Wpf.DialogService
{
    public interface IWindowFactory
    {
        TWindow Create<TWindow>();
    }
}