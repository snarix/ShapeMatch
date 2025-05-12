using System.Collections.Generic;
using UnityEngine;

namespace _Source.Gameplay.FigureSystem.Configs
{
    [CreateAssetMenu(fileName = "ScriptableObject/Data/ShapeData", menuName = "ShapeData")]
    public class ShapeData : ScriptableObject
    {
        [field: SerializeField] public List<Sprite> Shapes { get; private set; }
    }
}