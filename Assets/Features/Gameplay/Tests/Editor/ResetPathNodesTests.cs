using System;
using Features.Gameplay.Domain.Infrastructure;
using Features.Gameplay.Domain.Reactions;
using Features.Gameplay.Infrastructure;
using NSubstitute;
using NUnit.Framework;
using static Features.Gameplay.Tests.Mothers.CoordinateMother;
using static Features.Gameplay.Tests.Mothers.MapRepositoryMother;

namespace Features.Gameplay.Tests.Editor
{
    [TestFixture]
    public class ResetPathNodesTests
    {
        [Test]
        public void CallResetNodesFromMapRepository()
        {
            //Given
            var mapRepository = AMapRepository();
            var resetPathNodes = new ResetPathNodes(mapRepository, null);
            
            //When
            resetPathNodes.Do();
            
            //Then
            mapRepository.Received(1).ResetNodes();
        }
        
        [Test]
        public void ResetNodesOnRepository()
        {
            //Given
            var mapRepository = new MapRepository(
                withStartCoordinate: ACoordinate(),
                withGoalCoordinate: ACoordinate(11, 11)
            );
            var resetPathNodes = new ResetPathNodes(mapRepository, null);
            
            //When
            resetPathNodes.Do();
            
            //then
            Assert.AreEqual(mapRepository.IsStartSelected(), false);
            Assert.AreEqual(mapRepository.IsGoalSelected(), false);
        }
        
        
        [Test]
        public void SendOnPathNodesReset()
        {
            //Given
            var onPathNodesReset = Substitute.For<IObserver<IGameEvent>>();

            var mapRepository = AMapRepository();
            var resetPathNodes = new ResetPathNodes(mapRepository, onPathNodesReset);
            
            //When
            resetPathNodes.Do();
            
            //Then
            onPathNodesReset.Received(1).OnNext(Arg.Any<IGameEvent>());
        }
    }
}