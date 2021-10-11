using System;
using System.Collections.Generic;
using Features.Gameplay.Domain.Infrastructure;
using Features.Gameplay.Domain.Reactions;
using NSubstitute;
using NUnit.Framework;
using PathFinding;

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
            var calculatePath = new CalculatePath(pathfindingService: pathfindingService);
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
            var calculatePath = new CalculatePath(pathfindingService);
            
            //When
            calculatePath.Do(null);
            
            //Then
            pathfindingService.Received(1).CalculatePath(Arg.Any<IAStarNode>(), Arg.Any<IAStarNode>());
        }
    }
}