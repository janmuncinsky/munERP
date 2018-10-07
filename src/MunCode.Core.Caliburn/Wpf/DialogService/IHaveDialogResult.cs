namespace MunCode.Core.Wpf.DialogService
{
    public interface IHaveDialogResult<out TDialogResult>
    {
        TDialogResult DialogResult { get; }
    }
}