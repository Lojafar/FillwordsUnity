using FillWords.Root.UI.LoadingScreen;

namespace FillWords.Root.GameState.States.Params
{
    struct AssetLoadingParams
    {
        public readonly LoadingScreenBase LoadingScreen;
        public readonly float LoadedPercent;
        public AssetLoadingParams(LoadingScreenBase _loadingScreen, float _loadedPercent)
        {
            LoadingScreen = _loadingScreen;
            LoadedPercent = _loadedPercent;
        }
    }
}
