using FillWords.Root.ServiceInterfaces;
using FillWords.Root.UI.LoadingScreen;
using FillWords.MainMenu.Root;
using FillWords.MainMenu;
using FillWords.Gameplay.Root;
using FillWords.Gameplay;
using FillWords.Gameplay.Game.Root;
using System.Threading.Tasks;
namespace FillWords.Root.UI
{
    public interface IUIFactory : IPrewarmableService
    {
        public Task<LoadingScreenBase> CreateMainLoadScreen();
        public Task<MainMenuViewBase> CreateMainMenuView();
        public Task<HomeTabViewBase> CreateHomeTabView();
        public Task<SettingsTabViewBase> CreateSettingsTabView();
        public Task<GameTabViewBase> CreateGameTabView();
        public Task<GameFieldViewBase> CreateGameFieldView();
        public Task<MiniSettingsViewBase> CreateMiniSettingsView();
        public Task<VictoryTabViewBase> CreateVictoryTabView();
    }
}