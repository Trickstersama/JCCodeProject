using Features.Gameplay.Domain.Infrastructure;
using Features.Gameplay.Domain.ValueObjects;

namespace Features.Gameplay.Domain.Reactions
{
    public class SetGoalNode
    {
        readonly IMapRepository mapRepository;

        public SetGoalNode(IMapRepository mapRepository)
        {
            this.mapRepository = mapRepository;
        }

        public void Do(Coordinate coordinate)
        {
            mapRepository.SetGoal(coordinate);
        }
    }
}