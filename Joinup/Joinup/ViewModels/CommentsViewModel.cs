using GalaSoft.MvvmLight.Command;
using Joinup.Common.Models;
using Joinup.Common.Models.DatabaseModels;
using Joinup.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Joinup.ViewModels
{
    public class CommentsViewModel:BaseViewModel
    {
        #region Attributes
        private Plan plan;
        string commentText;
        ObservableCollection<Comment> comments = new ObservableCollection<Comment>();
        private bool isRefreshing;
        #endregion
        #region Properties
        public ObservableCollection<Comment> Comments
        {
            get
            {
                return new ObservableCollection<Comment>(comments.OrderBy(x => x.CommentDate));
            }
            set
            {
                comments = value;
                RaisePropertyChanged("Comments");
            }
        }
        public string CommentText
        {
            get
            {
                return commentText;
            }
            set
            {
                commentText = value;
                RaisePropertyChanged("CommentText");
            }
        }
        public Plan Plan
        {
            get
            {
                return plan;
            }
            set
            {
                plan = value;
                RaisePropertyChanged();
            }
        }
        public bool IsRefreshing
        {
            get
            {
                return isRefreshing;
            }
            set
            {
                isRefreshing = value;
                RaisePropertyChanged( "IsRefreshing" );
            }
        }
        #endregion
        #region Constructors
        public CommentsViewModel()
        {

        }
        public override Task InitializeAsync(object navigationData)
        {
            var parameter = (Plan)navigationData;

            Plan = parameter;

            LoadComments();

            return base.InitializeAsync(navigationData);
        }
        #endregion
        #region Commands

        public ICommand SendCommentCommand
        {
            get
            {
                return new RelayCommand(SendComment);
            }
        }

        public ICommand RefreshCommand
        {
            get
            {
                return new RelayCommand(LoadComments);
            }
        }

        #endregion
        #region Methods
        private async void SendComment()
        {
            if (!string.IsNullOrEmpty(CommentText))
            {
                Comment sendComment = new Comment();
                sendComment.CommentText = CommentText;
                sendComment.CommentDate = DateTime.Now;
                sendComment.UserDisplayImage = LoggedUser.UserImage;
                sendComment.UserDisplayName = LoggedUser.Name;
                sendComment.UserId = LoggedUser.Id;
                sendComment.PlanId = Plan.PlanId;

                var response = await DataService.GetInstance().SendComment(sendComment);

                if (!response.IsSuccess)
                {
                    await Application.Current.MainPage.DisplayAlert("Error al publicar el comentario", "Aceptar", "Cancelar");
                    IsRefreshing = false;
                    return;
                }

                CommentText = String.Empty;
                LoadComments();
            }
        }

        private async void LoadComments()
        {
            IsRefreshing = true;


            var response = await DataService.GetInstance().GetCommentsByPlan(plan.PlanId);

            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Aceptar", "Cancelar");
                IsRefreshing = false;
                return;
            }

            var list = (List<Comment>)response.Result;

            foreach (var comment in list)
            {
                comment.LoggedUserId = LoggedUser.Id;
            }

            Comments = new ObservableCollection<Comment>(list);
            IsRefreshing = false;
        }
        #endregion
     }
}
