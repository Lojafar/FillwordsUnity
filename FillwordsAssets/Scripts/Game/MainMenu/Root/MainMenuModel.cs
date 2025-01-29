using FillWords.Root.UI.Tabs;
using System;

namespace FillWords.MainMenu.Root
{
    public class MainMenuModel : ParentTabModel
    {
        public void OnSettingsBtn()
        {
            OpenTab<SettingsTabModel>();
        }
        public void OnHomeBtn()
        {
            OpenTab<HomeTabModel>();
        }
        public override void Open(Action onCompleted)
        {
            base.Open(onCompleted);
            OpenTab<HomeTabModel>();
        }
    }
}
