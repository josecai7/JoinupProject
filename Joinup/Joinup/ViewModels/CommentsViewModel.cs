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
        private bool isRefreshing;
        #endregion
        #region Properties
        public ObservableCollection<Comment> Comments
        {
            get
            {
                plan.Comments.Select( c => { c.LoggedUserId = LoggedUser.Id; return c; } ).ToList();
                return new ObservableCollection<Comment>(plan.Comments.OrderBy(x => x.CommentDate));
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
            plan = new Plan();
        }
        public override Task InitializeAsync(object navigationData)
        {
            var parameter = (Plan)navigationData;

            Plan = parameter;

            RaisePropertyChanged( "Comments" );

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
                return new RelayCommand( ReloadPlan );
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
                sendComment.LoggedUserId = LoggedUser.Id;
                plan.Comments.Add(sendComment);
                RaisePropertyChanged("Comments");

                CommentText = String.Empty;

                var response = await DataService.GetInstance().SendComment(sendComment);

                if (!response.IsSuccess)
                {
                    plan.Comments.Remove( sendComment );
                    await Application.Current.MainPage.DisplayAlert("Error al publicar el comentario", "Aceptar", "Cancelar");
                    IsRefreshing = false;
                    return;
                }            
            }
        }

        private async void ReloadPlan()
        {
            IsRefreshing = true;


            var response = await DataService.GetInstance().GetPlan( plan.PlanId);

            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Aceptar", "Cancelar");
                IsRefreshing = false;
                return;
            }

            plan = (Plan)response.Result;

            RaisePropertyChanged( "Comments" );

            IsRefreshing = false;
        }
        #endregion
     }
}
