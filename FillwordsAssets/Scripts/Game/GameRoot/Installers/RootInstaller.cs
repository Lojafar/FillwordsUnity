using FillWords.Root.GameState;
using FillWords.Root.UI;
using FillWords.Root.Audio;
using FillWords.Root.SceneManagment;
using FillWords.Root.AssetManagment;
using FillWords.Root.SaveLoad;
using FillWords.Root.Data;
using FillWords.Root.Data.Initial;
using FillWords.Utils;
using Zenject;
using UnityEngine;

namespace FillWords.Root.Installers
{
    class RootInstaller : MonoInstaller
    {
        [SerializeField] AudioSourcesHolder audioSourcesHolder;
        public override void InstallBindings()
        {
            BindInstances();
            BindFactories();
            BindServices();
        }
        void BindInstances()
        {
            CreateAndBindCoros();
            Container.Bind<AudioSourcesHolder>().FromInstance(audioSourcesHolder).AsSingle();
        }
        void CreateAndBindCoros()
        {
            Coroutines coroutines = new GameObject("[COROUTINES]").AddComponent<Coroutines>();
            DontDestroyOnLoad(coroutines);
            Container.Bind<Coroutines>().FromInstance(coroutines).AsSingle();
        }
        void BindFactories()
        {
            Container.Bind<IGameStatesFactory>().To<GameStatesFactory>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<UIFactory>().AsSingle().NonLazy();
        }
        void BindServices()
        {
            Container.Bind<IScenesLoader>().To<ScenesLoader>().AsSingle().NonLazy();
            Container.Bind<IAssetProvider>().To<ResourcesAssetProvider>().AsSingle().NonLazy();
            Container.Bind<ISaverLoader>().To<PrefsSaverLoader>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<InitDataSOLoader>().AsSingle().NonLazy();
            Container.Bind<AllDataContainer>().AsSingle();
            Container.BindInterfacesAndSelfTo<AudioPlayer>().AsSingle().NonLazy();
        }
    }
}