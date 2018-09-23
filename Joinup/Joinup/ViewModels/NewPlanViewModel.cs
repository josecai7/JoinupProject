using GalaSoft.MvvmLight.Command;
using Joinup.Common.Models;
using Joinup.Service;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Joinup.ViewModels
{
    public class NewPlanViewModel:BaseViewModel
    {
        #region Attributes
        private ApiService apiService;

        #endregion

        #region Properties

        public string Name { get; set; }
        public string Description { get; set; }

        #endregion

        #region Constructors

        public NewPlanViewModel()
        {
            apiService = new ApiService();

        }

        #endregion

        #region Commands

        public ICommand SavePlanCommand
        {
            get
            {
                return new RelayCommand(SavePlan);
            }
        }

        private void SavePlan()
        {
            Plan plan = new Plan();
            plan.PlanId = "0009";
            plan.Name = Name;
            plan.Description = Description;

            var url = Application.Current.Resources["UrlAPI"].ToString();
            var prefix = Application.Current.Resources["UrlPrefix"].ToString();
            var controller = Application.Current.Resources["UrlPlansController"].ToString();
            var response = apiService.Post<Plan>(url,prefix,controller,plan);



            Application.Current.MainPage.Navigation.PopAsync();
        }

        #endregion

    }
}
