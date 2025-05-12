using System;
using System.Collections.Generic;
using System.Linq;
using _Source.Gameplay.FigureSystem;
using _Source.Gameplay.FigureSystem.Factory;
using _Source.Gameplay.GameRules;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using Zenject;

namespace _Source.Gameplay.UI.ActionBar
{
    public class ActionBarView : MonoBehaviour
    {
        [SerializeField] private GameObject _cellPrefab;
        [SerializeField] private Transform _parent;
        
        private Rules _rules;
        private List<GameObject> _emptyCell = new();
        
        private Figure[] _figures;
        private bool[] _isCellFilled;
        
        private int MaxCells => _rules.MaxFillCellsInActionBar;

        public event Action GameOver;
        
        [Inject]
        public void Construct(Rules rules)
        {
            _rules = rules;
            _isCellFilled = new bool[MaxCells];
            _figures = new Figure[MaxCells];
        }
        
        public void CreateEmptyCells()
        {
            for (int i = 0; i < MaxCells; i++)
            {
                var cell = Instantiate(_cellPrefab, _parent);
                _emptyCell.Add(cell);
                _isCellFilled[i] = false;
                _figures[i] = null;
            }
        }
        
        public void ClearCells()
        {
            for (int i = 0; i < MaxCells; i++)
            {
                if (_figures[i] != null)
                {
                    _figures[i].transform.DOKill();
                    Destroy(_figures[i].gameObject);
                    _figures[i] = null;
                    _isCellFilled[i] = false;
                }
            }
        }
        
        public bool TryGetEmptyCell(out Vector3 position, out int cellIndex)
        {
            for (int i = 0; i < _emptyCell.Count; i++)
            {
                if (_isCellFilled[i] == false) 
                {
                    _isCellFilled[i] = true;
                    position = _emptyCell[i].transform.position;
                    cellIndex = i;
                    return true;
                }
            }
            position = Vector3.zero;
            cellIndex = -1;
            return false;
        }
        
        public void AssignFigure(Figure figure, int cellIndex)
        {
            if (cellIndex >= 0 && cellIndex < MaxCells)
            {
                _figures[cellIndex] = figure;
                CheckMatches();
            }
        }

        private void CheckMatches()
        {
            var figureData = new FigureData[MaxCells];
            for (int i = 0; i < MaxCells; i++)
                figureData[i] = _figures[i]?.Data;
            
            var matchedIndices = _rules.CheckMatches(figureData);
            if (matchedIndices.Count == _rules.MatchCountFiguresInCells)
                RemoveFigures(matchedIndices);
            else if (_isCellFilled.All(cell => cell) && matchedIndices.Count == 0)
                GameOver?.Invoke();
        }
        
        private void RemoveFigures(List<int> indices)
        {
            foreach (var index in indices)
            {
                if (_figures[index] != null)
                {
                    var figurePrefab = _figures[index];
                    
                    var sequence = DOTween.Sequence();
                    sequence.Append(figurePrefab.transform.DOScale(0f, 0.2f)).
                        OnComplete(() => Destroy(figurePrefab.gameObject));
                    
                    _figures[index] = null;
                    _isCellFilled[index] = false;
                }
            }
        }
    }
}