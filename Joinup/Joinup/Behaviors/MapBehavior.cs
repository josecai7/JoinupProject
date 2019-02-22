using Joinup.Common.Models.DatabaseModels;
using Joinup.Common.Models.SelectablesModels;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace Joinup.Behaviors
{
    public class MapBehavior : BindableBehavior<Map>
    {
        private Map _map;

        public static readonly BindableProperty ItemsSourceProperty =
            BindableProperty.CreateAttached( "ItemsSource", typeof( IEnumerable<Plan> ), typeof( MapBehavior ),
                default( IEnumerable<Plan> ), BindingMode.Default, null, OnItemsSourceChanged );

        public IEnumerable<Plan> ItemsSource
        {
            get { return (IEnumerable<Plan>) GetValue( ItemsSourceProperty ); }
            set { SetValue( ItemsSourceProperty, value ); }
        }

        private static void OnItemsSourceChanged(BindableObject view, object oldValue, object newValue)
        {
            var mapBehavior = view as MapBehavior;

            if ( mapBehavior != null )
            {
                mapBehavior.AddPins();
                mapBehavior.PositionMap();
            }
        }

        protected override void OnAttachedTo(Map bindable)
        {
            base.OnAttachedTo( bindable );

            _map = bindable;
        }

        protected override void OnDetachingFrom(Map bindable)
        {
            base.OnDetachingFrom( bindable );

            _map = null;
        }

        private void AddPins()
        {
            for ( int i = _map.Pins.Count - 1 ; i >= 0 ; i-- )
            {
                _map.Pins.RemoveAt( i );
            }

            var pins = new List<Pin>();

            foreach (Plan plan in ItemsSource)
            {
                var originPin = new Pin
                {
                    Type = PinType.SearchResult,
                    Position = new Position( plan.Latitude, plan.Longitude ),
                    Label = plan.Name == null ? "Nombre de plan por definir" : plan.Name,
                };
                if (originPin.Position.Latitude != 0 && originPin.Position.Longitude != 0)
                {
                    pins.Add( originPin );
                }
                if (plan.PlanType == PLANTYPE.TRAVEL)
                {
                    var destinationPin = new Pin
                    {
                        Type = PinType.SearchResult,
                        Position = new Position( plan.DestinationLatitude, plan.DestinationLongitude ),
                        Label = plan.Name == null ? "Nombre de plan por definir" : plan.Name,
                    };
                    if (destinationPin.Position.Latitude != 0 && destinationPin.Position.Longitude != 0)
                    {
                        pins.Add( destinationPin );
                    }
                }
            }

            foreach ( var pin in pins )
                _map.Pins.Add( pin );
        }

        private void PositionMap()
        {
            if ( ItemsSource == null || !ItemsSource.Any() )
                return;

            List<Position> positions = new List<Position>();

            foreach (Pin pin in _map.Pins)
            {
                positions.Add( pin.Position );
            }


            MapSpan mapSpan = FromPositions( positions );

            _map.MoveToRegion( mapSpan );

            Device.StartTimer( TimeSpan.FromMilliseconds( 500 ), () =>
            {
                _map.MoveToRegion( mapSpan );
                return false;
            } );
        }
        private static MapSpan FromPositions(IEnumerable<Position> positions)
        {
            double minLat = double.MaxValue;
            double minLon = double.MaxValue;
            double maxLat = double.MinValue;
            double maxLon = double.MinValue;

            foreach (var p in positions)
            {
                minLat = Math.Min( minLat, p.Latitude );
                minLon = Math.Min( minLon, p.Longitude );
                maxLat = Math.Max( maxLat, p.Latitude );
                maxLon = Math.Max( maxLon, p.Longitude );
            }

            return new MapSpan(
                new Position( (minLat + maxLat) / 2d, (minLon + maxLon) / 2d ),
                (maxLat - minLat)+0.2,
                (maxLon - minLon ) + 0.2 );
        }
    }
}