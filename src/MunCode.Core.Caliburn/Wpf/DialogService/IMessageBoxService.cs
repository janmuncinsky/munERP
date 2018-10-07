namespace MunCode.Core.Wpf.DialogService
{
    using System.Windows;

    public interface IMessageBoxService
    {
        MessageBoxResult ShowMessageBox(string messageBoxText);
    }
}