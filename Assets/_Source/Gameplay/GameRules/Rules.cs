using System.Collections.Generic;
using System.Linq;
using _Source.Gameplay.FigureSystem.Factory;
using UnityEngine;

namespace _Source.Gameplay.GameRules
{
    [CreateAssetMenu(fileName = "ScriptableObject/Rules", menuName = "Rules")]
    public class Rules : ScriptableObject
    {
        [field: SerializeField] public int MaxFiguresOnField { get; private set; }
        [field: SerializeField] public int MaxFillCellsInActionBar { get; private set; }
        [field: SerializeField] public int MatchCountFiguresInCells { get; private set; }
        
        public List<int> CheckMatches(FigureData[] cellFigures)
        {
            var figureGroups = new Dictionary<FigureData, List<int>>();

            for (int i = 0; i < cellFigures.Length; i++)
            {
                if (cellFigures[i] != null)
                {
                    var data = cellFigures[i];
                    if(!figureGroups.ContainsKey(data))
                        figureGroups[data] = new List<int>();
                    figureGroups[data].Add(i);
                }
            }

            foreach (var group in figureGroups)
            {
                if (group.Value.Count >= MatchCountFiguresInCells)
                {
                    return group.Value;
                }
            }
            
            return new List<int>();
        }
    }
}