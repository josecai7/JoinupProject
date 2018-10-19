using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;

namespace Joinup.ViewModels
{
    public class BaseViewModel:INotifyPropertyChanged
    {
        protected Page CurrentPage { get; private set; }
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName=null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected void SetValue<T>(ref T backingField, T value, [CallerMemberName]string propertyName=null)
        {
            if (EqualityComparer<T>.Default.Equals(backingField, value))
            {
                return;
            }
            backingField = value;
            this.OnPropertyChanged(propertyName);
        }
        public void Initialize(Page page)
        {
            CurrentPage = page;

            CurrentPage.Appearing += CurrentPageOnAppearing;
            CurrentPage.Disappearing += CurrentPageOnDisappearing;
        }

        protected virtual void CurrentPageOnAppearing(object sender, EventArgs eventArgs) { }

        protected virtual void CurrentPageOnDisappearing(object sender, EventArgs eventArgs) { }
    }
}
