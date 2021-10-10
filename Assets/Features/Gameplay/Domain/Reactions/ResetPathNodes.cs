using System;
using Features.Gameplay.Domain.Infrastructure;
using Features.Gameplay.Infrastructure;

namespace Features.Gameplay.Domain.Reactions
{
    public class ResetPathNodes
    {
        readonly IMapRepository mapRepository;
        readonly IObserver<IGameEvent> onPathNodesReset;

        public ResetPathNodes(
            IMapRepository mapRepository,
            IObserver<IGameEvent> onPathNodesReset
        )
        {
            this.mapRepository = mapRepository;
            this.onPathNodesReset = onPathNodesReset;
        }

        public void Do()
        {
            mapRepository.ResetNodes();
            onPathNodesReset?.OnNext(new GameEvent());
        }
    }
}