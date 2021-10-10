using System.Collections.Generic;
using Features.Gameplay.Domain.Actions;
using Features.Gameplay.Domain.ValueObjects;

namespace Features.Gameplay.Delivery.Presenters
{
    public class mapPresenter
    {
        readonly StartGame startGame;
        public mapPresenter(
            IEnumerable<MapTile> tiles, 
            StartGame startGame
        ) {
            this.startGame = startGame;
            
            startGame.Do(tiles);
        }

    }
}