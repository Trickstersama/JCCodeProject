using System.Collections.Generic;
using Features.Gameplay.Delivery.Presenters;
using Features.Gameplay.Delivery.Views;
using Features.Gameplay.Domain.Actions;
using Features.Gameplay.Domain.Infrastructure;
using Features.Gameplay.Domain.ValueObjects;

namespace Features.Gameplay

{
    public static class Context
    {
        public static void Initialize(IEnumerable<MapTile> tiles, MapView mapView)
        {
            var mapRepository = new MapRepository();
            var coordinateService = new CoordinatesService();
            var startGame = new StartGame(mapRepository);

            var mapPresenter = new mapPresenter(
                tiles:tiles,
                startGame: startGame,
                mapView: mapView,
                coordinateService: coordinateService
                );
        }
    }
}