using System.Collections.Generic;
using Features.Gameplay.Domain.ValueObjects;
using PathFinding;

namespace Features.Gameplay.Domain.Infrastructure
{
    public interface IMapRepository
    {
        void LoadNodes(IEnumerable<MapNode> newNodes);
        void SetStart(Coordinate coordinate);
        void SetGoal(Coordinate newGoal);
        void ResetNodes();
        bool IsStartSelected();
        Coordinate GetStartCoordinate();
        IAStarNode GetStartNode();
        IAStarNode GetGoalNode();
        bool IsWalkable(Coordinate coordinate);
    }
}