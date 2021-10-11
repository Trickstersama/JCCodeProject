using System.Linq;
using Features.Gameplay.Domain.Infrastructure;
using NSubstitute;
using PathFinding;

namespace Features.Gameplay.Tests.Mothers
{
    public static class PathFindingServiceMother
    {
        public static IPathfindingService APathfindingService()
        {
            var service = Substitute.For<IPathfindingService>();
            service.CalculatePath(Arg.Any<IAStarNode>(), Arg.Any<IAStarNode>())
                .Returns(Enumerable.Empty<IAStarNode>());
            return service;
        }
    }
}