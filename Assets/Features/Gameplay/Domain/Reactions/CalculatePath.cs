using System;
using System.Collections.Generic;
using Features.Gameplay.Domain.Infrastructure;
using PathFinding;

namespace Features.Gameplay.Domain.Reactions
{
    public class CalculatePath
    {
        readonly IPathfindingService pathfindingService;
        readonly IMapRepository mapRepository;

        public CalculatePath(
            IPathfindingService pathfindingService, 
            IMapRepository mapRepository
        ) {
            this.pathfindingService = pathfindingService;
            this.mapRepository = mapRepository;
        }

        public void Do(IObserver<IEnumerable<IAStarNode>> onPathCalculated)
        {
            var xxx = pathfindingService.CalculatePath(
                mapRepository.GetStartNode(),
                mapRepository.GetGoalNode()
            );
            
            onPathCalculated?.OnNext(xxx);
        }
    }
}