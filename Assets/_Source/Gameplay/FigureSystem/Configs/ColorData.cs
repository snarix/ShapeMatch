using System.Collections.Generic;
using UnityEngine;

namespace _Source.Gameplay.FigureSystem.Configs
{
    [CreateAssetMenu(fileName = "ScriptableObject/Data/ColorData", menuName = "ColorData")]
    public class ColorData : ScriptableObject
    {
        [field: SerializeField] public List<Sprite> Colors { get; private set; }
    }
}