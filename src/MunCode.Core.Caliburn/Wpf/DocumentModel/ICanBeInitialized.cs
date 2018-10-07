namespace MunCode.Core.Wpf.DocumentModel
{
    using System.Threading.Tasks;

    public interface ICanBeInitialized
    {
        Task Initialize();
    }
}