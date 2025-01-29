using Zenject;
using FillWords.Root.UI;
using FillWords.Root.EntryPoint;
using FillWords.MainMenu.Root;
using FillWords.Gameplay.Root;
using FillWords.Gameplay.Level;

namespace Assets.Fillwords.Scripts.Game.GameRoot
{
    class GameplayInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindServices();
            BindGameViews();
        }
        void BindServices()
        {
            Container.Bind<ILevelLoader>().To<DummyLevelLoader>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<GameSceneEntryPoint>().AsSingle().NonLazy();
        }
        void BindGameViews()
        {
            Container.Bind<TabsHandler>().AsSingle().NonLazy();
            Container.Bind<MainMenuBinder>().AsSingle().NonLazy();
            Container.Bind<GameplayBinder>().AsSingle().NonLazy();
        }
    }
}
