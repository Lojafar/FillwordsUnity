using FillWords.Root.UI.Tabs;
using FillWords.Root.Data;
using FillWords.Gameplay.Root;
using FillWords.Gameplay.Game.Root.Cell;
using FillWords.Gameplay.Level;
using System;
using System.Collections.Generic;
using System.Text;
using R3;

namespace FillWords.Gameplay.Game.Root
{
    public class GameFieldModel : ITabModel 
    {
        public readonly Subject<Action> OpeningEvent;
        public readonly Subject<Action> ClosingEvent;
        public readonly Subject<Unit> VictoryEvent;
        public readonly Subject<(int, int)> InitSizeEvent;
        public readonly Subject<CellData> CellInitedEvent;
        public readonly Subject<bool> EndSelectionEvent;
        public readonly Subject<Unit> SelectCellEvent;
        public readonly Subject<CellPos> DeselectCellEvent;
        readonly GameTabModel gameTabModel;
        readonly ILevelLoader levelLoader;
        readonly AllDataContainer allDataContainer;
        LevelData levelData;
        int findedWords;

        readonly List<CellData> selectedCells;
        readonly StringBuilder selectedWord;
        public GameFieldModel(GameTabModel _gameTabModel, ILevelLoader _levelLoader, AllDataContainer _allDataContainer)
        {
            OpeningEvent = new();
            ClosingEvent = new();
            VictoryEvent = new();
            InitSizeEvent = new();
            CellInitedEvent = new();
            EndSelectionEvent = new();
            SelectCellEvent = new();
            DeselectCellEvent = new();
            selectedCells = new List<CellData>();
            selectedWord = new StringBuilder(10);

            gameTabModel = _gameTabModel;
            levelLoader = _levelLoader;
            allDataContainer = _allDataContainer;
             
            gameTabModel.OnPlayLevel.Subscribe(_ => UpdateField());
        }
        public void UpdateField()
        {
            findedWords = 0;
            InitField();
        }
        void InitField()
        {
            levelData = levelLoader.LoadLevel(allDataContainer.ProgressData.CurrentLevel.Value);
            int LengthX = levelData.LettersField.GetLength(0);
            int LengthY = levelData.LettersField.GetLength(1);
            InitSizeEvent.OnNext((LengthX, LengthY));

            for(int x = 0; x < LengthX; x++)
            {
                for(int y = 0; y < LengthY; y++)
                {
                    CellData cellData = new(levelData.LettersField[x, y], (byte)x, (byte)y);
                    CellInitedEvent.OnNext(cellData);
                }
            }
        }
        public void OnCellSellected(CellData selectedCellData)
        {
            CellData lastSellectedCell = selectedCells.Count > 0 ? selectedCells[^1] : null;
            if (lastSellectedCell != null && !IsCellsNeighbors(selectedCellData.Position, lastSellectedCell.Position)) return;

            if (!IsCellSelected(selectedCellData.Position))
            {
                selectedWord.Append(selectedCellData.Letter);
                selectedCells.Add(selectedCellData);
                SelectCellEvent.OnNext(Unit.Default);
            }
            else
            {
                if (selectedCells[^2].Position == selectedCellData.Position)
                {
                    DeselectCellEvent.OnNext(selectedCells[^1].Position);
                    selectedCells.RemoveAt(selectedCells.Count - 1);
                    selectedWord.Remove(selectedWord.Length - 1, 1);
                }
            }
           
        }
        bool IsCellsNeighbors(CellPos cellPosFirst, CellPos cellPosSecond)
        {
            return
                cellPosFirst.X == cellPosSecond.X + 1 && cellPosFirst.Y == cellPosSecond.Y ||
                cellPosFirst.X == cellPosSecond.X - 1 && cellPosFirst.Y == cellPosSecond.Y ||
                cellPosFirst.X == cellPosSecond.X && cellPosFirst.Y == cellPosSecond.Y + 1 ||
                cellPosFirst.X == cellPosSecond.X && cellPosFirst.Y == cellPosSecond.Y - 1;
        }
        bool IsCellSelected(CellPos cellPos) 
        {
            foreach (CellData cellData in selectedCells)
            {
                if (cellData.Position == cellPos)
                {
                    return true;
                }
            }
            return false;
        }
        public void OnEndSelecting()
        {
            if (selectedWord.Length == 0) return;
            if (levelData.LevelWords.Contains(selectedWord.ToString()))
            {
                EndSelectionEvent.OnNext(true);
                findedWords++;
                if (levelData.LevelWords.Count <= findedWords)
                {
                    VictoryEvent.OnNext(Unit.Default);
                }
            }
            else
            {
                EndSelectionEvent.OnNext(false);
            }
            selectedWord.Clear();
            selectedCells.Clear();
        }
        public void OnVictoryCongratsEnded()
        {
            gameTabModel.CompleteLevel();
        }
        public void Open(Action onComleted)
        {
            OpeningEvent.OnNext(onComleted);
        }
        public void Close(Action onComleted)
        {
            ClosingEvent.OnNext(onComleted);
        }
    }
}