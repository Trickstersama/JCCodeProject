using Features.Gameplay.Domain.ValueObjects;

namespace Features.Gameplay.Tests.Mothers
{
    public static class CoordinateMother{
        public static Coordinate ACoordinate(int x = 0, int y = 0) =>
            new Coordinate
            {
                X = x,
                Y = y
            };
    }
}