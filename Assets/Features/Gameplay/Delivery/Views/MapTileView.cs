using Features.Gameplay.Domain.ValueObjects;
using UniRx;
using UnityEngine;

namespace Features.Gameplay.Delivery.Views
{
    public class MapTileView : MonoBehaviour
    {
        [SerializeField] MeshRenderer meshRenderer;


        public readonly ISubject<MapTile> OnMapTileClicked = new Subject<MapTile>();
        MapTile mapTile;

        public void Initialize(MapTile mapTile) => 
            this.mapTile = mapTile;

        public void SetMaterial(Material material) => 
            meshRenderer.material = material;
        void OnMouseDown() => 
            OnMapTileClicked.OnNext(mapTile);

        public void ShowAs(Material material)
        {
            Material[] matArray = meshRenderer.materials;
            matArray[1] = material;
            meshRenderer.materials = matArray;
            meshRenderer.materials[1] = material;
        }
    }
}
