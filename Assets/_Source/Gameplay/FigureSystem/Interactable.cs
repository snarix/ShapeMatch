using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace _Source.Gameplay.FigureSystem
{
    public class Interactable : MonoBehaviour
    {
        public event Action<Figure> Clicked;
        
        private void Update()
        {
            if (Mouse.current.leftButton.wasPressedThisFrame)
                TryHandleClick(Mouse.current.position.ReadValue());
            
            else if (Touchscreen.current != null && Touchscreen.current.primaryTouch.press.wasPressedThisFrame)
                TryHandleClick(Touchscreen.current.primaryTouch.position.ReadValue());
        }
        
        private void TryHandleClick(Vector2 screenPosition)
        {
            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(screenPosition);
            RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);

            if (hit.collider != null && hit.collider.gameObject == gameObject)
            {
                var figure = GetComponent<Figure>();
                Clicked?.Invoke(figure);
            }
        }
    }
}