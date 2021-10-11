using System;
using System.Collections.Generic;
using System.Linq;
using Features.Gameplay.Domain.Infrastructure;
using Features.Gameplay.Domain.ValueObjects;
using PathFinding;

namespace Features.Gameplay.Domain.Reactions
{
    public class CalculatePath
    {
        readonly IPathfindingService pathfindingService;

        public CalculatePath(IPathfindingService pathfindingService)
        {
            this.pathfindingService = pathfindingService;
        }

        public void Do(IObserver<IEnumerable<IAStarNode>> onPathNodesReset)
        {
            onPathNodesReset?.OnNext(Enumerable.Empty<IAStarNode>());
            pathfindingService.CalculatePath(new MapNode(), new MapNode());
        }
    }
}