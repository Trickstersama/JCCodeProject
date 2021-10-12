using System.Collections.Generic;
using Features.Gameplay.Domain.ValueObjects;
using PathFinding;

namespace Features.Gameplay.Domain.Infrastructure
{
    public class MapRepository : IMapRepository
    {
        Dictionary< Coordinate, MapNode> nodes = new Dictionary<Coordinate, MapNode>();
        Coordinate startCoordinate;
        Coordinate goalCoordinate;
        
        bool startIsSet;
        bool goalIsSet;
        public MapRepository(
            Coordinate? withStartCoordinate = null, 
            Coordinate? withGoalCoordinate = null
        ) {
            startCoordinate = withStartCoordinate ?? new Coordinate();
            goalCoordinate = withGoalCoordinate ?? new Coordinate();

            startIsSet = withStartCoordinate != null;
            goalIsSet = withGoalCoordinate != null;
        }

        public IEnumerable<IAStarNode> GetNodes => nodes.Values;


        public void LoadNodes(IEnumerable<MapNode> newNodes)
        {
            foreach (var newNode in newNodes)
            {
                nodes.Add(newNode.Coordinate(), newNode);
            }
        }

        public void SetStart(Coordinate newStart)
        {
            startCoordinate = newStart;
            startIsSet = true;
        }

        public void SetGoal(Coordinate newGoal)
        {
            goalCoordinate = newGoal;
            goalIsSet = true;
        }

        public void ResetNodes()
        {
            startCoordinate = new Coordinate();
            goalCoordinate = new Coordinate();

            startIsSet = false;
            goalIsSet = false;
        }

        public Coordinate GetStartCoordinate() => 
            startCoordinate;

        public IAStarNode GetStartNode()
        {
            return nodes[startCoordinate];
        }

        public IAStarNode GetGoalNode()
        {
            return nodes[goalCoordinate];
        }

        public Coordinate GetGoalCoordinate() => 
            goalCoordinate;
        
        public bool IsStartSelected() => 
            startIsSet;
        
        public bool IsGoalSelected() => 
            goalIsSet;
    }
}