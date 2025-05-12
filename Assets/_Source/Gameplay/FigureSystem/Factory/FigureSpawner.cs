using System;
using System.Collections;
using System.Collections.Generic;
using _Source.Gameplay.FigureSystem.Factory.Abstractions;
using _Source.Include;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace _Source.Gameplay.FigureSystem.Factory
{
    public class FigureSpawner : IInitializable
    {
        private Figure _figurePrefab;
        private IFigureFactory _figureFactory;
        private SpawnData _spawnData;
        private FigureCombinationGenerator _generator;
        private ICoroutineHandler _coroutineHandler;
        
        private List<FigureData> _fieldFigures;
        private List<Figure> _figures = new();
        
        public int CountFigures => _figures.Count;
        
        public event Action<Figure> FigureSpawned;
        public event Action AllFigureSpawned;
        public event Action FiguresDestroyed;
        
        public FigureSpawner(Figure figurePrefab, IFigureFactory figureFactory, SpawnData spawnData, FigureCombinationGenerator generator, ICoroutineHandler coroutineHandler)
        {
            _figurePrefab = figurePrefab;
            _figureFactory = figureFactory;
            _spawnData = spawnData;
            _coroutineHandler = coroutineHandler;
            _generator = generator;
        }
        
        public void Initialize()
        {
            _fieldFigures = _generator.GenerateFieldCombinations();
            _coroutineHandler.StartCoroutine(SpawnFigures());
        }
    
        public void SpawnNewFigures(int count)
        {
            _fieldFigures = _generator.GenerateCombinations(count);
            _coroutineHandler.StartCoroutine(SpawnFigures());
        }
        
        public void ClearFigures()
        {
            foreach (var figure in _figures)
            {
                if (figure != null)
                {
                    figure.OnDestroyed -= OnDestroyed;
                    Object.Destroy(figure.gameObject);
                }
            }
            _figures.Clear();
        }
        
        private IEnumerator SpawnFigures()
        {
            foreach (var figureData in _fieldFigures)
            {
                var spawnPoint = _spawnData.SpawnPoints[Random.Range(0, _spawnData.SpawnPoints.Count)];
                var figure = _figureFactory.CreateFigure(_figurePrefab, spawnPoint.position);
                _figures.Add(figure);
                figure.Initialize(figureData.Shape, figureData.Animal, figureData.Color);
                
                FigureSpawned?.Invoke(figure);
                figure.OnDestroyed += OnDestroyed;
                yield return new WaitForSeconds(_spawnData.TimeBetweenSpawn);
            }
            AllFigureSpawned?.Invoke();
        }

        private void OnDestroyed(Figure figure)
        {
            figure.OnDestroyed -= OnDestroyed;
            _figures.Remove(figure);

            if (CountFigures == 0)
                FiguresDestroyed?.Invoke();
        }
    }
}