using UnityEngine;

namespace _Source.Gameplay.FigureSystem.Configs
{
    [CreateAssetMenu(fileName = "ScriptableObject/Config/GameConfig", menuName = "GameConfig")]
    public class GameConfig : ScriptableObject
    {
        [field: SerializeField] public ShapeData ShapeData { get; private set; }
        [field: SerializeField] public ColorData ColorData { get; private set; }
        [field: SerializeField] public AnimalData AnimalData { get; private set; }
    }
}