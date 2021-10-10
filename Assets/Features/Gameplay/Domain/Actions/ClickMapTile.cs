using Features.Gameplay.Domain.Infrastructure;
using Features.Gameplay.Domain.ValueObjects;

namespace Features.Gameplay.Domain.Actions
{
    public class ClickMapTile
    {
        readonly IMapRepository mapRepository;
        readonly IMapService mapService;

        public ClickMapTile(IMapRepository mapRepository, IMapService mapService)
        {
            this.mapRepository = mapRepository;
            this.mapService = mapService;
        }

        public void Do(Coordinate coordinate)
        {
            if (mapService.StartIsNotSelected())
            {
                mapRepository.SetStart(coordinate);
            }
            else
            {
                if (mapService.CoordinateIsStart(coordinate))
                {
                    mapRepository.ResetNodes();
                }
                mapRepository.SetGoal(coordinate);            
            }
        }
    }
}