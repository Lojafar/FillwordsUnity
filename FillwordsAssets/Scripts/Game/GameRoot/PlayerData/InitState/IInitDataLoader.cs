using FillWords.Root.ServiceInterfaces;
namespace FillWords.Root.Data.Initial 
{
    public interface IInitDataLoader : IPrewarmableService
    {
        public ProgressData LoadInitProgress();
        public SettingsData LoadInitSettings();
    }
}
