using Joinup.Common.Models;
using Joinup.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
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

        public CommentsViewModel()
        {
            apiService = new ApiService();
            LoadComments();
        }
        private async void LoadComments()
        {
            var response = await apiService.GetList<Comment>("http://apijoinup.azurewebsites.net", "/api","/Comments");

            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert("Error","Aceptar","Cancelar");
                return;
            }

            var list = (List<Comment>)response.Result;
            CommentList = new ObservableCollection<Comment>(list);



        }

    }
}
