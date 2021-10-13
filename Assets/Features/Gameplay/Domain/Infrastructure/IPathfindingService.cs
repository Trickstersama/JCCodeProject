using System.Collections.Generic;
using PathFinding;

namespace Features.Gameplay.Domain.Infrastructure
{
    public interface IPathfindingService
    {
        IEnumerable<IAStarNode> CalculatePath(IAStarNode startNode, IAStarNode endNode);
    }
}