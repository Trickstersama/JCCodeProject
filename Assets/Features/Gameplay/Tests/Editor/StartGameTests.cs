using System.Collections.Generic;
using System.Linq;
using Features.Gameplay.Domain.Actions;
using Features.Gameplay.Domain.Infrastructure;
using Features.Gameplay.Domain.ValueObjects;
using NSubstitute;
using NUnit.Framework;

namespace Features.Gameplay.Tests.Editor

{
    [TestFixture]
    public class StartGameTests
    {
        [Test]
        public void StartGameLoadsMapRepository()
        {
            //Given
            var mapRepository = Substitute.For<IMapRepository>();
            var startGame = new StartGame(mapRepository);
            

            //When
            startGame.Do(Enumerable.Empty<MapTile>());

            //Then

            mapRepository.Received(1).LoadTiles(Arg.Any<IEnumerable<MapTile>>());
        }
    }
}