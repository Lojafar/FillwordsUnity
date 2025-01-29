using UnityEngine;

namespace FillWords.Root.Data.Initial
{
    [CreateAssetMenu(fileName = "InitialState", menuName = "ScriptableObjs/InitialData")]
    public class DataInitStateSO : ScriptableObject
    {
        [field: SerializeField] public ProgressData InitialProgress { get; private set; }
        [field: SerializeField] public SettingsData InitialSettings { get; private set; }
    }
}
