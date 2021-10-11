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
            IObserver<Coordinate> onSetGoal
        ) {
            if (!mapRepository.IsStartSelected())
                mapRepository.SetStart(coordinate);
            else
                SendResetOrSetGoal(coordinate, onResetNodes, onSetGoal);
        }

        void SendResetOrSetGoal(Coordinate coordinate, IObserver<IGameEvent> onResetNodes, IObserver<Coordinate> onSetGoal)
        {
            if (IsStartNode(coordinate))
                onResetNodes?.OnNext(new GameEvent());
            else
                onSetGoal?.OnNext(coordinate);
        }

        bool IsStartNode(Coordinate coordinate) => 
            mapService.CoordinateIsStart(coordinate, mapRepository.GetStartCoordinate());
    }
}