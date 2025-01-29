using FillWords.Gameplay.Game.Root.Cell;
using System;
using R3;
namespace FillWords.Gameplay.Game.Root
{
    public class GameFieldViewModel 
    {
        readonly GameFieldModel fieldModel;
        public readonly Subject<Action> OpeningEvent;
        public readonly Subject<Action> ClosingEvent;
        public readonly Subject<Action> VictoryCongratsEvent;
        public readonly Subject<(int, int)> InitSizeEvent;
        public readonly Subject<CellData> CellInitedEvent;
        public readonly Subject<Unit> SelectCellEvent;
        public readonly Subject<CellPos> DeselectCellEvent;
        public readonly Subject<Unit> ConfirmSelectionEvent;
        public readonly Subject<Unit> DeselectionEvent;
        public GameFieldViewModel(GameFieldModel model)
        {
            fieldModel = model;
            OpeningEvent = model.OpeningEvent;
            ClosingEvent = model.ClosingEvent;
            VictoryCongratsEvent = new();
            InitSizeEvent = model.InitSizeEvent;
            CellInitedEvent = model.CellInitedEvent;
            SelectCellEvent = model.SelectCellEvent;
            DeselectCellEvent = model.DeselectCellEvent;
            ConfirmSelectionEvent = new();
            DeselectionEvent = new();
            model.VictoryEvent.Subscribe(_ => VictoryCongratsEvent.OnNext(OnVictoryAnimsEnded));
            model.EndSelectionEvent.Subscribe(isWordRight =>
            {
                if (isWordRight) ConfirmSelectionEvent.OnNext(Unit.Default);
                else DeselectionEvent.OnNext(Unit.Default);
            });
        }
        void OnVictoryAnimsEnded()
        {
            fieldModel.OnVictoryCongratsEnded();
        }
        public void OnCellSelected(CellData cellData)
        {
            fieldModel.OnCellSellected(cellData);
        }
        public void OnEndSelecting()
        {
            fieldModel.OnEndSelecting();
        }
    }
}
