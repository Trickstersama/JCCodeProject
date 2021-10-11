using System;
using Features.Gameplay.Domain.Infrastructure;
using Features.Gameplay.Infrastructure;

namespace Features.Gameplay.Domain.Reactions
{
    public class ResetPathNodes
    {
        readonly IMapRepository mapRepository;

        public ResetPathNodes(IMapRepository mapRepository) =>
            this.mapRepository = mapRepository;

        public void Do(IObserver<IGameEvent> onPathNodesReset)
        {
            mapRepository.ResetNodes();
            onPathNodesReset?.OnNext(new GameEvent());
        }
    }
}