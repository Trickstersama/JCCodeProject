using System.Collections.Generic;
using System.Linq;
using Features.Gameplay.Domain.ValueObjects;

namespace Features.Gameplay.Domain.Infrastructure
{
    public class MapRepository : IMapRepository
    {
        List<MapTile> tiles = new List<MapTile>();
        public void LoadTiles(IEnumerable<MapTile> tiles)
        {
            this.tiles = tiles.ToList();
        }

        public void SetStart(Coordinate any)
        {
            throw new System.NotImplementedException();
        }

        public void SetGoal(Coordinate coordinate)
        {
            throw new System.NotImplementedException();
        }

        public void ResetNodes()
        {
            throw new System.NotImplementedException();
        }
    }
}