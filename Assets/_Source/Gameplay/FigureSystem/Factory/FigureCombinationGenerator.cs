using System.Collections.Generic;
using _Source.Gameplay.FigureSystem.Configs;
using _Source.Gameplay.GameRules;
using UnityEngine;

namespace _Source.Gameplay.FigureSystem.Factory
{
    public class FigureCombinationGenerator
    {
        private GameConfig _config;
        private Rules _rules;

        public FigureCombinationGenerator(GameConfig config, Rules rules)
        {
            _config = config;
            _rules = rules;
        }

        public List<FigureData> GenerateFieldCombinations()
        {
            int maxUniqueFigures = _rules.MaxFiguresOnField / _rules.MatchCountFiguresInCells;
            int possibleCombinations = _config.ShapeData.Shapes.Count * _config.ColorData.Colors.Count * _config.AnimalData.Animals.Count;
            maxUniqueFigures = Mathf.Min(maxUniqueFigures, possibleCombinations);
            
            List<FigureData> uniqueFigures = GenerateUniqueFigures(maxUniqueFigures);
            
            List<FigureData> fieldFigures = new List<FigureData>();
            
            foreach (var figure in uniqueFigures)
                for (int i = 0; i < _rules.MatchCountFiguresInCells; i++)
                    fieldFigures.Add(figure);
            
            Shuffle(fieldFigures);

            return fieldFigures;
        }
        
        public List<FigureData> GenerateCombinations(int totalFigures)
        {
            int maxUniqueFigures = Mathf.CeilToInt((float)totalFigures / _rules.MatchCountFiguresInCells);
            List<FigureData> uniqueFigures = GenerateUniqueFigures(maxUniqueFigures);
    
            List<FigureData> fieldFigures = new List<FigureData>();
            int addedFigures = 0;
    
            foreach (var figure in uniqueFigures)
            {
                for (int i = 0; i < _rules.MatchCountFiguresInCells && addedFigures < totalFigures; i++)
                {
                    fieldFigures.Add(figure);
                    addedFigures++;
                }
            }
    
            Shuffle(fieldFigures);
            return fieldFigures;
        }
        
        private List<FigureData> GenerateUniqueFigures(int maxUniqueFigures)
        {
            List<FigureData> uniqueFigures = new List<FigureData>();
            HashSet<string> usedCombinations = new HashSet<string>();

            while (uniqueFigures.Count < maxUniqueFigures)
            {
                Sprite shape = _config.ShapeData.Shapes[Random.Range(0, _config.ShapeData.Shapes.Count)];
                Sprite color = _config.ColorData.Colors[Random.Range(0, _config.ColorData.Colors.Count)];
                Sprite animal = _config.AnimalData.Animals[Random.Range(0, _config.AnimalData.Animals.Count)];

                string combinationKey = $"{shape.name}_{color.name}_{animal.name}";
                if (!usedCombinations.Contains(combinationKey))
                {
                    uniqueFigures.Add(new FigureData(shape, color, animal));
                    usedCombinations.Add(combinationKey);
                }
            }

            return uniqueFigures;
        }

        private void Shuffle(List<FigureData> list)
        {
            for (int i = list.Count - 1; i > 0; i--)
            {
                int j = Random.Range(0, i + 1);
                (list[i], list[j]) = (list[j], list[i]);
            }
        }
    }
}