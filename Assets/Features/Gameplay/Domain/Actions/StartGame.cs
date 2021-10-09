using System.Collections.Generic;
using Features.Gameplay.Domain.Infrastructure;
using Features.Gameplay.Domain.ValueObjects;

namespace Features.Gameplay.Domain.Actions
{
    public class StartGame
    {
        readonly IMapRepository mapRepository;

        public StartGame(IMapRepository mapRepository)
        {
            this.mapRepository = mapRepository;
        }

        public void Do(IEnumerable<MapTile> tiles)
        {
            mapRepository.LoadTiles(tiles);
        }
    }
}