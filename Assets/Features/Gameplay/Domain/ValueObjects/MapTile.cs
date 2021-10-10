using System;

namespace Features.Gameplay.Domain.ValueObjects
{
    [Serializable]
    public struct MapTile
    {
        public Coordinate coordinate;
        public TileType TileType;
    }
}