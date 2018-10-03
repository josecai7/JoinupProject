using Joinup.Common.Models;
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

            var pins = ItemsSource.Select( x =>
            {
                var pin = new Pin
                {
                    Type = PinType.SearchResult,
                    Position = new Position( x.Latitude, x.Longitude ),
                    Label = x.Name
                };

                return pin;
            } ).ToArray();

            foreach ( var pin in pins )
                _map.Pins.Add( pin );
        }

        private void PositionMap()
        {
            if ( ItemsSource == null || !ItemsSource.Any() )
                return;

            var centerPosition = new Position( ItemsSource.Average( x => x.Latitude ), ItemsSource.Average( x => x.Longitude ) );

            var distance = 0.2;

            _map.MoveToRegion( MapSpan.FromCenterAndRadius( centerPosition, Distance.FromMiles( distance ) ) );

            Device.StartTimer( TimeSpan.FromMilliseconds( 500 ), () =>
            {
                _map.MoveToRegion( MapSpan.FromCenterAndRadius( centerPosition, Distance.FromMiles( distance ) ) );
                return false;
            } );
        }
    }
}