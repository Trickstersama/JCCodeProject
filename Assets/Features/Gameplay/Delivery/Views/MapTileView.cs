using Features.Gameplay.Domain.ValueObjects;
using UniRx;
using UnityEngine;

namespace Features.Gameplay.Delivery.Views
{
    public class MapTileView : MonoBehaviour
    {
        [SerializeField] MeshRenderer meshRenderer;
        [SerializeField] Material pathMaterial;
        [SerializeField] Material startGoalMaterial;
        [SerializeField] Material DefaultTintMaterial;

        public readonly ISubject<MapTile> OnMapTileClicked = new Subject<MapTile>();
        MapTile mapTile;

        public void Initialize(MapTile mapTile) => 
            this.mapTile = mapTile;

        public void SetMaterial(Material material) => 
            meshRenderer.material = material;
        void OnMouseDown() => 
            OnMapTileClicked.OnNext(mapTile);

        public void ShowAsPath()
        {
            Material[] matArray = meshRenderer.materials;
            matArray[1] = pathMaterial;
            meshRenderer.materials = matArray;
            meshRenderer.materials[1] = pathMaterial;
        }
    }
}
