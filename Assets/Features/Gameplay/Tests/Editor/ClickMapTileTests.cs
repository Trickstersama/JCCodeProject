using System;
using Features.Gameplay.Domain.Actions;
using Features.Gameplay.Domain.Infrastructure;
using Features.Gameplay.Domain.ValueObjects;
using NSubstitute;
using NUnit.Framework;
using static Features.Gameplay.Tests.Mothers.ClickMapTileMother;
using static Features.Gameplay.Tests.Mothers.CoordinateMother;
using static Features.Gameplay.Tests.Mothers.MapRepositoryMother;
using static Features.Gameplay.Tests.Mothers.MapServiceMother;

namespace Features.Gameplay.Tests.Editor
{
    [TestFixture]
    public class ClickMapTileTests
    {
        Coordinate startCoordinate;

        [Test]
        public void CallIsStartSelectedFromMapRepository()
        {
            //given
            var mapRepository = AMapRepository();
            var clickMapTile = AClickMapTile(withMapRepository: mapRepository);
            
            //when
            clickMapTile.Do(ACoordinate());
            
            //then
            mapRepository.Received(1).IsStartSelected();
        }
        
        [Test]
        public void ClickMapTileSetsStartIfNotSetYet()
        {
            //given
            var mapRepository = AMapRepository(withStartSelected: false);
            var clickMapTile = AClickMapTile(withMapRepository: mapRepository);
            
            //when
            clickMapTile.Do(ACoordinate());
            
            //then
            mapRepository.Received(1).SetStart(Arg.Any<Coordinate>());
        }
        
        [Test]
        public void ClickMapTileSetsGoalIfOnlyStartIsSet()
        {
            //given
            var mapRepository = AMapRepository(withStartSelected: true);
            var clickMapTile = AClickMapTile(withMapRepository: mapRepository);
            
            //when
            clickMapTile.Do(ACoordinate());
            
            //then
            mapRepository.Received(1).SetGoal(Arg.Any<Coordinate>());
        }
        
        [Test]
        public void CallCoordinateIsStart()
        {
            //given
            var mapRepository = AMapRepository(withStartSelected: true);
            var mapService = AMapService();
            var clickMapTile = AClickMapTile(
                withMapRepository: mapRepository,
                withMapService: mapService
            );
            
            //when
            clickMapTile.Do(ACoordinate());
            
            //then
            mapService.Received(1).CoordinateIsStart(Arg.Any<Coordinate>(), Arg.Any<Coordinate>());
        }
        
        [Test]
        public void CalGetStartCoordinateFromRepository()
        {
            //given
            var mapRepository = AMapRepository(withStartSelected: true);
            var clickMapTile = AClickMapTile(withMapRepository: mapRepository);
            
            //when
            clickMapTile.Do(ACoordinate());
            
            //then
            mapRepository.Received(1).GetStartCoordinate();
        }
        
        [Test]
        public void TileClickedIsStart()
        {
            //given
            var mapRepository = AMapRepository(withStartSelected: true);
            var mapService = AMapService(withCoordinateIsStart: true);
            var clickMapTile = AClickMapTile(
                withMapRepository: mapRepository,
                withMapService: mapService
            );
            
            //when
            clickMapTile.Do(ACoordinate());
            
            //then
            mapRepository.Received(1).ResetNodes();
        }
        
        [Test]
        public void ResetInvokesEvent()
        {
            //given
            Action onResetNodes = null;
            var mapRepository = AMapRepository(withStartSelected: true);
            var mapService = AMapService(withCoordinateIsStart: true);
            var clickMapTile = AClickMapTile(
                withMapRepository: mapRepository,
                withMapService: mapService,
                withResetNodes: onResetNodes
            );
            
            //when
            clickMapTile.Do(new Coordinate());
            
            //then
            mapRepository.Received(1).ResetNodes();
        }
        
        [Test]
        public void FirstClickSetsStart()
        {
            //given
            var mapRepository = new MapRepository();
            var mapService = new MapService();
            var clickMapTile = AClickMapTile(
                withMapRepository: mapRepository,
                withMapService: mapService
            );
            var newCoordinate = ACoordinate(3, 3);
            var expectedCoordinate = newCoordinate;
            
            //when
            clickMapTile.Do(newCoordinate);
            
            //then
            Assert.AreEqual(mapRepository.GetStartCoordinate(), expectedCoordinate);
        }
        
        [Test]
        public void SecondClickSetsGoal()
        {
            //given
            var mapRepository = new MapRepository(withStartCoordinate: ACoordinate());
            var mapService = new MapService();
            var clickMapTile = AClickMapTile(
                withMapRepository: mapRepository,
                withMapService: mapService
            );
            var newCoordinate = ACoordinate(3, 3);
            var expectedCoordinate = newCoordinate;
            
            //when
            clickMapTile.Do(newCoordinate);
            
            //then
            Assert.AreEqual(mapRepository.GetGoalCoordinate(), expectedCoordinate);
        }
        
        [Test]
        public void ThirdClickOverridesGoal()
        {
            //given
            startCoordinate = ACoordinate(2, 2);
            var mapRepository = new MapRepository(
                withStartCoordinate: startCoordinate,
                withGoalCoordinate: ACoordinate(11, 11)
            );
            var mapService = new MapService();
            var clickMapTile = AClickMapTile(
                withMapRepository: mapRepository,
                withMapService: mapService
            );
            var newCoordinate = ACoordinate(11,11);
            
            //when
            clickMapTile.Do(newCoordinate);
            
            //then
            Assert.AreEqual(mapRepository.GetStartCoordinate(), startCoordinate);
            Assert.AreEqual(mapRepository.GetGoalCoordinate(), newCoordinate);
        }
        [Test]
        public void FourthClickResetCoordinates()
        {
            //given
            startCoordinate = ACoordinate(2, 2);
            var mapRepository = new MapRepository(
                withStartCoordinate: startCoordinate,
                withGoalCoordinate: ACoordinate(11, 11)
            );
            var mapService = new MapService();
            var clickMapTile = AClickMapTile(
                withMapRepository: mapRepository,
                withMapService: mapService
            );
            var newCoordinate = startCoordinate;
            
            //when
            clickMapTile.Do(newCoordinate);
            
            //then
            Assert.AreEqual(mapRepository.IsStartSelected(), false);
            Assert.AreEqual(mapRepository.IsGoalSelected(), false);
        }
        

    }
}