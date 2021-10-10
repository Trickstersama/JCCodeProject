using Features.Gameplay.Domain.Infrastructure;
using NSubstitute;

namespace Features.Gameplay.Tests.Mothers
{
    public static class MapRepositoryMother
    {
        public static IMapRepository AMapRepository(bool withStartSelected = false)
        {
            var repository = Substitute.For<IMapRepository>();
            repository.IsStartSelected().Returns(withStartSelected);
            return repository;
        }
    }
}