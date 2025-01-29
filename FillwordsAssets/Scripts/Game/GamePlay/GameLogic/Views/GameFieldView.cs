using FillWords.Root.AssetManagment;
using FillWords.Root.Audio;
using FillWords.Gameplay.Game.Root;
using FillWords.Gameplay.Game.Root.Cell;
using System;
using R3;
using Zenject;
using UnityEngine;
using DG.Tweening;
namespace FillWords.Gameplay.Game
{
    public class GameFieldView : GameFieldViewBase
    {
        [SerializeField] CellsContainer cellsContainer;
        CellsSelection cellsSelection;
        LetterCellBase currentSelectedCell;
        IAssetProvider assetProvider;
        IAudioPlayer<AudioClip> audioPlayer;
        AudioClip cellAudioClip;
        Sequence victorySequence;
        const string cellClipPath = "Audio/BubblePop";
        const float victoryAnimPanelScale = 1.2f;
        const float victoryAnimDuration = 1.2f;
        [Inject]
        public void Construct(IAssetProvider _assetProvider, IAudioPlayer<AudioClip> _audioPlayer)
        {
            assetProvider = _assetProvider;
            audioPlayer = _audioPlayer;
            LoadAssets();
        }
        async void LoadAssets()
        {
            cellAudioClip = await assetProvider.LoadAsset<AudioClip>(cellClipPath);
        }
        public override void OnBind(GameFieldViewModel viewModel)
        {
            base.OnBind(viewModel);

            cellsContainer.Init(assetProvider);
            cellsSelection = new CellsSelection(assetProvider, cellsContainer.transform);

            victorySequence = DOTween.Sequence();
            victorySequence.
                Append(cellsContainer.transform.DOScale(new Vector3(victoryAnimPanelScale, victoryAnimPanelScale, 1), victoryAnimDuration / 2).SetEase(Ease.Flash)).
                Append(cellsContainer.transform.DOScale(Vector3.one, victoryAnimDuration / 2)).SetAutoKill(false);

            viewModel.VictoryCongratsEvent.Subscribe(callback => PlayVictoryAnimation(callback));
            viewModel.InitSizeEvent.Subscribe(size => InitField(size));
            viewModel.CellInitedEvent.Subscribe(cellData => OnCellInited(cellData));
            viewModel.SelectCellEvent.Subscribe(_ => OnSelectCell());
            viewModel.DeselectCellEvent.Subscribe(pos => OnDeselectCell(pos));
            viewModel.ConfirmSelectionEvent.Subscribe(_ => cellsSelection.ConfirmSelection());
            viewModel.DeselectionEvent.Subscribe(_ => cellsSelection.DeselectAll());
        }
        void PlayVictoryAnimation(Action callback)
        {
            victorySequence.Restart();
            victorySequence.Play().OnComplete(() => callback?.Invoke());
        }
        void InitField((int, int) size)
        {
            cellsContainer.Clear();
            cellsSelection.Clear();
            cellsContainer.SetContainerSize(size.Item1);
        }
        void OnCellInited(CellData cellData)
        {
            var CreatedCell = cellsContainer.SpawnCell(cellData);
            CreatedCell.CellInputEvent.Subscribe(cell => OnCellInput(cell));
        }
        void OnCellInput(LetterCellBase cell)
        {
            currentSelectedCell = cell;
            viewModel.OnCellSelected(cell.CellData);
        }
        void OnSelectCell()
        {
            audioPlayer.PlaySFX(cellAudioClip);
            cellsSelection.SelectCell(currentSelectedCell);
        }
        void OnDeselectCell(CellPos pos)
        {
            audioPlayer.PlaySFX(cellAudioClip);
            cellsSelection.Deselect(pos);
        }
        private void Update()
        {
            if (Input.GetMouseButtonUp(0)) EndSelection();
        }
        void EndSelection()
        {
            viewModel.OnEndSelecting();
        }
    }
}
