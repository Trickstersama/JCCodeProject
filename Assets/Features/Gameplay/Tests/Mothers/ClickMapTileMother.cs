using Features.Gameplay.Domain.Actions;
using Features.Gameplay.Domain.Infrastructure;
using static Features.Gameplay.Tests.Mothers.MapRepositoryMother;
using static Features.Gameplay.Tests.Mothers.MapServiceMother;

namespace Features.Gameplay.Tests.Mothers
{
    public static class ClickMapTileMother
    {
        public static ClickMapTile AClickMapTile(
            IMapRepository withMapRepository = null,
            IMapService withMapService = null
        ) {
            return new ClickMapTile(
                mapRepository: withMapRepository ?? AMapRepository(),
                mapService: withMapService ?? AMapService());
        }
    }
}