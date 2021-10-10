using Features.Gameplay.Domain.Actions;
using Features.Gameplay.Domain.Infrastructure;
using Features.Gameplay.Domain.ValueObjects;
using NSubstitute;
using NUnit.Framework;
using static Features.Gameplay.Tests.Mothers.MapServiceMother;

namespace Features.Gameplay.Tests.Editor
{
    [TestFixture]
    public class ClickMapTileTests
    {
        [Test]
        public void CallStartIsNotSelectedFromMapService()
        {
            //given
            var mapRepository = Substitute.For<IMapRepository>();
            var mapService = AMapService();
            var clickMapTile = new ClickMapTile(mapRepository, mapService);
            
            //when
            clickMapTile.Do(new Coordinate());
            
            //then
            mapService.Received(1).StartIsNotSelected();
        }
        
        [Test]
        public void ClickMapTileSetsStartIfNotSetYet()
        {
            //given
            var mapRepository = Substitute.For<IMapRepository>();
            var mapService = AMapService(withStartIsNotSelected: true);
            var clickMapTile = new ClickMapTile(mapRepository, mapService);
            
            //when
            clickMapTile.Do(new Coordinate());
            
            //then
            mapRepository.Received(1).SetStart(Arg.Any<Coordinate>());
        }
        
        [Test]
        public void ClickMapTileSetsGoalIfOnlyStartIsSet()
        {
            //given
            var mapRepository = Substitute.For<IMapRepository>();
            var mapService = AMapService(withStartIsNotSelected: false);
            var clickMapTile = new ClickMapTile(mapRepository, mapService);
            
            //when
            clickMapTile.Do(new Coordinate());
            
            //then
            mapRepository.Received(1).SetGoal(Arg.Any<Coordinate>());
        }
        
        [Test]
        public void CallCoordinateIsStart()
        {
            //given
            var mapRepository = Substitute.For<IMapRepository>();
            var mapService = AMapService(withStartIsNotSelected: false);
            var clickMapTile = new ClickMapTile(mapRepository, mapService);
            
            //when
            clickMapTile.Do(new Coordinate());
            
            //then
            mapService.Received(1).CoordinateIsStart(Arg.Any<Coordinate>());
        }
        
        [Test]
        public void TileClickedIsStart()
        {
            //given
            var mapRepository = Substitute.For<IMapRepository>();
            var mapService = AMapService(withStartIsNotSelected: false);
            var clickMapTile = new ClickMapTile(mapRepository, mapService);
            
            //when
            clickMapTile.Do(new Coordinate());
            
            //then
            mapRepository.Received(1).ResetNodes();
        }
    }
}