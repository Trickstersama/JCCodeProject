using Features.Gameplay.Domain.ValueObjects;

namespace Features.Gameplay.Domain.Infrastructure
{
    public class MapService : IMapService
    {
        public bool CoordinateIsStart(Coordinate selectedCoordinate, Coordinate startCoordinate) => 
            selectedCoordinate.Equals(startCoordinate);
    }
}