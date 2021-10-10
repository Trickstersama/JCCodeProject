using Features.Gameplay.Domain.ValueObjects;

namespace Features.Gameplay.Tests.Mothers
{
    public static class WorldCoordinateMother{
        public static WorldCoordinate AWorldCoordinate(float x = 0, float y = 0) =>
            new WorldCoordinate
            {
                X = x,
                Y = y
            };
    }
}