using _Source.Application.Bootstraps;
using _Source.Gameplay;
using _Source.Gameplay.FigureSystem;
using _Source.Gameplay.FigureSystem.Configs;
using _Source.Gameplay.FigureSystem.Factory;
using _Source.Gameplay.FigureSystem.Factory.Abstractions;
using _Source.Gameplay.GameRules;
using _Source.Gameplay.UI;
using _Source.Gameplay.UI.ActionBar;
using _Source.Include;
using UnityEngine;
using Zenject;

namespace _Source.Application.Installers
{
    public class GameplayInstaller : MonoInstaller
    {
        [SerializeField] private GameConfig _gameConfig;
        [SerializeField] private SpawnData _spawnData;
        [SerializeField] private Figure _figurePrefab;
        [SerializeField] private ActionBarView _actionBar;
        [SerializeField] private Rules _rules;
        [SerializeField] private GameplayUIRoot _uiRoot;
        
        public override void InstallBindings()
        {
            Container.Bind<Camera>().FromInstance(Camera.main).AsSingle();
            Container.Bind<ICoroutineHandler>().To<MonoBehaviourCoroutineHandler>().AsSingle().WithArguments(this);
            
            Container.BindInterfacesAndSelfTo<GameplayBootstrap>().AsSingle().NonLazy();
            Container.BindInstance(_uiRoot).AsSingle();
            
            Container.BindInstance(_gameConfig).AsSingle();
            Container.BindInstance(_spawnData).AsSingle();
            Container.BindInstance(_figurePrefab).AsSingle();
            Container.BindInstance(_rules).AsSingle();
            
            Container.Bind<IFigureFactory>().To<FigureFactory>().AsSingle();
            Container.Bind<FigureCombinationGenerator>().AsSingle();
            Container.BindInterfacesAndSelfTo<FigureSpawner>().AsSingle().NonLazy();
            
            Container.BindInstance(_actionBar).AsSingle();
            Container.BindInterfacesAndSelfTo<GameplayController>().AsSingle();
        }
    }
}