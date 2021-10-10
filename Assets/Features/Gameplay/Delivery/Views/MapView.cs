using System.Collections.Generic;
using Features.Gameplay.Domain.ValueObjects;
using Features.Gameplay.Infrastructure;
using UnityEngine;

namespace Features.Gameplay.Delivery.Views
{
    public class MapView : MonoBehaviour
    {
        [SerializeField] MapTileView mapTilePrefab;
        [Header("Materials")]
        [SerializeField] Material grassMaterial;
        [SerializeField] Material forestMaterial;
        [SerializeField] Material desertMaterial;
        [SerializeField] Material mountainMaterial;
        [SerializeField] Material waterMaterial;
        
        IEnumerable<MapTile> tiles;
        ICoordinateService coordinateService;

        Dictionary<TileType, Material> tileMaterials = new Dictionary<TileType, Material>();
        
        
        public void Initialize(IEnumerable<MapTile> mapTile, ICoordinateService coordinateService)
        {
            tiles = mapTile;
            this.coordinateService = coordinateService;
            FillPrefabsByType();
            
            DrawTiles();
        }

        void DrawTiles()
        {
            foreach (var tile in tiles)
            {
                var newTile = Instantiate(
                    mapTilePrefab,
                    WorldPositionByCoordinate(tile.coordinate),
                    Quaternion.identity,
                    transform
                );
                newTile.SetMaterial(GetMaterialByType(tile.TileType));
                newTile.Initialize(tile);
            }
        }

        Material GetMaterialByType(TileType type)
        {
            return tileMaterials[type];
        }

        Vector3 WorldPositionByCoordinate(Coordinate coordinate)
        {
            var worldPosition = coordinateService.ToWorldPosition(coordinate);
            return new Vector3(worldPosition.X, transform.position.y, worldPosition.Y);
        }

        void FillPrefabsByType()
        {
            tileMaterials.Add(TileType.Grass, grassMaterial);
            tileMaterials.Add(TileType.Forest, forestMaterial);
            tileMaterials.Add(TileType.Desert, desertMaterial);
            tileMaterials.Add(TileType.Mountain, mountainMaterial);
            tileMaterials.Add(TileType.Water, waterMaterial);
        }
    }
}
