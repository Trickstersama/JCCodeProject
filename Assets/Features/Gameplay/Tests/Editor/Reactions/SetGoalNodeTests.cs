using System;
using Features.Gameplay.Domain.Infrastructure;
using Features.Gameplay.Domain.Reactions;
using Features.Gameplay.Domain.ValueObjects;
using Features.Gameplay.Infrastructure;
using NSubstitute;
using NUnit.Framework;
using static Features.Gameplay.Tests.Mothers.CoordinateMother;
using static Features.Gameplay.Tests.Mothers.MapRepositoryMother;

namespace Features.Gameplay.Tests.Editor.Reactions
{
    [TestFixture]
    public class SetGoalNodeTests
    {
        [Test]
        public void CallSetGoalNodeFromRepository()
        {
            //Given
            var mapRepository = AMapRepository();
            var resetPathNodes = new SetGoalNode(mapRepository);
            
            //When
            resetPathNodes.Do(ACoordinate(), null);
            
            //Then
            mapRepository.Received(1).SetGoal(Arg.Any<Coordinate>());
        }
        
        [Test]
        public void AddGoalCoordinateWithOnlyStartCoordinate()
        {
            //Given
            var startCoordinate = ACoordinate(1, 1);
            var newCoordinate = ACoordinate(7, 2);
            
            var mapRepository = new MapRepository(withStartCoordinate:  startCoordinate);
            var resetPathNodes = new SetGoalNode(mapRepository);
            
            //When
            resetPathNodes.Do(newCoordinate, null);
            
            //Then
            Assert.AreEqual(mapRepository.GetStartCoordinate(), startCoordinate);
            Assert.AreEqual(mapRepository.GetGoalCoordinate(), newCoordinate);
            Assert.AreEqual(mapRepository.IsStartSelected(), true);
            Assert.AreEqual(mapRepository.IsGoalSelected(), true);
        }

        [Test]
        public void AddGoalCoordinateWhenAlreadyHasGoal()
        {
            //Given
            var startCoordinate = ACoordinate(1, 1);
            var goalCoordinate = ACoordinate(3, 1);
            var newCoordinate = ACoordinate(7, 2);
            
            var mapRepository = new MapRepository(
                withStartCoordinate:  startCoordinate,
                withGoalCoordinate: goalCoordinate
            );
            var resetPathNodes = new SetGoalNode(mapRepository);
            
            //When
            resetPathNodes.Do(newCoordinate, null);
            
            //Then
            Assert.AreEqual(mapRepository.GetStartCoordinate(), startCoordinate);
            Assert.AreEqual(mapRepository.GetGoalCoordinate(), newCoordinate);
            Assert.AreEqual(mapRepository.IsStartSelected(), true);
            Assert.AreEqual(mapRepository.IsGoalSelected(), true);
        }

        [Test]
        public void SendOnGoalSet()
        {
            //Given
            var goalCoordinate = ACoordinate(2, 3);
            var onResetNodes = Substitute.For<IObserver<IGameEvent>>();
            var mapRepository = AMapRepository();
            var resetPathNodes = new SetGoalNode(mapRepository);
            
            //When
            resetPathNodes.Do(goalCoordinate, onResetNodes);
            
            //Then
            onResetNodes.Received(1).OnNext(Arg.Any<IGameEvent>());
        }
    }
}