using Features.Gameplay.Domain.ValueObjects;
using Features.Gameplay.Infrastructure;

namespace Features.Gameplay.Domain.Infrastructure
{
    public class CoordinatesService : ICoordinateService
    {
        public WorldCoordinate ToWorldPosition(Coordinate coordinate)
        {
            if (coordinate.Y % 2 == 0)
            {
                return new WorldCoordinate
                {
                    X = coordinate.X,
                    Y = coordinate.Y * .75f
                };
            }

            return new WorldCoordinate
            {
                X = coordinate.X - .5f,
                Y = coordinate.Y * .75f
            };
        }
    }
}