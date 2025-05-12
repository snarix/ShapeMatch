using System.Collections.Generic;
using UnityEngine;

namespace _Source.Gameplay.FigureSystem.Configs
{
    [CreateAssetMenu(fileName = "ScriptableObject/Data/AnimalData", menuName = "AnimalData")]
    public class AnimalData : ScriptableObject
    {
        [field: SerializeField] public List<Sprite> Animals { get; private set; }
    }
}