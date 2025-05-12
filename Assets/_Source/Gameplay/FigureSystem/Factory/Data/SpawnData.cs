using System;
using System.Collections.Generic;
using UnityEngine;

namespace _Source.Gameplay.FigureSystem.Factory
{
    [Serializable]
    public class SpawnData
    {
        [SerializeField] private List<Transform> _spawnPoints;
        [SerializeField] private float _timeBetweenSpawn;

        public List<Transform> SpawnPoints => _spawnPoints;

        public float TimeBetweenSpawn => _timeBetweenSpawn;
    }
}