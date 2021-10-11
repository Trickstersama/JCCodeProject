using Features.Gameplay.Domain.Infrastructure;
using Features.Gameplay.Domain.Reactions;
using static Features.Gameplay.Tests.Mothers.MapRepositoryMother;
using static Features.Gameplay.Tests.Mothers.PathFindingServiceMother;

namespace Features.Gameplay.Tests.Mothers
{
    public static class CalculatePathMother
    {
        public static CalculatePath ACalculatePath(
            IPathfindingService withPathfindingService = null,
            IMapRepository withMapRepository = null
        ) {
            return new CalculatePath(
                withPathfindingService ?? APathfindingService(),
                withMapRepository ?? AMapRepository());
        }
    }
}