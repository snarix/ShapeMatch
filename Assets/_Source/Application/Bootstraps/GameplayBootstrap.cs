using System;
using _Source.Gameplay.UI;
using UnityEngine.SceneManagement;
using Zenject;

namespace _Source.Application.Bootstraps
{
    public class GameplayBootstrap : IInitializable, IDisposable
    {
        private GameplayUIRoot _gameplayUIRoot;

        public GameplayBootstrap(GameplayUIRoot gameplayUIRoot)
        {
            _gameplayUIRoot = gameplayUIRoot;
        }

        public void Initialize()
        {
            _gameplayUIRoot.Won += OnWon;
            _gameplayUIRoot.Lost += OnLost;
        }
        
        public void Dispose()
        {
            _gameplayUIRoot.Won -= OnWon;
            _gameplayUIRoot.Lost -= OnLost;
        }
        
        private void OnWon()
        {
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); 
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
            
        private void OnLost()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}