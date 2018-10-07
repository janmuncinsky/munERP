namespace MunCode.Core.Wpf.DocumentModel.Menu
{
    public abstract class MenuComponent : IMenuComponent
    {
        public abstract string Header { get; }

        public abstract void OpenScreen();
    }
}