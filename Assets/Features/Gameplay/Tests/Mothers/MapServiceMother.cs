using System.Collections.Generic;
using System.Linq;
using Features.Gameplay.Domain.Infrastructure;
using Features.Gameplay.Domain.ValueObjects;
using NSubstitute;

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
                .Returns(Enumerable.Empty<MapNode>());
            service.SetNodesNeighbours(Arg.Any<IEnumerable<MapNode>>())
                .Returns(Enumerable.Empty<MapNode>());
            return service;
        }
    }
}