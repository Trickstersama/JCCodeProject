using System.Collections.Generic;
using Features.Gameplay.Delivery.Presenters;
using Features.Gameplay.Delivery.Views;
using Features.Gameplay.Domain.Actions;
using Features.Gameplay.Domain.Infrastructure;
using Features.Gameplay.Domain.Reactions;
using Features.Gameplay.Domain.ValueObjects;

namespace Features.Gameplay

{
    public static class Context
    {
        public static void Initialize(IEnumerable<MapTile> tiles, MapView mapView)
        {
            //repos and services
            var mapRepository = new MapRepository();
            var mapService = new MapService();
            var coordinateService = new CoordinatesService();
            //actions
            var startGame = new CreateNodes(
                mapRepository: mapRepository,
                mapService: mapService
            );
            var resetPathNodes = new ResetPathNodes(mapRepository: mapRepository);
            var clickMapTile = new ClickMapTile(
                mapRepository: mapRepository, 
                mapService: mapService
            );
            var setGoalNode = new SetGoalNode(mapRepository);

            var mapPresenter = new MapPresenter(
                tiles:tiles,
                createNodes: startGame,
                mapView: mapView,
                coordinateService: coordinateService,
                clickMapTile: clickMapTile,
                resetPathNodes: resetPathNodes,
                setGoalNode: setGoalNode
            );
        }
    }
}