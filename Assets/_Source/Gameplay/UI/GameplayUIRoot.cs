using System;
using UnityEngine;
using UnityEngine.UI;

namespace _Source.Gameplay.UI
{
    public class GameplayUIRoot : MonoBehaviour
    {
        [SerializeField] private GameObject _winPanel;
        [SerializeField] private GameObject _losePanel;
        
        [SerializeField] private Button _winButton;
        [SerializeField] private Button _loseButton;
        
        [SerializeField] private Button _reshuffleButton;
        
        public event Action Won;
        public event Action Lost;
        public event Action Reshuffled;
        
        private void OnEnable()
        {
            _winButton.onClick.AddListener(Win);
            _loseButton.onClick.AddListener(Lose);
            _reshuffleButton.onClick.AddListener(Reshuffle);
        }

        private void OnDisable()
        {
            _winButton.onClick.RemoveListener(Win);
            _loseButton.onClick.RemoveListener(Lose);
            _reshuffleButton.onClick.RemoveListener(Reshuffle);
        }

        public void WinPanelSetActive(bool flag)
        {
            _winPanel.SetActive(flag);
            _losePanel.SetActive(!flag);
        }
        
        public void LosePanelSetActive(bool flag)
        {
            _losePanel.SetActive(flag);
            _winPanel.SetActive(!flag);
        }
        
        private void Win()
        {
            Won?.Invoke();
        }
        
        private void Lose()
        {
            Lost?.Invoke();
        }
        
        private void Reshuffle()
        {
            _reshuffleButton.interactable = false;
            Reshuffled?.Invoke();
        }

        public void EnableReshuffleButton()
        {
            _reshuffleButton.interactable = true;
        }
    }
}