using Features.Gameplay.Domain.Reactions;
using Features.Gameplay.Domain.ValueObjects;
using NSubstitute;
using NUnit.Framework;
using static Features.Gameplay.Tests.Mothers.CoordinateMother;
using static Features.Gameplay.Tests.Mothers.MapRepositoryMother;

namespace Features.Gameplay.Tests.Editor.Actions
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
            resetPathNodes.Do(ACoordinate());
            
            //Then
            mapRepository.Received(1).SetGoal(Arg.Any<Coordinate>());
        }
        /*
        Assert.AreEqual(mapRepository.GetStartCoordinate(), startCoordinate);
        Assert.AreEqual(mapRepository.GetGoalCoordinate(), newCoordinate);
        Assert.AreEqual(mapRepository.IsStartSelected(), true);
        Assert.AreEqual(mapRepository.IsGoalSelected(), true);
        */
    }
}