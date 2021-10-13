using System;
using System.Collections.Generic;
using System.Linq;
using Features.Gameplay.Domain.Infrastructure;
using Features.Gameplay.Domain.ValueObjects;
using NSubstitute;
using NUnit.Framework;
using static Features.Gameplay.Tests.Mothers.CoordinateMother;
using static Features.Gameplay.Tests.Mothers.CreateNodesMother;
using static Features.Gameplay.Tests.Mothers.MapRepositoryMother;
using static Features.Gameplay.Tests.Mothers.MapServiceMother;
using static Features.Gameplay.Tests.Mothers.ValueObjects.MapNodeMother;
using static Features.Gameplay.Tests.Mothers.ValueObjects.MapTileMother;

namespace Features.Gameplay.Tests.Editor.Actions

{
    [TestFixture]
    public class CreateNodesTests
    {
        [Test]
        public void CallLoadNodesFromRepository()
        {
            //Given
            var mapRepository = AMapRepository();
            var startGame = ACreateNodes(withMapRepository: mapRepository);

            //When
            startGame.Do(Enumerable.Empty<MapTile>(), null);

            //Then
            mapRepository.Received(1).LoadNodes(Arg.Any<IEnumerable<MapNode>>());
        }

        [Test] 
        public void CallCreateNodesFromTiles()
        {
            //Given
            var mapService = AMapService();
            var createNodes = ACreateNodes(withMapService: mapService);

            //When
            createNodes.Do(Enumerable.Empty<MapTile>(), null);

            //Then
            mapService.Received(1).CreateNodesFromTiles(Arg.Any<IEnumerable<MapTile>>());
        }
        
        [Test] 
        public void CallSetNodesNeighbours()
        {
            //Given
            var mapService = AMapService();
            var createNodes = ACreateNodes(withMapService: mapService);

            //When
            createNodes.Do(Enumerable.Empty<MapTile>(), null);

            //Then
            mapService.Received(1).SetNodesNeighbours(Arg.Any<IEnumerable<MapNode>>());
        }

        [Test]
        public void SendOnNodesCreated()
        {
            //Given
            var onResetNodes = Substitute.For<IObserver<IEnumerable<MapTile>>>();
            var mapService = AMapService();
            var createNodes = ACreateNodes(withMapService: mapService);

            //When
            createNodes.Do(Enumerable.Empty<MapTile>(), onResetNodes);

            //Then
            onResetNodes.Received(1).OnNext(Arg.Any<IEnumerable<MapTile>>());

            
        }
        [Test] 
        public void CreateOneNode()
        {
            //Given
            var inputTiles = new[] {AMapTile(1, 1, TileType.Desert)}; 
            var mapService = new MapService();
            var mapRepository = new MapRepository();
            var createNodes = ACreateNodes(
                withMapService: mapService,
                withMapRepository: mapRepository
            );

            var expectedNodes = new []{AMapNode(withWeight: GameConstants.desertWeight, withCoordinate: ACoordinate(1,1))};

            //When
            createNodes.Do(inputTiles, null);

            //Then
            Assert.IsTrue(mapRepository.GetNodes.All(expectedNodes.Contains) );
            Assert.IsTrue(mapRepository.GetNodes.Count() == expectedNodes.Count());
        }

        [Test]
        public void Create5Nodes()
        {
            //Given
            var inputTiles = new[]
            {
                AMapTile(1, 1, TileType.Desert),
                AMapTile(2, 1, TileType.Forest),
                AMapTile(3, 1, TileType.Grass),
                AMapTile(4, 1, TileType.Mountain),
                AMapTile(5, 1, TileType.Mountain),
            }; 
            var mapService = new MapService();
            var mapRepository = new MapRepository();
            var createNodes = ACreateNodes(
                withMapService: mapService,
                withMapRepository: mapRepository
            );

            var expectedNodes = new []
            {
                AMapNode(withWeight: GameConstants.desertWeight, withCoordinate: ACoordinate(1,1)),
                AMapNode(withWeight: GameConstants.forestWeight, withCoordinate:ACoordinate(2,1)),
                AMapNode(withWeight: GameConstants.grassWeight, withCoordinate:ACoordinate(3,1)),
                AMapNode(withWeight: GameConstants.mountainWeight, withCoordinate:ACoordinate(4,1)),
                AMapNode(withWeight: GameConstants.mountainWeight, withCoordinate:ACoordinate(5,1))
            };

            //When
            createNodes.Do(inputTiles, null);

            //Then
            Assert.IsTrue(mapRepository.GetNodes.All(expectedNodes.Contains) );
            Assert.IsTrue(mapRepository.GetNodes.Count() == expectedNodes.Count());
        }

        [Test]
        public void LoadOneNodeAsStartWithNoNeighbours()
        {
            //Given
            var inputTiles = new[] {AMapTile(1, 1, TileType.Desert)}; 
            var mapService = new MapService();
            var mapRepository = new MapRepository(withStartCoordinate: ACoordinate(1,1));
            var createNodes = ACreateNodes(
                withMapService: mapService,
                withMapRepository: mapRepository
            );

            //When
            createNodes.Do(inputTiles, null);

            //Then
            Assert.IsTrue(!mapRepository.GetStartNode().Neighbours.Any());
        }
        
        [Test]
        public void LoadOneNodeAsStartWithFullNeighbours()
        {
            //Given
            var inputTiles = new[]
            {
                AMapTile(0, 0, TileType.Desert),
                AMapTile(1, 0, TileType.Grass),
                AMapTile(0, 1, TileType.Grass),
                AMapTile(1, 1, TileType.Mountain),
                AMapTile(2, 1, TileType.Water),
                AMapTile(0, 2, TileType.Desert),
                AMapTile(1, 2, TileType.Desert),
            }; 
            var mapService = new MapService();
            var mapRepository = new MapRepository(withStartCoordinate: ACoordinate(1,1));
            var createNodes = ACreateNodes(
                withMapService: mapService,
                withMapRepository: mapRepository
            );

            var expectedNodes = new []
            {
                AMapNode(withWeight: GameConstants.desertWeight, withCoordinate:ACoordinate(0,0)),
                AMapNode(withWeight: GameConstants.grassWeight, withCoordinate:ACoordinate(1,0)),
                AMapNode(withWeight: GameConstants.waterWeight, withCoordinate:ACoordinate(2,1)),
                AMapNode(withWeight: GameConstants.desertWeight, withCoordinate:ACoordinate(1,2)),
                AMapNode(withWeight: GameConstants.desertWeight, withCoordinate:ACoordinate(0,2)),
                AMapNode(withWeight: GameConstants.grassWeight, withCoordinate:ACoordinate(0,1)),
            };

            //When
            createNodes.Do(inputTiles, null);

            //Then
            Assert.IsTrue(mapRepository.GetStartNode().Neighbours.All(expectedNodes.Contains));
            Assert.IsTrue(mapRepository.GetStartNode().Neighbours.Count() == expectedNodes.Length);
        }
        
        [Test]
        public void LoadOneNodeAsStartWithFull3Neighbours()
        {
            //Given
            var inputTiles = new[]
            {
                AMapTile(0, 0, TileType.Desert),
                AMapTile(1, 0, TileType.Grass),
                AMapTile(0, 1, TileType.Grass),
                AMapTile(1, 1, TileType.Mountain),
                AMapTile(2, 1, TileType.Water),
                AMapTile(0, 2, TileType.Desert),
                AMapTile(1, 2, TileType.Desert),
            }; 
            var mapService = new MapService();
            var mapRepository = new MapRepository(withStartCoordinate: ACoordinate());
            var createNodes = ACreateNodes(
                withMapService: mapService,
                withMapRepository: mapRepository
            );

            var expectedNodes = new []
            {
                AMapNode(withWeight: GameConstants.grassWeight, withCoordinate:ACoordinate(1,0)),
                AMapNode(withWeight: GameConstants.grassWeight, withCoordinate:ACoordinate(0,1)),
                AMapNode(withWeight: GameConstants.mountainWeight, withCoordinate:ACoordinate(1,1)),
            };

            //When
            createNodes.Do(inputTiles, null);

            //Then
            Assert.IsTrue(mapRepository.GetStartNode().Neighbours.All(expectedNodes.Contains));
            Assert.IsTrue(mapRepository.GetStartNode().Neighbours.Count() == expectedNodes.Length);
        }
        
    }
}