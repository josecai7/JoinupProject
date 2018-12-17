using Joinup.Common.Models;
using Joinup.Common.Models.DatabaseModels;
using Joinup.Helpers;
using Joinup.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Joinup.Service
{
    public class DataService
    {
        #region Singleton

        private static DataService instance;

        public static DataService GetInstance()
        {
            if (instance == null)
            {
                return new DataService();
            }
            else
            {
                return instance;
            }
        }
        #endregion
        public async Task<Response> JoinAPlan(int pPlanId, string pUserId)
        {
            var url = Application.Current.Resources["UrlAPI"].ToString();
            var prefix = Application.Current.Resources["UrlPrefix"].ToString();
            var controller = Application.Current.Resources["UrlMeetsController"].ToString();

            Meet meet = new Meet();
            meet.PlanId = pPlanId;
            meet.UserId = pUserId;

            //Save meet
            var response = await ApiService.GetInstance().Post<Meet>(url, prefix, controller, meet, Settings.TokenType, Settings.AccessToken);

            return response;
        }
        public async Task<Response> UnJoinAPlan(int pPlanId, string pUserId)
        {
            var url = Application.Current.Resources["UrlAPI"].ToString();
            var prefix = Application.Current.Resources["UrlPrefix"].ToString();
            var controller = "/Meets/DeleteMeet";

            Meet meet = new Meet();
            meet.PlanId = pPlanId;
            meet.UserId = pUserId;

            //Delete meet
            var response = await ApiService.GetInstance().Post<Meet>(url, prefix, controller, meet, Settings.TokenType, Settings.AccessToken);

            return response;
        }
        public async Task<Response> SavePlan(Plan pPlan)
        {
            var url = Application.Current.Resources["UrlAPI"].ToString();
            var prefix = Application.Current.Resources["UrlPrefix"].ToString();
            var controller = Application.Current.Resources["UrlPlansController"].ToString();

            //Save plan
            var response = await ApiService.GetInstance().Post<Plan>(url, prefix, controller, pPlan, Settings.TokenType, Settings.AccessToken);

            if (response.IsSuccess)
            {
                Plan plan = (Plan)response.Result;
                foreach (Common.Models.Image image in pPlan.PlanImages)
                {
                    image.EntityId = plan.PlanId;
                    var imageResponse = await SaveImage(image);
                    if (!imageResponse.IsSuccess)
                    {
                        return response;
                    }
                }
            }
            
            return response;
        }   
        public async Task<Response> Cancel(Plan pPlan)
        {
            var url = Application.Current.Resources["UrlAPI"].ToString();
            var prefix = Application.Current.Resources["UrlPrefix"].ToString();
            var controller = Application.Current.Resources["UrlPlansController"].ToString();

            //Save plan
            var response = await ApiService.GetInstance().Delete(url, prefix, controller, pPlan.PlanId, Settings.TokenType, Settings.AccessToken);

            if (response.IsSuccess)
            {
                //Revisar eliminacion en cascada
            }

            return response;
        }
        public async Task<Response> SaveImage(Common.Models.Image pImage)
        {
            var url = Application.Current.Resources["UrlAPI"].ToString();
            var prefix = Application.Current.Resources["UrlPrefix"].ToString();
            var controller = Application.Current.Resources["UrlImagesController"].ToString();

            //Save plan
            var response = await ApiService.GetInstance().Post<Common.Models.Image>(url, prefix, controller, pImage, Settings.TokenType, Settings.AccessToken);

            return response;
        }

        public async Task<Response> GetPlans()
        {
            var url = Application.Current.Resources["UrlAPI"].ToString();
            var prefix = Application.Current.Resources["UrlPrefix"].ToString();
            var controller = Application.Current.Resources["UrlPlansController"].ToString();

            var response = await ApiService.GetInstance().GetList<Plan>( url, prefix, controller, Settings.TokenType, Settings.AccessToken );

            return response;
        }
        public async Task<Response> GetCommentsByPlan(int pPlanId)
        {
            var url = Application.Current.Resources["UrlAPI"].ToString();
            var prefix = Application.Current.Resources["UrlPrefix"].ToString();
            var controller = Application.Current.Resources["UrlCommentsController"].ToString();
            controller+="/ByPlan/"+pPlanId;

            var response = await ApiService.GetInstance().GetList<Comment>(url, prefix, controller, Settings.TokenType, Settings.AccessToken);

            return response;
        }



    }
}
