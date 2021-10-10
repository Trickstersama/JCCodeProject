using System;

namespace Features.Gameplay.Domain.ValueObjects
{
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
}