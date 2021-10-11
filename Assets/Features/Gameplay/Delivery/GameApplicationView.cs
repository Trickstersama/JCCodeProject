using System.Collections.Generic;
using Features.Gameplay.Delivery.Views;
using Features.Gameplay.Domain.ValueObjects;
using Features.Gameplay.Infrastructure;
using UnityEngine;

namespace Features.Gameplay.Delivery
{
    public class GameApplicationView : MonoBehaviour
    {
        [SerializeField] MapView mapView;
        [SerializeField] MapSetup mapSetup;
        
        void Start()
        {
            Context.Initialize(
                LoadTiles(),
                mapView
            );
        }

        IEnumerable<MapTile> LoadTiles() => 
            mapSetup.ReadAllTiles();
    }
}
