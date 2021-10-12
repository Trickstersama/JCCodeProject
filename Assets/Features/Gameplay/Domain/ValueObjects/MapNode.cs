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
            throw new NotImplementedException(); 
        }

        public float EstimatedCostTo(IAStarNode target)
        {
            throw new NotImplementedException();
        }

        public override bool Equals(object obj)
        {
            if (!(obj is MapNode)) return false;
            var node = (MapNode) obj;
            if (weight == node.weight)
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
    }
}