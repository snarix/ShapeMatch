using System;
using _Source.Gameplay.FigureSystem.Factory;
using UnityEngine;

namespace _Source.Gameplay.FigureSystem
{
    public class Figure : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private Interactable _interactable;
        [SerializeField] private SpriteRenderer _shapeRenderer;
        [SerializeField] private SpriteRenderer _animalRenderer;
        [SerializeField] private SpriteRenderer _colorRenderer;
        
        public FigureData Data { get; private set; }

        public Interactable Interactable => _interactable;

        public event Action<Figure> OnDestroyed;

        public void Initialize(Sprite shape, Sprite animal, Sprite color)
        {
            Data = new FigureData(shape, color, animal);
            _shapeRenderer.sprite = shape;
            _animalRenderer.sprite = animal;
            _colorRenderer.sprite = color;
        }

        private void OnDestroy() => OnDestroyed?.Invoke(this);

        public void RigidbodySimulated(bool flag) => _rigidbody.simulated = flag;
    }
}