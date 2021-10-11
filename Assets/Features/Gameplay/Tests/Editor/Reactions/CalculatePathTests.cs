using System;
using System.Collections.Generic;
using Features.Gameplay.Domain.Infrastructure;
using NSubstitute;
using NUnit.Framework;
using PathFinding;
using static Features.Gameplay.Tests.Mothers.CalculatePathMother;
using static Features.Gameplay.Tests.Mothers.MapRepositoryMother;

namespace Features.Gameplay.Tests.Editor.Reactions
{
    [TestFixture]
    public class CalculatePathTests
    {
        [Test]
        public void SendOnPathCalculated()
        {
            //Given
            var pathfindingService = Substitute.For<IPathfindingService>();
            var calculatePath = ACalculatePath(withPathfindingService: pathfindingService);
            var onPathNodesReset = Substitute.For<IObserver<IEnumerable<IAStarNode>>>();
            
            //When
            calculatePath.Do(onPathNodesReset);
            
            //Then
            onPathNodesReset.Received(1).OnNext(Arg.Any<IEnumerable<IAStarNode>>());
        }
        
        [Test]
        public void CallPathfindingService()
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
    }
}