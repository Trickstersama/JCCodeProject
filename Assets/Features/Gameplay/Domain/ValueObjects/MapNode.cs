using System;
using System.Collections.Generic;
using System.Linq;
using PathFinding;

#pragma warning disable 659

namespace Features.Gameplay.Domain.ValueObjects
{
    public class MapNode : IAStarNode
    {
        int weight;
        List<MapNode> neighbours;
        Coordinate coordinate;

        public MapNode(Coordinate coordinate, int weight = 0)
        {
            this.coordinate = coordinate;
            this.weight = weight;
            neighbours = new List<MapNode>();
        }
        
        public void SetNeighbours(IEnumerable<MapNode> neighbours)
        {
            this.neighbours = neighbours.ToList();
        }

        public IEnumerable<IAStarNode> Neighbours => neighbours; //referencia a nodos adyacentes
        public float CostTo(IAStarNode neighbour) //el costo en distancia con respecto a los vecinos
        {
            var xxx = (MapNode) neighbour;
            return xxx.weight;
        }

        public float EstimatedCostTo(IAStarNode target)
        {
            var targetNode = (MapNode) target;
            var actualPosition = calculatePosition(Coordinate());
            var targetPosition = calculatePosition(targetNode.coordinate);
            return (float) Math.Sqrt(Math.Pow(Math.Abs(actualPosition.X - targetPosition.X), 2) +
                             Math.Pow(Math.Abs(actualPosition.Y - targetPosition.Y), 2));
        }

        public override bool Equals(object obj)
        {
            if (!(obj is MapNode)) return false;
            var node = (MapNode) obj;
            if (weight == node.weight &&
                coordinate.Equals(node.coordinate))
            {
                return true;
            }
            return false;
        }

        public override string ToString()
        {
            return $"MapNode -> weight {weight}";
        }

        public Coordinate Coordinate() => 
            coordinate;
        
        WorldCoordinate calculatePosition(Coordinate coordinate)
        {
            if (coordinate.Y % 2 == 0)
            {
                return new WorldCoordinate
                {
                    X = coordinate.X,
                    Y = coordinate.Y * .75f
                };
            }

            return new WorldCoordinate
            {
                X = coordinate.X - .5f,
                Y = coordinate.Y * .75f
            };
        }

    }
}