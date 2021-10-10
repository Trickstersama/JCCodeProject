using System;
using Features.Gameplay.Domain.Infrastructure;
using Features.Gameplay.Domain.ValueObjects;
using Features.Gameplay.Infrastructure;

namespace Features.Gameplay.Domain.Actions
{
    public class ClickMapTile
    {
        readonly IMapRepository mapRepository;
        readonly IMapService mapService;

        public ClickMapTile(
            IMapRepository mapRepository,
            IMapService mapService 
        ) {
            this.mapRepository = mapRepository;
            this.mapService = mapService;
        }

        public void Do(
            Coordinate coordinate, 
            IObserver<IGameEvent> onResetNodes,
            IObserver<IGameEvent> onGoalSet
        ) {
            if (!mapRepository.IsStartSelected())
                mapRepository.SetStart(coordinate);
            else
            {
                SendResetOrSetGoal(coordinate, onResetNodes, onGoalSet);
            }
        }

        void SendResetOrSetGoal(Coordinate coordinate, IObserver<IGameEvent> onResetNodes, IObserver<IGameEvent> onGoalSet)
        {
            if (mapService.CoordinateIsStart(coordinate, mapRepository.GetStartCoordinate()))
                onResetNodes?.OnNext(new GameEvent());
            else
            {
                mapRepository.SetGoal(coordinate);
                onGoalSet?.OnNext(new GameEvent());
            }
        }
    }
}