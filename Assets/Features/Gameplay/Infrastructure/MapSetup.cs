using System.Collections.Generic;
using Features.Gameplay.Domain.ValueObjects;
using UnityEngine;

namespace Features.Gameplay.Infrastructure
{
    [CreateAssetMenu(fileName = "MapSetup", menuName = "MapSetup")]
    public class MapSetup : ScriptableObject
    {
        [SerializeField] List<MapTile> tiles;

        public IEnumerable<MapTile> ReadAllTiles()
        {
            return tiles;
        }
    }
}