using Features.Gameplay.Domain.ValueObjects;
using PathFinding;

namespace Features.Gameplay.Tests.Mothers.ValueObjects
{
    public static class MapNodeMother
    {
        public static IAStarNode AMapNode(
            int withWeight = 0,
            Coordinate? withCoordinate = null
        ) =>
            new MapNode(
                withCoordinate ?? new Coordinate(),
                withWeight
            );
    }
}