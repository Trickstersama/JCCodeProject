using Features.Gameplay.Domain.Infrastructure;
using Features.Gameplay.Domain.ValueObjects;
using NSubstitute;

namespace Features.Gameplay.Tests.Mothers
{
    public static class MapServiceMother
    {
        public static IMapService AMapService(
            bool withCoordinateIsStart = true,
            bool withStartIsNotSelected = true    
        ) {
            var service = Substitute.For<IMapService>();
            service.CoordinateIsStart(Arg.Any<Coordinate>()).Returns(withCoordinateIsStart);
            service.StartIsNotSelected().Returns(withStartIsNotSelected);
            return service;
        }
    }
}