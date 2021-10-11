using System;
using Features.Gameplay.Domain.Infrastructure;
using Features.Gameplay.Domain.ValueObjects;
using Features.Gameplay.Infrastructure;

namespace Features.Gameplay.Domain.Reactions
{
    public class SetGoalNode
    {
        readonly IMapRepository mapRepository;

        public SetGoalNode(IMapRepository mapRepository)
        {
            this.mapRepository = mapRepository;
        }

        public void Do(Coordinate coordinate, IObserver<IGameEvent> onGoalSet)
        {
            mapRepository.SetGoal(coordinate);
            onGoalSet?.OnNext(new GameEvent());
        }
    }
}