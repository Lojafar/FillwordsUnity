using FillWords.Gameplay.Game.Root.Cell;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
namespace FillWords.Gameplay.Game
{
    public class LetterCell : LetterCellBase, IPointerEnterHandler, IPointerDownHandler
    {
        Image cellImage;
        [SerializeField] TMP_Text LetterText;
        [SerializeField] GameObject ExtraSelection;

        private void Awake()
        {
            cellImage = GetComponent<Image>();
        }
        protected override void OnInited()
        {
            base.OnInited();
            LetterText.raycastTarget = false;
            LetterText.text = CellData.Letter.ToString();
            Deselect();
        }
        public override void ConfirmSelection()
        {
            cellImage.raycastTarget = false;
        }
        public override void Select(Color color)
        {
            cellImage.color = color;
        }
        public override void SelectAsFirst()
        {
            ExtraSelection.SetActive(true);
        }
        public override void Deselect()
        {
            cellImage.color = Color.white;
            ExtraSelection.SetActive(false);
        }
        public void OnPointerEnter(PointerEventData eventData)
        {
            if (Input.GetMouseButton(0)) OnSelected();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            OnSelected();
        }
        void OnSelected()
        {
            CellInputEvent.OnNext(this);
        }
    }
}
