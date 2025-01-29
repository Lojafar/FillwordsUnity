using FillWords.Root.AssetManagment;
using FillWords.Gameplay.Game.Root.Cell;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace FillWords.Gameplay.Game
{
    public class CellsSelection
    {
        readonly List<LetterCellBase> selectedCells;
        readonly List<GameObject> appliedConnections;
        readonly Stack<GameObject> activeConnections;
        readonly Transform parentTransform;
        GameObject connectionPrefab;
        Color selectionColor;
        const string cellBorderKey = "CellSelectionBorder";
        public CellsSelection(IAssetProvider assetProvider, Transform _parentTransform)
        {
            selectedCells = new();
            appliedConnections = new();
            activeConnections = new();
            parentTransform = _parentTransform;
            UpdateColor();
            Task.WaitAll(LoadBorder(assetProvider));
        }
        void UpdateColor()
        {
            selectionColor = new Color(Random.Range(0.1f, 0.8f), Random.Range(0.1f, 0.8f), Random.Range(0.1f, 0.8f));
        }
        async Task LoadBorder(IAssetProvider assetProvider)
        {
            connectionPrefab = await assetProvider.LoadPrefab<GameObject>(cellBorderKey);
        }
        public void SelectCell(LetterCellBase letterCell)
        {
            letterCell.Select(selectionColor);
            selectedCells.Add(letterCell);
            if (selectedCells.Count > 1)
            {
                SpawnConnection(letterCell.transform.position, selectedCells[^2].transform.position);
            }
        }
        void SpawnConnection(Vector3 cellPosFirst, Vector3 cellPosSecond)
        {
            Vector2 connectOffset = cellPosFirst - cellPosSecond;
            GameObject spawnedConnection = Object.Instantiate(connectionPrefab, parentTransform);
            spawnedConnection.transform.position = new Vector3(cellPosFirst.x - connectOffset.x / 2, cellPosFirst.y - connectOffset.y / 2);
            int conRotationZ = Mathf.Approximately(connectOffset.y, 0) ? 0 : 90;
            spawnedConnection.transform.localEulerAngles = new Vector3(spawnedConnection.transform.localEulerAngles.x, spawnedConnection.transform.localEulerAngles.y, conRotationZ);
            activeConnections.Push(spawnedConnection);
        }
        public void Deselect(CellPos pos)
        {
            LetterCellBase cellToDeselect = GetCellByPos(pos);
            if(cellToDeselect == null)
            {
                return;
            }
            cellToDeselect.Deselect();

            if (activeConnections.Count > 0)
            {
                Object.Destroy(activeConnections.Pop());
            }
            selectedCells.Remove(cellToDeselect);
        }
        public LetterCellBase GetCellByPos(CellPos cellPos)
        {
            foreach(LetterCellBase letterCell in selectedCells)
            {
                if (letterCell.CellData.Position == cellPos) return letterCell;
            }
            return null;
        }
        public void DeselectAll()
        {
            foreach (LetterCellBase letterCell in selectedCells)
            {
                letterCell.Deselect();
            }
            while(activeConnections.Count > 0)
            {
                Object.Destroy(activeConnections.Pop());
            }
            ClearCollections();
        }
        public void ConfirmSelection()
        {
            selectedCells[0].SelectAsFirst();
            foreach (LetterCellBase letterCell in selectedCells)
            {
                letterCell.ConfirmSelection();
            }
            foreach(GameObject connection in activeConnections)
            {
                appliedConnections.Add(connection);
            }
            ClearCollections();
            UpdateColor();
        }
        public void Clear()
        {
            DeselectAll();
            while (appliedConnections.Count > 0)
            {
                Object.Destroy(appliedConnections[0]);
                appliedConnections.RemoveAt(0);
            }
        }
        void ClearCollections()
        {
            selectedCells.Clear();
            activeConnections.Clear();
        }
    }
}
