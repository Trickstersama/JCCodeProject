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
        //pre
        readonly ISubject<IEnumerable<MapTile>> onCreateNodes = new Subject<IEnumerable<MapTile>>();
        readonly ISubject<IGameEvent> onResetNodes = new Subject<IGameEvent>();
        readonly ISubject<Coordinate> onSetGoal = new Subject<Coordinate>();
        
        //post
        readonly ISubject<IEnumerable<MapTile>> onNodesCreated = new Subject<IEnumerable<MapTile>>();
        readonly ISubject<IGameEvent> onGoalSet = new Subject<IGameEvent>();
        readonly ISubject<Coordinate> onStartSet = new Subject<Coordinate>();
        readonly ISubject<IGameEvent> onPathNodesReset = new Subject<IGameEvent>();
        
        readonly CreateNodes createNodes;
        readonly MapView mapView;
        readonly ICoordinateService coordinateService;
        readonly ClickMapTile clickMapTile;
        readonly ResetPathNodes resetPathNodes;
        readonly SetGoalNode setGoalNode;

        public MapPresenter(
            IEnumerable<MapTile> tiles,
            CreateNodes createNodes,
            MapView mapView,
            ICoordinateService coordinateService,
            ClickMapTile clickMapTile,
            ResetPathNodes resetPathNodes, 
            SetGoalNode setGoalNode
        ) {
            this.createNodes = createNodes;
            this.mapView = mapView;
            this.coordinateService = coordinateService;
            this.clickMapTile = clickMapTile;
            this.resetPathNodes = resetPathNodes;
            this.setGoalNode = setGoalNode;

            PrepareForDisposition(new CompositeDisposable(),Disposables());
            onCreateNodes.OnNext(tiles);
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
                OnGoalSet,
                OnStartSet
            };

        IDisposable OnInitializeMap =>
            onCreateNodes
                .Do(tiles => createNodes.Do(tiles: tiles, onNodesCreated))
                .Subscribe();

        IDisposable OnMapInitialized =>
            onNodesCreated
                .Do(tiles => mapView.Initialize(tiles, coordinateService))
                .Subscribe();

        IDisposable OnMapTileClicked =>
            mapView.OnMapTileClicked
                .Do(mapTile => clickMapTile.Do(
                    mapTile.coordinate, 
                    onResetNodes, 
                    onSetGoal, 
                    onStartSet)
                )
                .Subscribe();

        IDisposable OnSetGoal =>
            onSetGoal
                .Do(coordinate => setGoalNode.Do(coordinate, onGoalSet))
                .Subscribe();

        IDisposable OnStartSet =>
            onStartSet
                .Do(mapView.SetStart)
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
                .Do(_ => mapView.ResetNodes())
                .Subscribe();
        
        static void PrepareForDisposition(CompositeDisposable disposables, IEnumerable<IDisposable> subscriptions)
        {
            foreach (var subscription in subscriptions) subscription.AddTo(disposables);
        }
    }
}