using Features.Gameplay.Domain.Actions;
using Features.Gameplay.Domain.Infrastructure;
using static Features.Gameplay.Tests.Mothers.MapRepositoryMother;
using static Features.Gameplay.Tests.Mothers.MapServiceMother;

namespace Features.Gameplay.Tests.Mothers
{
    public static class CreateNodesMother
    {
        public static CreateNodes ACreateNodes(
            IMapService withMapService = null,
            IMapRepository withMapRepository = null
        ) =>
            new CreateNodes(
                mapRepository: withMapRepository ?? AMapRepository(),
                mapService: withMapService ?? AMapService()
            );
    }
}