using Features.Gameplay.Domain.ValueObjects;
using UnityEngine;

namespace Features.Gameplay.Delivery.Views
{
    public class MapTileView : MonoBehaviour
    {
        [SerializeField] MeshRenderer meshRenderer;
        MapTile mapTile;

        public void Initialize(MapTile mapTile)
        {
            this.mapTile = mapTile;
        }

        public void SetMaterial(Material material)
        {
            meshRenderer.material = material;
        }
    }
}
