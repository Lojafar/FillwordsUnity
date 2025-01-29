using UnityEngine;
using R3;
namespace FillWords.Gameplay.Game.Root.Cell
{
    abstract class LetterCellBase : MonoBehaviour
    {
        CellData cellData;
        public CellData CellData => cellData;
        public Subject<LetterCellBase> CellInputEvent { get; private set; }
        public void Init(CellData _cellData)
        {
            cellData = _cellData;
            CellInputEvent = new();
            OnInited();
        }
        protected virtual void OnInited()
        {

        }
        public virtual void ConfirmSelection()
        {
        }
        public abstract void Select(Color color);
        public abstract void SelectAsFirst();
        public abstract void Deselect();
    }
}
