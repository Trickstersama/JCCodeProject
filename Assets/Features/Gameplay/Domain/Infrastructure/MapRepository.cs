using System.Collections.Generic;
using System.Linq;
using Features.Gameplay.Domain.ValueObjects;
using PathFinding;

namespace Features.Gameplay.Domain.Infrastructure
{
    public class MapRepository : IMapRepository
    {
        List<MapTile> tiles = new List<MapTile>();
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

       
        public void LoadNodes(IEnumerable<MapTile> tiles)
        {
            this.tiles = tiles.ToList();
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
            return new MapNode();
        }

        public IAStarNode GetGoalNode()
        {
            return new MapNode();
        }

        public Coordinate GetGoalCoordinate() => 
            goalCoordinate;
        
        public bool IsStartSelected() => 
            startIsSet;
        
        public bool IsGoalSelected() => 
            goalIsSet;
    }
}