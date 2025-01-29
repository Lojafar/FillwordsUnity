using FillWords.Root.AssetManagment;
using System.Threading.Tasks;

namespace FillWords.Root.Data.Initial
{
    public class InitDataSOLoader : IInitDataLoader
    {
        readonly IAssetProvider assetProvider;
        DataInitStateSO dataInitSO;
        const string initialDataSOKey = "ProgressInitialState";
        public InitDataSOLoader(IAssetProvider _assetProvider)
        {
            assetProvider = _assetProvider;
        }
        public ProgressData LoadInitProgress()
        {
            return dataInitSO.InitialProgress;
        }
        public SettingsData LoadInitSettings()
        {
            return dataInitSO.InitialSettings;
        }
        public async Task Prewarm()
        {
            dataInitSO = await assetProvider.LoadConfig<DataInitStateSO>(initialDataSOKey);
        }
    }
}
