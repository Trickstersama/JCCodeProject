using System;
using System.Collections.Generic;
using Features.Gameplay.Delivery.Views;
using Features.Gameplay.Domain.Actions;
using Features.Gameplay.Domain.ValueObjects;
using Features.Gameplay.Infrastructure;
using UniRx;
using UnityEngine;

namespace Features.Gameplay.Delivery.Presenters
{
    public class mapPresenter
    {
        readonly ISubject<IEnumerable<MapTile>> onInitializeMap = new Subject<IEnumerable<MapTile>>();
        readonly ISubject<IEnumerable<MapTile>> onMapInitialized = new Subject<IEnumerable<MapTile>>();
        
        readonly StartGame startGame;
        readonly MapView mapView;
        readonly ICoordinateService coordinateService;

        public mapPresenter(
            IEnumerable<MapTile> tiles, 
            StartGame startGame,
            MapView mapView,
            ICoordinateService coordinateService
        ) {
            this.startGame = startGame;
            this.mapView = mapView;
            this.coordinateService = coordinateService;

            DoSubscriptions();
            onInitializeMap.OnNext(tiles);
        }


        IEnumerable<IDisposable> Disposables() => 
            new[]
            {
                OnInitializeMap,
                OnMapInitialized,
                OnMapTileClicked
            };

        IDisposable OnInitializeMap =>
            onInitializeMap
                .Do(tiles => startGame.Do(tiles: tiles))
                .Subscribe(onMapInitialized.OnNext);

        IDisposable OnMapInitialized =>
            onMapInitialized
                .Do(tiles => mapView.Initialize(tiles, coordinateService))
                .Subscribe();

        IDisposable OnMapTileClicked =>
            mapView.OnMapTileClicked
                .Subscribe(_ => Debug.Log("CLickeo esto " + _.coordinate));
        
        void DoSubscriptions() => 
            PrepareForDisposition(new CompositeDisposable(), Disposables());
        static void PrepareForDisposition(CompositeDisposable disposables, IEnumerable<IDisposable> subscriptions)
        {
            foreach (var subscription in subscriptions) subscription.AddTo(disposables);
        }

    }
}