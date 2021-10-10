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
    }
}