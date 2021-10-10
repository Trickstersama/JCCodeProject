using Features.Gameplay.Domain.ValueObjects;

namespace Features.Gameplay.Infrastructure
{
    public interface ICoordinateService
    {
        WorldCoordinate ToWorldPosition(Coordinate coordinate);
    }
}