using Features.Gameplay.Domain.ValueObjects;

namespace Features.Gameplay.Tests.Mothers.ValueObjects
{
    public static class MapTileMother
    {
        public static MapTile AMapTile(
            int withXCoordinate = 0,
            int withYCoordinate = 0,
            TileType? withTileType = null
        ) {
            return new MapTile
            {
                TileType = withTileType ?? TileType.Desert,
                coordinate = new Coordinate
                {
                    X = withXCoordinate,
                    Y = withYCoordinate
                }
            };
        }
    }
}