using System;
using System.Collections.Generic;
using PathFinding;

namespace Features.Gameplay.Domain.ValueObjects
{
    public class MapNode : IAStarNode
    {
        public IEnumerable<IAStarNode> Neighbours { get; }
        public float CostTo(IAStarNode neighbour)
        {
            throw new NotImplementedException();
        }

        public float EstimatedCostTo(IAStarNode target)
        {
            throw new NotImplementedException();
        }
    }
}