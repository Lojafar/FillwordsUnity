using UnityEngine;
using UnityEngine.UI;
using TMPro;
namespace FillWords.Root.UI.LoadingScreen
{
    class MainLoadingScreen : LoadingScreenBase
    {
        [SerializeField] Image progressBar;
        [SerializeField] TMP_Text percentsTMP;
        public override void UpdateProgress(float progress)
        {
            progressBar.fillAmount = progress;
            percentsTMP.text = ((int)(progress * 100)).ToString() + "%";
        }

    }
}
