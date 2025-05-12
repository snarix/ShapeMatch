using UnityEngine;

namespace _Source.Gameplay.FigureSystem.Factory.Abstractions
{
    public interface IFigureFactory
    {
        Figure CreateFigure(Figure figure, Vector3 position);
    }
}