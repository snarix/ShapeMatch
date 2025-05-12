using System;
using UnityEngine;

namespace _Source.Gameplay.FigureSystem.Factory
{
    [Serializable]
    public class FigureData
    {
        public Sprite Shape { get; }
        public Sprite Color { get; }
        public Sprite Animal { get; }

        public FigureData(Sprite shape, Sprite color, Sprite animal)
        {
            Shape = shape;
            Color = color;
            Animal = animal;
        }
        
        public override bool Equals(object obj)
        {
            if (obj is FigureData other)
            {
                return Shape == other.Shape && Color == other.Color && Animal == other.Animal;
            }
            return false;
        }

        public override int GetHashCode()
        {
            int hash = HashCode.Combine(Shape, Color, Animal);
            return hash;
        }
    }
}