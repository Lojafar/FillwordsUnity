using FillWords.Root.UI.LoadingScreen;

namespace FillWords.Root.GameState.States.Params
{
    public struct DataPreparingParam
    {
        public readonly LoadingScreenBase LoadingScreen;
        public readonly float LoadedPercent;
        public DataPreparingParam(LoadingScreenBase _loadingScreen, float _loadedPercent)
        {
            LoadingScreen = _loadingScreen;
            LoadedPercent = _loadedPercent;
        }
    }
}
