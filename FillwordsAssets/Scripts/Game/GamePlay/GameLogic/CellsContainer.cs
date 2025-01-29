using FillWords.Root.AssetManagment;
using FillWords.Gameplay.Game.Root.Cell;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace FillWords.Gameplay.Game
{
    public class CellsContainer : MonoBehaviour
    {
        LetterCellBase letterCellPrefab;
        [SerializeField] GridLayoutGroup cellsGrid;
        List<LetterCellBase> spawnedLetterCells;

        [SerializeField] int MaxCellSize;
        [SerializeField] int CellSizeDivision;
        const int minCellsCount = 3;
        const string CellPrefabKey = "LetterCell";

        public async void Init(IAssetProvider assetProvider)
        {
            letterCellPrefab = await assetProvider.LoadPrefab<LetterCellBase>(CellPrefabKey);
            spawnedLetterCells = new List<LetterCellBase>();
        }
        public void SetContainerSize(int sizeX)
        {
            int cellSize = MaxCellSize - (sizeX - minCellsCount) * CellSizeDivision;
            cellsGrid.cellSize = new Vector2(cellSize, cellSize);
        }
        public LetterCellBase SpawnCell(CellData cellData)
        {
            LetterCellBase letterGameObject = Instantiate(letterCellPrefab, cellsGrid.transform);
            letterGameObject.Init(cellData);
            spawnedLetterCells.Add(letterGameObject);
            return letterGameObject;
        }
        public void Clear()
        {
            foreach(LetterCellBase cell in spawnedLetterCells)
            {
                Destroy(cell.gameObject);
            }
            spawnedLetterCells.Clear();
        }
    }
}
