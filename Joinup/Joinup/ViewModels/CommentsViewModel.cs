using GalaSoft.MvvmLight.Command;
using Joinup.Common.Models;
using Joinup.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading;
using System.Windows.Input;
using Xamarin.Forms;

namespace Joinup.ViewModels
{
    public class CommentsViewModel:BaseViewModel
    {
        private ApiService apiService;

        private ObservableCollection<Comment> commentList;

        public ObservableCollection<Comment> CommentList
        {
            get
            {
                return commentList;
            }
            set
            {
                SetValue(ref commentList, value);
            }
        }


        private bool isRefreshing;

        public bool IsRefreshing
        {
            get
            {
                return isRefreshing;
            }
            set
            {
                SetValue( ref isRefreshing, value );
            }
        }

        public CommentsViewModel()
        {
            apiService = new ApiService();
            LoadComments();
        }
        private async void LoadComments()
        {
            IsRefreshing = true;

            var connection = await apiService.CheckConnection();
            if (connection.IsSuccess )
            {
                //string url = Application.Current.Resources["UrlAPI"].ToString();

                var response = await apiService.GetList<Comment>("http://joinupapi.azurewebsites.net/", "/api", "/Comments" );

                if ( !response.IsSuccess )
                {
                    await Application.Current.MainPage.DisplayAlert( "Error", "Aceptar", "Cancelar" );
                    return;
                }

                var list = (List<Comment>) response.Result;
                CommentList = new ObservableCollection<Comment>( list );
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert( "Revisa tu conexión a internet", "Aceptar", "Cancelar" );
                return;
            }

            IsRefreshing = false;
        }

        public ICommand RefreshCommand
        {
            get
            {
                return new RelayCommand( LoadComments );
            }
        }

    }
}
