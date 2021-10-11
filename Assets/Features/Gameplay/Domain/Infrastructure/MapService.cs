using System.Collections.Generic;
using Features.Gameplay.Domain.ValueObjects;
using PathFinding;

namespace Features.Gameplay.Domain.Infrastructure
{
    public class MapService : IMapService
    {
        public bool CoordinateIsStart(Coordinate selectedCoordinate, Coordinate startCoordinate) => 
            selectedCoordinate.Equals(startCoordinate);

        public Dictionary<Coordinate, IAStarNode> CreateNodesFromTiles(IEnumerable<MapTile> tiles)
        {
            var nodes = new Dictionary<Coordinate, IAStarNode>();
            foreach (var tile in tiles)
            {
                var newNode = new MapNode();
                nodes.Add(tile.coordinate, newNode);
            }
            return nodes;
        }

        public IEnumerable<IAStarNode> SetNodesNeighbours(Dictionary<Coordinate, IAStarNode> nodes)
        {
            var xxx = new List<IAStarNode>();
            foreach (var kvpNode in nodes)
            {
            }

            return xxx;
        }

        int SelectWeightByType(TileType type)
        {
            return type switch
            {
                TileType.Grass => 1,
                TileType.Forest => 3,
                TileType.Desert => 5,
                TileType.Mountain => 10,
                TileType.Water => 0,
                _ => 0
            };
        }
    }
}