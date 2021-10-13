using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Features.Gameplay.Domain.Infrastructure;
using Features.Gameplay.Domain.ValueObjects;
using NSubstitute;
using NUnit.Framework;
using PathFinding;
using static Features.Gameplay.Tests.Mothers.CalculatePathMother;
using static Features.Gameplay.Tests.Mothers.CoordinateMother;
using static Features.Gameplay.Tests.Mothers.MapRepositoryMother;
using static Features.Gameplay.Tests.Mothers.PathFindingServiceMother;
using static Features.Gameplay.Tests.Mothers.ValueObjects.MapTileMother;

namespace Features.Gameplay.Tests.Editor.Reactions
{
    [TestFixture]
    public class CalculatePathTests
    {
        [Test]
        public void SendOnPathCalculated()
        {
            //Given
            var onPathCalculated = Substitute.For<IObserver<IEnumerable<Coordinate>>>();
            var pathfindingService = APathfindingService();
            var calculatePath = ACalculatePath(withPathfindingService: pathfindingService);
            
            //When
            calculatePath.Do(onPathCalculated);
            
            //Then
            onPathCalculated.Received(1).OnNext(Arg.Any<IEnumerable<Coordinate>>());
        }
        
        [Test]
        public void CallCalculatePathOnService()
        {
            //Given
            var pathfindingService = Substitute.For<IPathfindingService>();
            var calculatePath = ACalculatePath(withPathfindingService: pathfindingService);
            
            //When
            calculatePath.Do(null);
            
            //Then
            pathfindingService.Received(1).CalculatePath(Arg.Any<IAStarNode>(), Arg.Any<IAStarNode>());
        }
        
        [Test]
        public void CallGetStartNodeFromRepository()
        {
            //Given
            var mapRepository = AMapRepository();
            var calculatePath = ACalculatePath(withMapRepository: mapRepository);
            
            //When
            calculatePath.Do(null);
            
            //Then
            mapRepository.Received(1).GetStartNode();
        }
        
        [Test]
        public void CallGetGoalNodeFromRepository()
        {
            //Given
            var mapRepository = AMapRepository();
            var calculatePath = ACalculatePath(withMapRepository: mapRepository);
            
            //When
            calculatePath.Do(null);
            
            //Then
            mapRepository.Received(1).GetGoalNode();
        }
        [Test]
        public void TravelToNeighbour()
        {
            //Given
            var startCoordinate = ACoordinate(0, 0);
            var goalCoordinate = ACoordinate(1, 0);
            var onPathCalculated = Substitute.For<IObserver<IEnumerable<Coordinate>>>();
            
            var mapRepository = new MapRepository(
                withStartCoordinate: startCoordinate,
                withGoalCoordinate: goalCoordinate,
                withNodes: SomeNodes()
            );
            var pathfindingService = new PathFindingService();
            var calculatePath = ACalculatePath(
                withMapRepository: mapRepository,
                withPathfindingService: pathfindingService
                );
            
            var expectedPath = new List<Coordinate>()
            {
                ACoordinate(0, 0),
                ACoordinate(1, 0),
            };

            //When
            calculatePath.Do(onPathCalculated: onPathCalculated);
            
            //Then
            onPathCalculated.Received(1).OnNext(Arg.Is<IEnumerable<Coordinate>>(actual => expectedPath.SequenceEqual(actual)));
        }
        
        [Test]
        public void TravelTTheSide()
        {
            //Given
            var startCoordinate = ACoordinate(0, 0);
            var goalCoordinate = ACoordinate(4, 0);
            var onPathCalculated = Substitute.For<IObserver<IEnumerable<Coordinate>>>();
            
            var mapRepository = new MapRepository(
                withStartCoordinate: startCoordinate,
                withGoalCoordinate: goalCoordinate,
                withNodes: SomeNodes()
            );
            var pathfindingService = new PathFindingService();
            var calculatePath = ACalculatePath(
                withMapRepository: mapRepository,
                withPathfindingService: pathfindingService
            );

            var expectedPath = new List<Coordinate>()
            {
                ACoordinate(0, 0),
                ACoordinate(1, 0),
                ACoordinate(2, 0),
                ACoordinate(3, 0),
                ACoordinate(4, 0),
            };
            
            //When
            calculatePath.Do(onPathCalculated: onPathCalculated);
            
            //Then
            onPathCalculated.Received(1).OnNext(Arg.Is<IEnumerable<Coordinate>>(actual => expectedPath.SequenceEqual(actual)));
        }
        
        [Test]
        public void TravelToTop()
        {
            //Given
            var startCoordinate = ACoordinate(0, 0);
            var goalCoordinate = ACoordinate(3, 4);
            var onPathCalculated = Substitute.For<IObserver<IEnumerable<Coordinate>>>();
            
            var mapRepository = new MapRepository(
                withStartCoordinate: startCoordinate,
                withGoalCoordinate: goalCoordinate,
                withNodes: SomeNodes()
            );
            var pathfindingService = new PathFindingService();
            var calculatePath = ACalculatePath(
                withMapRepository: mapRepository,
                withPathfindingService: pathfindingService
            );

            var expectedPath = new List<Coordinate>()
            {
                ACoordinate(0, 0),
                ACoordinate(0, 1),
                ACoordinate(0, 2),
                ACoordinate(1, 2),
                ACoordinate(2, 3),
                ACoordinate(3, 3),
                ACoordinate(3, 4),
            };
            
            //When
            calculatePath.Do(onPathCalculated: onPathCalculated);
            
            //Then
            onPathCalculated.Received(1).OnNext(Arg.Is<IEnumerable<Coordinate>>(actual => expectedPath.SequenceEqual(actual)));
        }

        Dictionary<Coordinate, MapNode> SomeNodes()
        {
            var rawNodes = new[]
            {
                AMapTile(0, 4, TileType.Forest), AMapTile(1, 4, TileType.Grass), AMapTile(2, 4, TileType.Mountain), AMapTile(3, 4, TileType.Forest), AMapTile(4, 4, TileType.Water),
                AMapTile(0, 3, TileType.Forest), AMapTile(1, 3, TileType.Desert), AMapTile(2, 3, TileType.Grass), AMapTile(3, 3, TileType.Grass), AMapTile(4, 3, TileType.Forest),
                AMapTile(0, 2, TileType.Grass), AMapTile(1, 2, TileType.Grass), AMapTile(2, 2, TileType.Water), AMapTile(3, 2, TileType.Mountain), AMapTile(4, 2, TileType.Desert),
                AMapTile(0, 1, TileType.Grass), AMapTile(1, 1, TileType.Water), AMapTile(2, 1, TileType.Forest), AMapTile(3, 1, TileType.Water), AMapTile(4, 1, TileType.Grass),
                AMapTile(0, 0, TileType.Grass), AMapTile(1, 0, TileType.Grass), AMapTile(2, 0, TileType.Desert), AMapTile(3, 0, TileType.Grass), AMapTile(4, 0, TileType.Mountain),
            };

            var mapService = new MapService();
            var nodesFromTiles = mapService.CreateNodesFromTiles(rawNodes);
            var nodesWithNeighbours = mapService.SetNodesNeighbours(nodesFromTiles);
            var nodeSetup = new Dictionary<Coordinate, MapNode>();
            foreach (var node in nodesWithNeighbours)
            {
                nodeSetup.Add(node.Coordinate(), node);
            }
            return nodeSetup;
        }
    }
}