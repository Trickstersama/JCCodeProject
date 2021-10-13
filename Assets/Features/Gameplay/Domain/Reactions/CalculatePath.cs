using System;
using System.Collections.Generic;
using System.Linq;
using Features.Gameplay.Domain.Infrastructure;
using Features.Gameplay.Domain.ValueObjects;

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

        public void Do(IObserver<IEnumerable<Coordinate>> onPathCalculated)
        {
            var rawNodes = pathfindingService.CalculatePath(
                mapRepository.GetStartNode(),
                mapRepository.GetGoalNode()
            );
            var coordinatesInOrder = rawNodes.Select(rawNode =>
            {
                var node = (MapNode) rawNode;
                return node.Coordinate();
            });
            onPathCalculated?.OnNext(coordinatesInOrder);
        }
    }
}