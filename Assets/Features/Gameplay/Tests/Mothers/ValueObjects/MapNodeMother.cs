using Features.Gameplay.Domain.ValueObjects;

namespace Features.Gameplay.Tests.Mothers.ValueObjects
{
    public static class MapNodeMother
    {
        public static MapNode AMapNode(
            int withWeight = 0,
            Coordinate? withCoordinate = null
        ) =>
            new MapNode(
                withCoordinate ?? new Coordinate(),
                withWeight
            );
    }
}