using System;

namespace Features.Gameplay.Domain.ValueObjects
{
    [Serializable]
    public struct MapTile
    {
        public Coordinate coordinate;
        public TileType TileType;
        
        public override string ToString()
        {
            return $"Coordinate: = {coordinate}, Tile type = {TileType}";
        }
    }
}