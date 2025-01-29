using System;
namespace FillWords.Root.UI.Tabs
{
    public interface ITabModel
    {
        public void Open(Action callback);
        public void Close(Action callback);
    }
}
