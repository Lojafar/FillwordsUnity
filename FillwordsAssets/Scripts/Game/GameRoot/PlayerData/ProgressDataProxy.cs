using R3;

namespace FillWords.Root.Data
{
    public class ProgressDataProxy
    {
        public readonly ReactiveProperty<int> CurrentLevel;
        public readonly ProgressData DataOrigin;
        public ProgressDataProxy(ProgressData _progressData)
        {
            DataOrigin = _progressData;
            CurrentLevel = new ReactiveProperty<int>(DataOrigin.Level);
            CurrentLevel.Skip(1).Subscribe(newLevelNum =>
            {
                if (newLevelNum >= 0) DataOrigin.Level = newLevelNum;
            });

        }
    }
}
