using System.Collections.Generic;
using System.Linq;
using Features.Gameplay.Domain.ValueObjects;
using Features.Gameplay.Infrastructure;
using UniRx;
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
        //
        [SerializeField] Material pathMaterial;
        [SerializeField] Material startGoalMaterial;
        [SerializeField] Material DefaultTintMaterial;

        ICoordinateService coordinateService;
        Dictionary<TileType, Material> tileMaterials = new Dictionary<TileType, Material>();
        Dictionary<Coordinate, MapTileView> tileViews = new Dictionary<Coordinate, MapTileView>();
        List<Coordinate> tilesInPath = new List<Coordinate>();
        

        
        public readonly ISubject<MapTile> OnMapTileClicked = new Subject<MapTile>();

        public void Initialize(IEnumerable<MapTile> mapTile, ICoordinateService coordinateService)
        {
            this.coordinateService = coordinateService;
            FillPrefabsByType();
            
            CreateTiles(mapTile);
        }

        public void ResetNodes() => 
            ResetPathToDefaultMaterial();

        public void SetStart(Coordinate coordinate)
        {
            tilesInPath.Clear();
            tileViews[coordinate].ShowAs(startGoalMaterial);
            tilesInPath.Add(coordinate);
        }

        void CreateTiles(IEnumerable<MapTile> mapTiles)
        {
            foreach (var tile in mapTiles)
            {
                var newTile = Instantiate(
                    mapTilePrefab,
                    WorldPositionByCoordinate(tile.coordinate),
                    Quaternion.identity,
                    transform
                );
                newTile.SetMaterial(GetMaterialByType(tile.TileType));
                newTile.Initialize(tile);
                newTile.OnMapTileClicked.Subscribe(OnMapTileClicked);
                tileViews.Add(tile.coordinate, newTile);
            }
        }

        Material GetMaterialByType(TileType type) => 
            tileMaterials[type];

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

        public void DrawPath(IEnumerable<Coordinate> path)
        {
            ResetPathToDefaultMaterial();
            
            tilesInPath = path.ToList();
            var pathList = path.ToArray();
            tileViews[pathList[0]].ShowAs(startGoalMaterial);
            tileViews[pathList[pathList.Length - 1]].ShowAs(startGoalMaterial);
            for (var i = 1; i < pathList.Length-1; i++)
            {
                tileViews[pathList[i]].ShowAs(pathMaterial);
            }
        }

        void ResetPathToDefaultMaterial()
        {
            tilesInPath.ForEach(tile => tileViews[tile].ShowAs(DefaultTintMaterial));
            tilesInPath.Clear();
        }
    }
}
