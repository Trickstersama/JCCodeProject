using System.Collections.Generic;
using Features.Gameplay.Domain.ValueObjects;

namespace Features.Gameplay.Domain.Infrastructure
{
    public interface IMapRepository
    {
        void LoadTiles(IEnumerable<MapTile> tiles);
        void SetStart(Coordinate coordinate);
        void SetGoal(Coordinate coordinate);
        void ResetNodes();
    }
}