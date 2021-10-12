using System.Collections.Generic;
using Features.Gameplay.Domain.ValueObjects;

namespace Features.Gameplay.Domain.Infrastructure
{
    public interface IMapService
    {
        bool CoordinateIsStart(Coordinate selectedCoordinate, Coordinate startCoordinate);
        IEnumerable<MapNode> CreateNodesFromTiles(IEnumerable<MapTile> tiles);
        IEnumerable<MapNode> SetNodesNeighbours(IEnumerable<MapNode> nodes);
    }
}