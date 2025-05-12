using System;
using _Source.Gameplay.FigureSystem;
using _Source.Gameplay.FigureSystem.Factory;
using _Source.Gameplay.UI;
using _Source.Gameplay.UI.ActionBar;
using DG.Tweening;
using UnityEngine;

namespace _Source.Gameplay
{
    public class GameplayController : IDisposable
    {
        private ActionBarView _actionBarView;
        private FigureSpawner _figureSpawner;
        private GameplayUIRoot _gameplayUIRoot;

        public GameplayController(ActionBarView actionBarView, FigureSpawner figureSpawner, GameplayUIRoot gameplayUIRoot)
        {
            _actionBarView = actionBarView;
            _figureSpawner = figureSpawner;
            _gameplayUIRoot = gameplayUIRoot;
            
            _actionBarView.CreateEmptyCells();
            _figureSpawner.FigureSpawned += OnFigureSpawned;
            _figureSpawner.FiguresDestroyed += OnFiguresDestroyed;
            _figureSpawner.AllFigureSpawned += OnAllFigureSpawned;
            _actionBarView.GameOver += OnGameOver;
            _gameplayUIRoot.Reshuffled += OnReshuffled;
        }

        public void Dispose()
        {
            _figureSpawner.FigureSpawned -= OnFigureSpawned;
            _figureSpawner.FiguresDestroyed -= OnFiguresDestroyed;
            _figureSpawner.AllFigureSpawned -= OnAllFigureSpawned;
            _actionBarView.GameOver -= OnGameOver;
            _gameplayUIRoot.Reshuffled -= OnReshuffled;
        }
        
        private void OnFigureSpawned(Figure figure) => figure.Interactable.Clicked += OnClicked;
        
        private void OnClicked(Figure figure)
        {
            figure.Interactable.Clicked -= OnClicked;
            
            if (_actionBarView.TryGetEmptyCell(out Vector3 targetPosition, out int cellIndex))
            {
                figure.RigidbodySimulated(false);
                
                var sequence = DOTween.Sequence();
                sequence.Append(figure.transform.DOMove(targetPosition, 0.5f).SetEase(Ease.InOutQuad))
                        .Join(figure.transform.DORotate(new Vector3(0f, 0f, 0f), 0.5f))
                        .Append(figure.transform.DOScale(0.9f, 0.2f))
                        .OnComplete(() => _actionBarView.AssignFigure(figure, cellIndex));
            }
        }
        
        private void OnGameOver()
        {
            _gameplayUIRoot.LosePanelSetActive(true);
        }
        
        private void OnFiguresDestroyed()
        {
            _gameplayUIRoot.WinPanelSetActive(true);
        }
        
        private void OnReshuffled()
        {
            int figuresCount = _figureSpawner.CountFigures;
            _actionBarView.ClearCells();
            _figureSpawner.ClearFigures();
            _figureSpawner.SpawnNewFigures(figuresCount);
        }
        
        private void OnAllFigureSpawned()
        {
            _gameplayUIRoot.EnableReshuffleButton();
        }
    }
}