using Features.Gameplay.Delivery.Views;
using UnityEngine;

namespace Features.Gameplay.Delivery
{
    public class GameApplicationView : MonoBehaviour
    {
        [SerializeField] MapView mapView;
        void Start()
        {
            LoadTiles();
            Context.Initialize();
        }

        void LoadTiles()
        {
        }

    }
}
