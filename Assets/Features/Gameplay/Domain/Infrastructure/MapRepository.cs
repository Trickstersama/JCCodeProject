using System.Collections.Generic;
using System.Linq;
using Features.Gameplay.Domain.ValueObjects;

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

       
        public void LoadTiles(IEnumerable<MapTile> tiles)
        {
            this.tiles = tiles.ToList();
        }

        public void SetStart(Coordinate newStart)
        {
            startCoordinate = newStart;
            startIsSet = true;
        }

        public void SetGoal(Coordinate newGoal) => 
            goalCoordinate = newGoal;

        public void ResetNodes()
        {
            startCoordinate = new Coordinate();
            goalCoordinate = new Coordinate();

            startIsSet = false;
            goalIsSet = false;
        }


        public Coordinate GetStartCoordinate() => 
            startCoordinate;

        public Coordinate GetGoalCoordinate() => 
            goalCoordinate;
        
        public bool IsStartSelected() => 
            startIsSet;
        
        public bool IsGoalSelected() => 
            goalIsSet;
    }
}