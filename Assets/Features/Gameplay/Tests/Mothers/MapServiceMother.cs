using System.Collections.Generic;
using System.Linq;
using Features.Gameplay.Domain.Infrastructure;
using Features.Gameplay.Domain.ValueObjects;
using NSubstitute;
using PathFinding;

namespace Features.Gameplay.Tests.Mothers
{
    public static class MapServiceMother
    {
        public static IMapService AMapService(
            bool withCoordinateIsStart = true
        ) {
            var service = Substitute.For<IMapService>();
            service.CoordinateIsStart(Arg.Any<Coordinate>(), Arg.Any<Coordinate>())
                .Returns(withCoordinateIsStart);
            service.CreateNodesFromTiles(Arg.Any<IEnumerable<MapTile>>())
                .Returns(new Dictionary<Coordinate, IAStarNode>());
            service.SetNodesNeighbours(Arg.Any<Dictionary<Coordinate, IAStarNode>>())
                .Returns(Enumerable.Empty<IAStarNode>());
            return service;
        }
    }
}