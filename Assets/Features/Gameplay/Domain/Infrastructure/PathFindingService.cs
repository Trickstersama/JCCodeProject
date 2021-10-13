using System.Collections.Generic;
using PathFinding;

namespace Features.Gameplay.Domain.Infrastructure
{
    public class PathFindingService : IPathfindingService
    {
        public IEnumerable<IAStarNode> CalculatePath(IAStarNode startNode, IAStarNode endNode) => 
            AStar.GetPath(startNode, endNode);
    }
}