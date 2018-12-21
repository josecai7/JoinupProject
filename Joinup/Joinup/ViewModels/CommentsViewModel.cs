using Joinup.Common.Models;
using Joinup.Common.Models.DatabaseModels;
using Joinup.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Joinup.ViewModels
{
    public class CommentsViewModel:BaseViewModel
    {
        #region Attributes
        private Plan plan;
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
        #endregion
        #region Methods
        private async void LoadComments()
        {
            var response = await DataService.GetInstance().GetCommentsByPlan(plan.PlanId);

            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Aceptar", "Cancelar");
                IsRefreshing = false;
                return;
            }

            var list = (List<Comment>)response.Result;
            Comments = new ObservableCollection<Comment>(list);
        }
        #endregion
     }
}
