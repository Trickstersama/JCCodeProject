using System;
using System.Collections.Generic;
using Features.Gameplay.Delivery.Views;
using Features.Gameplay.Domain.Actions;
using Features.Gameplay.Domain.Reactions;
using Features.Gameplay.Domain.ValueObjects;
using Features.Gameplay.Infrastructure;
using UniRx;
using UnityEngine;

namespace Features.Gameplay.Delivery.Presenters
{
    public class MapPresenter
    {
        readonly ISubject<IEnumerable<MapTile>> onInitializeMap = new Subject<IEnumerable<MapTile>>();
        readonly ISubject<IEnumerable<MapTile>> onMapInitialized = new Subject<IEnumerable<MapTile>>();

        //pre
        readonly ISubject<IGameEvent> onResetNodes = new Subject<IGameEvent>();
        readonly ISubject<Coordinate> onSetGoal = new Subject<Coordinate>();
        
        //post
        readonly ISubject<IGameEvent> onGoalSet = new Subject<IGameEvent>();
        readonly ISubject<IGameEvent> onPathNodesReset = new Subject<IGameEvent>();
        
        readonly StartGame startGame;
        readonly MapView mapView;
        readonly ICoordinateService coordinateService;
        readonly ClickMapTile clickMapTile;
        readonly ResetPathNodes resetPathNodes;
        readonly SetGoalNode setGoalNode;

        public MapPresenter(IEnumerable<MapTile> tiles,
            StartGame startGame,
            MapView mapView,
            ICoordinateService coordinateService,
            ClickMapTile clickMapTile,
            ResetPathNodes resetPathNodes, 
            SetGoalNode setGoalNode
        ) {
            this.startGame = startGame;
            this.mapView = mapView;
            this.coordinateService = coordinateService;
            this.clickMapTile = clickMapTile;
            this.resetPathNodes = resetPathNodes;
            this.setGoalNode = setGoalNode;

            DoSubscriptions();
            onInitializeMap.OnNext(tiles);
        }


        IEnumerable<IDisposable> Disposables() => 
            new[]
            {
                OnInitializeMap,
                OnMapInitialized,
                OnMapTileClicked,
                OnResetNodes,
                OnNodeReset,
                OnSetGoal,
                OnGoalSet
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
                .Do(mapTile => clickMapTile.Do(mapTile.coordinate, onResetNodes, onSetGoal) )
                .Subscribe();

        IDisposable OnSetGoal =>
            onSetGoal
                .Do(coordinate => setGoalNode.Do(coordinate, onGoalSet))
                .Subscribe();

        IDisposable OnResetNodes =>
            onResetNodes
                .Do(_ => resetPathNodes.Do(onPathNodesReset))
                .Subscribe();

        IDisposable OnGoalSet =>
            onGoalSet
                .Do(_ => Debug.Log("Goal Set, looking for path"))
                .Subscribe();

        IDisposable OnNodeReset =>
            onPathNodesReset
                .Do(_ => Debug.Log("Reset nodes"))
                .Subscribe();
            
        void DoSubscriptions() => 
            PrepareForDisposition(new CompositeDisposable(), Disposables());
        static void PrepareForDisposition(CompositeDisposable disposables, IEnumerable<IDisposable> subscriptions)
        {
            foreach (var subscription in subscriptions) subscription.AddTo(disposables);
        }
    }
}