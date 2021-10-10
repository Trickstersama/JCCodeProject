using System.Collections.Generic;
using Features.Gameplay.Delivery.Views;
using Features.Gameplay.Domain.Actions;
using Features.Gameplay.Domain.ValueObjects;
using Features.Gameplay.Infrastructure;

namespace Features.Gameplay.Delivery.Presenters
{
    public class mapPresenter
    {
        readonly StartGame startGame;
        public mapPresenter(
            IEnumerable<MapTile> tiles, 
            StartGame startGame,
            MapView mapView,
            ICoordinateService coordinateService
        ) {
            this.startGame = startGame;
            
            startGame.Do(tiles: tiles);
            mapView.Initialize(mapTile: tiles, coordinateService: coordinateService);
        }

    }
}