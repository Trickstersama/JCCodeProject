using Features.Gameplay.Domain.ValueObjects;

namespace Features.Gameplay.Domain.Infrastructure
{
    public interface IMapService
    {
        bool StartIsNotSelected();
        bool CoordinateIsStart(Coordinate any);
    }
}