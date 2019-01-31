using GalaSoft.MvvmLight.Command;
using Joinup.Common.Models;
using Joinup.Common.Models.DatabaseModels;
using Joinup.Service;
using Joinup.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Joinup.ViewModels
{
    public class RemarkPlanViewModel:BaseViewModel
    {
        #region Attributes
        Plan plan;
        Remark remark;
        bool isEnabled;
        bool isRunning;
        #endregion
        #region Properties
        public Plan Plan
        {
            get
            {
                return plan;
            }
            set
            {
                plan = value;
                RaisePropertyChanged("Plan");
            }
        }
        public int Score
        {
            get
            {
                return remark.Score;
            }
            set
            {
                remark.Score = value;
                RaisePropertyChanged("Score");
            }
        }
        public string Comment
        {
            get
            {
                return remark.CommentText;
            }
            set
            {
                remark.CommentText = value;
                RaisePropertyChanged("Comment");
            }
        }
        public bool IsEnabled
        {
            get
            {
                return isEnabled;
            }
            set
            {
                isEnabled = value;
                RaisePropertyChanged("IsEnabled");
            }
        }
        public bool IsRunning
        {
            get
            {
                return isRunning;
            }
            set
            {
                isRunning = value;
                RaisePropertyChanged("IsRunning");
            }
        }
        #endregion
        #region Constructors
        public RemarkPlanViewModel()
        {
            remark = new Remark();
        }
        public override Task InitializeAsync(object navigationData)
        {
            var parameter = navigationData as Plan;
            if (parameter != null)
            {
                Plan = parameter;
            }
            return base.InitializeAsync(navigationData);
        }
        #endregion
        #region Commands
        public ICommand SendRemarkCommand
        {
            get
            {
                return new RelayCommand(SendRemark);
            }
        }
        #endregion
        #region Methods
        public void SendRemark()
        {
            if (string.IsNullOrEmpty(Comment))
            {
                ToastNotificationUtils.ShowErrorToastNotifications("Ups...Debes seleccionar una categoria");
                return;
            }
            else
            {
                remark.PlanId = plan.PlanId;
                remark.PlanName = plan.Name;
                remark.UserId = LoggedUser.Id;
                remark.UserDisplayName = LoggedUser.Name;
                remark.UserDisplayImage = LoggedUser.UserImage;
                remark.CommentDate = DateTime.Now;
                SaveRemark();
            }
        }

        private async void SaveRemark()
        {
            IsEnabled = false;
            IsRunning = true;
            var response = await DataService.GetInstance().SaveRemark(remark);

            if (!response.IsSuccess)
            {
                ToastNotificationUtils.ShowErrorToastNotifications("Ha habido un error en la publicación de la reseña. Intentelo de nuevo más tarde");
            }
            else
            {
                Remark remark = (Remark)response.Result;            
            }
            IsRunning = false;
            IsEnabled = true;
        }
        #endregion
    }
}
