using System.Collections.Generic;
using System.Linq;
using Features.Gameplay.Domain.Infrastructure;
using Features.Gameplay.Domain.ValueObjects;
using NSubstitute;
using NUnit.Framework;
using PathFinding;
using static Features.Gameplay.Tests.Mothers.CreateNodesMother;
using static Features.Gameplay.Tests.Mothers.MapRepositoryMother;
using static Features.Gameplay.Tests.Mothers.MapServiceMother;
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
            startGame.Do(Enumerable.Empty<MapTile>());

            //Then
            mapRepository.Received(1).LoadNodes(Arg.Any<IEnumerable<IAStarNode>>());
        }

        [Test] 
        public void CallCreateNodesFromTiles()
        {
            //Given
            var mapService = AMapService();
            var startGame = ACreateNodes(withMapService: mapService);

            //When
            startGame.Do(Enumerable.Empty<MapTile>());

            //Then
            mapService.Received(1).CreateNodesFromTiles(Arg.Any<IEnumerable<MapTile>>());
        }
        
        [Test] 
        public void CallSetNodesNeighbours()
        {
            //Given
            var mapService = AMapService();
            var startGame = ACreateNodes(withMapService: mapService);

            //When
            startGame.Do(Enumerable.Empty<MapTile>());

            //Then
            mapService.Received(1).SetNodesNeighbours(Arg.Any<Dictionary<Coordinate, IAStarNode>>());
        }

        
        [Test] [Ignore("Not yet decided implementation")]
        public void CreateOneNode()
        {
            //Given
            var inputTiles = new[] {AMapTile(1, 1)}; 
            var mapService = new MapService();
            var mapRepository = new MapRepository();
            var startGame = ACreateNodes(
                withMapService: mapService,
                withMapRepository: mapRepository
            );

            var expectedNodes = new []{new MapNode()};

            //When
            startGame.Do(inputTiles);

            //Then
            mapRepository.Received(1).LoadNodes(expectedNodes);
        }
    }
}