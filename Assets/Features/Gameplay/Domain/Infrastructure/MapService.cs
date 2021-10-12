using System.Collections.Generic;
using Features.Gameplay.Domain.ValueObjects;

namespace Features.Gameplay.Domain.Infrastructure
{
    public class MapService : IMapService
    {
        public bool CoordinateIsStart(Coordinate selectedCoordinate, Coordinate startCoordinate) => 
            selectedCoordinate.Equals(startCoordinate);

        public IEnumerable<MapNode> CreateNodesFromTiles(IEnumerable<MapTile> tiles)
        {
            var nodes = new List<MapNode>();
            foreach (var tile in tiles)
            {
                var newNode = new MapNode(
                    coordinate: tile.coordinate,
                    weight: SelectWeightByType(tile.TileType)
                );
                nodes.Add(newNode);
            }
            return nodes;
        }

        public IEnumerable<MapNode> SetNodesNeighbours(IEnumerable<MapNode> nodes)
        {
            var nodesByCoordinate = new Dictionary<Coordinate, MapNode>();
            foreach (var node in nodes)
            {
                nodesByCoordinate.Add(node.Coordinate(), node);
            }
            return CreateNodeWithNeighboursList(nodesByCoordinate);
        }

        IEnumerable<MapNode> CreateNodeWithNeighboursList(Dictionary<Coordinate, MapNode> nodesByCoordinate)
        {
            var nodesWithNeighbour = new List<MapNode>();
            foreach (var kvpNode in nodesByCoordinate)
            {
                var node = kvpNode.Value;
                node.SetNeighbours(FindNeighboursFor(kvpNode.Key, nodesByCoordinate));
                nodesWithNeighbour.Add(kvpNode.Value);
            }

            return nodesWithNeighbour;
        }

        IEnumerable<MapNode> FindNeighboursFor(
            Coordinate coordinate, 
            Dictionary<Coordinate, MapNode> mapNodes
        ) {
            var possibleNeighbours = PossibleNeighboursFor(coordinate);
            var checkedNeighbours = new List<MapNode>();
            foreach (var neighbour in possibleNeighbours)
            {
                if (mapNodes.ContainsKey(neighbour))
                {
                    checkedNeighbours.Add(mapNodes[neighbour]);
                }
            }
            return checkedNeighbours;
        }

        IEnumerable<Coordinate> PossibleNeighboursFor(Coordinate coordinate)
        {
            if (coordinate.Y % 2 == 1)
            {
                return new[]
                {
                    new Coordinate {X = coordinate.X, Y = coordinate.Y + 1},
                    new Coordinate {X = coordinate.X + 1, Y = coordinate.Y},
                    new Coordinate {X = coordinate.X, Y = coordinate.Y - 1},
                    new Coordinate {X = coordinate.X - 1, Y = coordinate.Y - 1},
                    new Coordinate {X = coordinate.X - 1, Y = coordinate.Y},
                    new Coordinate {X = coordinate.X - 1, Y = coordinate.Y + 1}
                };
            }
            return new[]
            {
                new Coordinate {X = coordinate.X + 1, Y = coordinate.Y + 1},
                new Coordinate {X = coordinate.X + 1, Y = coordinate.Y},
                new Coordinate {X = coordinate.X + 1, Y = coordinate.Y - 1},
                new Coordinate {X = coordinate.X, Y = coordinate.Y - 1},
                new Coordinate {X = coordinate.X - 1, Y = coordinate.Y},
                new Coordinate {X = coordinate.X, Y = coordinate.Y + 1}
            };
        }

        int SelectWeightByType(TileType type)
        {
            return type switch
            {
                TileType.Grass => GameConstants.grassWeight,
                TileType.Forest => GameConstants.forestWeight,
                TileType.Desert => GameConstants.desertWeight,
                TileType.Mountain => GameConstants.mountainWeight,
                TileType.Water => GameConstants.waterWeight,
                _ => 0
            };
        }
    }
}