using System;
using System.Collections.Generic;
using Features.Gameplay.Domain.Infrastructure;
using Features.Gameplay.Domain.ValueObjects;

namespace Features.Gameplay.Domain.Actions
{
    public class CreateNodes
    {
        readonly IMapRepository mapRepository;
        readonly IMapService mapService;

        public CreateNodes(
            IMapRepository mapRepository,
            IMapService mapService
        ) {
            this.mapRepository = mapRepository;
            this.mapService = mapService;
        }

        public void Do(IEnumerable<MapTile> tiles, IObserver<IEnumerable<MapTile>> onNodesCreated)
        {
            var freshNodes = mapService.CreateNodesFromTiles(tiles);
            var nodesWithNeighbours = mapService.SetNodesNeighbours(freshNodes);
            mapRepository.LoadNodes(nodesWithNeighbours);
            onNodesCreated?.OnNext(tiles);
        }
    }   
}