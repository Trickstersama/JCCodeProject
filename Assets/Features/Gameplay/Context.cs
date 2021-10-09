using System.Collections.Generic;
using Features.Gameplay.Domain.Actions;
using Features.Gameplay.Domain.Infrastructure;
using Features.Gameplay.Domain.ValueObjects;

namespace Features.Gameplay

{
    public static class Context
    {
        public static void Initialize()
        {
            var mapRepository = new MapRepository();

            var startGame = new StartGame(mapRepository);
        }

        
    }

    public class MapRepository : IMapRepository
    {
        public void LoadTiles(IEnumerable<MapTile> tiles)
        {
            throw new System.NotImplementedException();
        }
    }
}