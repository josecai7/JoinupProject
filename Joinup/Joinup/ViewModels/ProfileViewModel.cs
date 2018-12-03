using GalaSoft.MvvmLight.Command;
using Joinup.Common.Models;
using Joinup.Helpers;
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
    public class ProfileViewModel : BaseViewModel
    {
        #region Attributes
        private MyUserASP user;
        private bool plansTab;
        private bool tab2;
        #endregion
        #region Properties
        public ObservableCollection<Plan> AssistPlanList
        {
            get
            {
                return new ObservableCollection<Plan>( plans.Where( item => item.UserId == User.Id ).OrderBy( x => x.PlanDate ) );
            }
        }
        public ObservableCollection<Plan> PublishedPlanList
        {
            get
            {
                
                return new ObservableCollection<Plan>( plans.Where( item => item.AssistantUsers.Find( user => user.Id == LoggedUser.Id ) != null ).OrderBy( x => x.PlanDate ) );
            }
        }
        public bool PlansTab
        {
            get { return plansTab; }
            set
            {
                plansTab = value;
                RaisePropertyChanged();
            }
        }
        public bool Tab2
        {
            get { return tab2; }
            set
            {
                tab2 = value;
                RaisePropertyChanged();
            }
        }
        public MyUserASP User
        {
            get
            {
                return user;
            }
            set
            {
                user = value;
                RaisePropertyChanged();
            }
        }
        #endregion
        #region Constructors
        public ProfileViewModel()
        {
            user = LoggedUser;
            SetPlansTab();
        }

        public override Task InitializeAsync(object navigationData)
        {
            User = (MyUserASP) navigationData;

            return base.InitializeAsync( navigationData );
        }
        #endregion
        #region Commands
        public ICommand PlansCommand
        {
            get
            {
                return new RelayCommand(SetPlansTab);
            }
        }

        public ICommand Tab2Command
        {
            get
            {
                return new RelayCommand(SetTab2);
            }
        }
        #endregion
        #region Methods

        private void SetPlansTab()
        {
            PlansTab = true;
            Tab2 = false;
        }

        private void SetTab2()
        {
            PlansTab = false;
            Tab2 = true;
        }
        #endregion
    }
}