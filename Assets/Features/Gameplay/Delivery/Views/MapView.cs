using System.Collections;
using System.Collections.Generic;
using Features.Gameplay.Domain.ValueObjects;
using Features.Gameplay.Infrastructure;
using UnityEngine;

namespace Features.Gameplay.Delivery.Views
{
    public class MapView : MonoBehaviour
    {
        [SerializeField] MapTileView tilePrefab;
        IEnumerable<MapTile> tiles;
        ICoordinateService coordinateService;

        public void Initialize(IEnumerable<MapTile> mapTile, ICoordinateService coordinateService)
        {
            tiles = mapTile;
            this.coordinateService = coordinateService;

            DrawTiles();
        }

        void DrawTiles()
        {
            foreach (var tile in tiles)
            {
                var xxx = Instantiate(tilePrefab, WorldPositionByCoordinate(tile.coordinate), Quaternion.identity);
            }
        }

        Vector3 WorldPositionByCoordinate(Coordinate tile)
        {
            var xxx = coordinateService;
            return new Vector3();
        }
    }
}
