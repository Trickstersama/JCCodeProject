using System.Collections.Generic;
using Features.Gameplay.Delivery.Presenters;
using Features.Gameplay.Domain.Actions;
using Features.Gameplay.Domain.Infrastructure;
using Features.Gameplay.Domain.ValueObjects;

namespace Features.Gameplay

{
    public static class Context
    {
        public static void Initialize(IEnumerable<MapTile> tiles)
        {
            var mapRepository = new MapRepository();
            var startGame = new StartGame(mapRepository);

            var mapPresenter = new mapPresenter(tiles, startGame);
        }
    }
}