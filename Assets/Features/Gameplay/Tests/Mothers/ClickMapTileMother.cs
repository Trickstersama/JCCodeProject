using System;
using Features.Gameplay.Domain.Actions;
using Features.Gameplay.Domain.Infrastructure;

namespace Features.Gameplay.Tests.Mothers
{
    public static class ClickMapTileMother
    {
        public static ClickMapTile AClickMapTile(
            IMapRepository withMapRepository = null,
            IMapService withMapService = null,
            Action withResetNodes = null
        ) {
            return new ClickMapTile(
                mapRepository: withMapRepository ?? MapRepositoryMother.AMapRepository(),
                mapService: withMapService ?? MapServiceMother.AMapService(),
                onResetNodes: withResetNodes);
        }
    }
}