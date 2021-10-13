using Features.Gameplay.Domain.Infrastructure;
using Features.Gameplay.Domain.ValueObjects;
using NSubstitute;

namespace Features.Gameplay.Tests.Mothers
{
    public static class MapRepositoryMother
    {
        public static IMapRepository AMapRepository(
            bool withStartSelected = false,
            bool withCoordinateIsWalkable = true
        ) {
            var repository = Substitute.For<IMapRepository>();
            repository.IsStartSelected().Returns(withStartSelected);
            repository.IsWalkable(Arg.Any<Coordinate>()).Returns(withCoordinateIsWalkable);
            return repository;
        }
    }
}