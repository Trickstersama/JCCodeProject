namespace Features.Gameplay.Domain.ValueObjects
{
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