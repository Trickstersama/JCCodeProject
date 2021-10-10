using System;
using Features.Gameplay.Domain.Infrastructure;
using Features.Gameplay.Domain.ValueObjects;

namespace Features.Gameplay.Domain.Actions
{
    public class ClickMapTile
    {
        readonly IMapRepository mapRepository;
        readonly IMapService mapService;
        readonly Action OnResetNodes;

        public ClickMapTile(
            IMapRepository mapRepository,
            IMapService mapService, 
            Action onResetNodes
        ) {
            this.mapRepository = mapRepository;
            this.mapService = mapService;
            OnResetNodes = onResetNodes;
        }

        public void Do(Coordinate coordinate)
        {
            if (!mapRepository.IsStartSelected())
            {
                mapRepository.SetStart(coordinate);
            }
            else
            {
                if (mapService.CoordinateIsStart(coordinate,mapRepository.GetStartCoordinate()))
                {
                    mapRepository.ResetNodes();
                    OnResetNodes?.Invoke();
                }
                mapRepository.SetGoal(coordinate);            
            }
        }
    }
}