using System.Collections.Generic;
using Features.Gameplay.Domain.ValueObjects;
using PathFinding;

namespace Features.Gameplay.Domain.Infrastructure
{
    public interface IMapService
    {
        bool CoordinateIsStart(Coordinate selectedCoordinate, Coordinate startCoordinate);
        Dictionary<Coordinate, IAStarNode> CreateNodesFromTiles(IEnumerable<MapTile> tiles);
        IEnumerable<IAStarNode> SetNodesNeighbours(Dictionary<Coordinate, IAStarNode> nodes);
    }
}