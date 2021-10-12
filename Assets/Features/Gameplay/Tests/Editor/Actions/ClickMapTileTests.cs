using System;
using Features.Gameplay.Domain.Infrastructure;
using Features.Gameplay.Domain.ValueObjects;
using Features.Gameplay.Infrastructure;
using NSubstitute;
using NUnit.Framework;
using static Features.Gameplay.Tests.Mothers.ClickMapTileMother;
using static Features.Gameplay.Tests.Mothers.CoordinateMother;
using static Features.Gameplay.Tests.Mothers.MapRepositoryMother;
using static Features.Gameplay.Tests.Mothers.MapServiceMother;

namespace Features.Gameplay.Tests.Editor.Actions
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
            clickMapTile.Do(ACoordinate(), null, null, null);
            
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
            clickMapTile.Do(ACoordinate(), null, null, null);
            
            //then
            mapRepository.Received(1).SetStart(Arg.Any<Coordinate>());
        }

        [Test]
        public void CallCoordinateIsStartFromService()
        {
            //given
            var mapRepository = AMapRepository(withStartSelected: true);
            var mapService = AMapService();
            var clickMapTile = AClickMapTile(
                withMapRepository: mapRepository,
                withMapService: mapService
            );
            
            //when
            clickMapTile.Do(ACoordinate(), null, null, null);
            
            //then
            mapService.Received(1).CoordinateIsStart(Arg.Any<Coordinate>(), Arg.Any<Coordinate>());
        }
        
        [Test]
        public void CallGetStartCoordinateFromRepository()
        {
            //given
            var mapRepository = AMapRepository(withStartSelected: true);
            var clickMapTile = AClickMapTile(withMapRepository: mapRepository);
            
            //when
            clickMapTile.Do(ACoordinate(), null, null, null);
            
            //then
            mapRepository.Received(1).GetStartCoordinate();
        }
        
        [Test]
        public void SendOnResetNodes()
        {
            //given
            var onResetNodes = Substitute.For<IObserver<IGameEvent>>();
            var mapRepository = AMapRepository(withStartSelected: true);
            var mapService = AMapService(withCoordinateIsStart: true);
            var clickMapTile = AClickMapTile(
                withMapRepository: mapRepository,
                withMapService: mapService
            );
            
            //when
            clickMapTile.Do(ACoordinate(), onResetNodes, null, null);
            
            //then
            onResetNodes.Received(1).OnNext(Arg.Any<IGameEvent>());
        }
        
        [Test]
        public void SendOnSetGoal()
        {
            //given
            var onSetGoal = Substitute.For<IObserver<Coordinate>>();
            var mapRepository = AMapRepository(withStartSelected: true);
            var mapService = AMapService(withCoordinateIsStart: false);
            var clickMapTile = AClickMapTile(
                withMapRepository: mapRepository,
                withMapService: mapService
            );
            var newCoordinate = ACoordinate(1, 2);
            var expectedCoordinate = newCoordinate;
            
            //when
            clickMapTile.Do(newCoordinate, null, onSetGoal, null);
            
            //then
            onSetGoal.Received(1).OnNext(expectedCoordinate);
        }
        [Test]
        public void SendOnStartSet()
        {
            //given
            var onSetGoal = Substitute.For<IObserver<Coordinate>>();
            var mapRepository = AMapRepository(withStartSelected: true);
            var mapService = AMapService(withCoordinateIsStart: false);
            var clickMapTile = AClickMapTile(
                withMapRepository: mapRepository,
                withMapService: mapService
            );
            var newCoordinate = ACoordinate(1, 2);
            var expectedCoordinate = newCoordinate;
            
            //when
            clickMapTile.Do(newCoordinate, null, onSetGoal, null);
            
            //then
            onSetGoal.Received(1).OnNext(expectedCoordinate);
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
            clickMapTile.Do(newCoordinate, null, null, null);
            
            //then
            Assert.AreEqual(mapRepository.GetStartCoordinate(), expectedCoordinate);
            Assert.AreEqual(mapRepository.IsStartSelected(), true);

        }
        
        [Test]
        public void SecondClickSetsGoal()
        {
            //given
            var onSetGoal = Substitute.For<IObserver<Coordinate>>();
            var mapRepository = new MapRepository(withStartCoordinate: ACoordinate());
            var mapService = new MapService();
            var clickMapTile = AClickMapTile(
                withMapRepository: mapRepository,
                withMapService: mapService
            );
            var newCoordinate = ACoordinate(3, 3);
            var expectedCoordinate = newCoordinate;
            
            //when
            clickMapTile.Do(newCoordinate, null, onSetGoal, null);
            
            //then
            onSetGoal.Received(1).OnNext(expectedCoordinate);
        }
        
        [Test]
        public void ThirdClickOverridesGoal()
        {
            //given
            var onSetGoal = Substitute.For<IObserver<Coordinate>>();
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
            var expectedCoordinate = newCoordinate;
            
            //when
            clickMapTile.Do(newCoordinate, null, onSetGoal, null);

            //then
            onSetGoal.Received(1).OnNext(expectedCoordinate);
        }
    }
}