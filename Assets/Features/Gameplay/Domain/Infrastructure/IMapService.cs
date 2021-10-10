using Features.Gameplay.Domain.ValueObjects;

namespace Features.Gameplay.Domain.Infrastructure
{
    public interface IMapService
    {
        bool CoordinateIsStart(Coordinate selectedCoordinate, Coordinate startCoordinate);
    }
}