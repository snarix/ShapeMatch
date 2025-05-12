using _Source.Gameplay.FigureSystem.Factory.Abstractions;
using UnityEngine;

namespace _Source.Gameplay.FigureSystem.Factory
{
    public class FigureFactory : IFigureFactory
    {
        public Figure CreateFigure(Figure figurePrefab, Vector3 position) => Object.Instantiate(figurePrefab, position, Quaternion.identity);
    }
}