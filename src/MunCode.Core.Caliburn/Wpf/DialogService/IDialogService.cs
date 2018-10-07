namespace MunCode.Core.Wpf.DialogService
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IDialogService
    {
        Task<TDialogResult> ShowDialog<TDialogResult>(object context = null, IDictionary<string, object> settings = null);
    }
}