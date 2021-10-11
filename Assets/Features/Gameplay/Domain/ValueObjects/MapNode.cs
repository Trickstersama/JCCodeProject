using System;
using System.Collections.Generic;
using PathFinding;

namespace Features.Gameplay.Domain.ValueObjects
{
    public class MapNode : IAStarNode
    {
        int weight;
        public IEnumerable<IAStarNode> Neighbours { get; } //referencia a nodos adyacentes
        public float CostTo(IAStarNode neighbour) //el costo en distancia con respecto a los vecinos
        {
            throw new NotImplementedException(); 
        }

        public float EstimatedCostTo(IAStarNode target)
        {
            throw new NotImplementedException();
        }
    }
}