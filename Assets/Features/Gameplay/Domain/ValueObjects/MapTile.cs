using System;

namespace Features.Gameplay.Domain.ValueObjects
{
    [Serializable]
    public struct MapTile
    {
        public Coordinate coordinate;
        public TileType TileType;
    }

    public enum TileType
    {
        Grass, Forest, Desert, Mountain, Water
    }

    [Serializable]
    public struct Coordinate
    {
        public int X;
        public int Y;
        
        public override string ToString()
        {
            return $"Coordinate: X = {X}, Y = {Y}";
        }
    }

    public struct WorldCoordinate
    {
        public float X;
        public float Y;

        public override string ToString()
        {
            return $"World Coordinate: X = {X}, Y = {Y}";
        }
    }
    
}