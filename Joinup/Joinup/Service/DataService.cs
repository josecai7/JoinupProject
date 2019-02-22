using Joinup.Common.Models;
using Joinup.Common.Models.DatabaseModels;
using Joinup.Helpers;
using Joinup.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public async Task<Response> JoinAPlan(int pPlanId, string pUserId, bool pIsHost)
        {
            var url = Application.Current.Resources["UrlAPI"].ToString();
            var prefix = Application.Current.Resources["UrlPrefix"].ToString();
            var controller = Application.Current.Resources["UrlMeetsController"].ToString();

            Meet meet = new Meet();
            meet.PlanId = pPlanId;
            meet.UserId = pUserId;
            meet.IsHost = pIsHost;

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
        public async Task<Response> SavePlan(Plan pPlan, ObservableCollection<Common.Models.DatabaseModels.Image> pImages)
        {
            var url = Application.Current.Resources["UrlAPI"].ToString();
            var prefix = Application.Current.Resources["UrlPrefix"].ToString();
            var controller = Application.Current.Resources["UrlPlansController"].ToString();

            //Save plan
            var response = await ApiService.GetInstance().Post<Plan>(url, prefix, controller, pPlan, Settings.TokenType, Settings.AccessToken);

            if (response.IsSuccess)
            {
                Plan plan = (Plan)response.Result;
                foreach (Common.Models.DatabaseModels.Image image in pImages)
                {
                    image.PlanId = plan.PlanId;
                    var imageResponse = await SaveImage(image);
                    if (!imageResponse.IsSuccess)
                    {
                        return response;
                    }
                }
            }
            
            return response;
        }
        public async Task<Response> EditPlan(Plan pPlan)
        {
            var url = Application.Current.Resources["UrlAPI"].ToString();
            var prefix = Application.Current.Resources["UrlPrefix"].ToString();
            var controller = Application.Current.Resources["UrlPlansController"].ToString();

            //Save plan
            var response = await ApiService.GetInstance().Put<Plan>(url, prefix, controller, pPlan,pPlan.PlanId, Settings.TokenType, Settings.AccessToken);

            if (response.IsSuccess)
            {
                Plan plan = (Plan)response.Result;               
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
        public async Task<Response> SaveImage(Common.Models.DatabaseModels.Image pImage)
        {
            var url = Application.Current.Resources["UrlAPI"].ToString();
            var prefix = Application.Current.Resources["UrlPrefix"].ToString();
            var controller = Application.Current.Resources["UrlImagesController"].ToString();

            //Save plan
            var response = await ApiService.GetInstance().Post<Common.Models.DatabaseModels.Image>(url, prefix, controller, pImage, Settings.TokenType, Settings.AccessToken);

            return response;
        }
        public async Task<Response> SaveRemark(Remark pRemark)
        {
            var url = Application.Current.Resources["UrlAPI"].ToString();
            var prefix = Application.Current.Resources["UrlPrefix"].ToString();
            var controller = Application.Current.Resources["UrlRemarksController"].ToString();

            //Save remark
            var response = await ApiService.GetInstance().Post<Remark>(url, prefix, controller, pRemark, Settings.TokenType, Settings.AccessToken);

            return response;
        }
        public async Task<Response> GetPlan(int pPlanId)
        {
            var url = Application.Current.Resources["UrlAPI"].ToString();
            var prefix = Application.Current.Resources["UrlPrefix"].ToString();
            var controller = Application.Current.Resources["UrlPlansController"].ToString();

            var response = await ApiService.GetInstance().GetList<Plan>( url, prefix, controller,pPlanId, Settings.TokenType, Settings.AccessToken );
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
        public async Task<Response> GetPlansByUserId(string pUserId)
        {
            var url = Application.Current.Resources["UrlAPI"].ToString();
            var prefix = Application.Current.Resources["UrlPrefix"].ToString();
            var controller = Application.Current.Resources["UrlPlansController"].ToString();
            controller += "/ByUser/" + pUserId;

            var response = await ApiService.GetInstance().GetList<Plan>(url, prefix, controller, Settings.TokenType, Settings.AccessToken);

            return response;
        }
        public async Task<Response> GetRemarksByUserId(string pUserId)
        {
            var url = Application.Current.Resources["UrlAPI"].ToString();
            var prefix = Application.Current.Resources["UrlPrefix"].ToString();
            var controller = Application.Current.Resources["UrlRemarksController"].ToString();
            controller += "/ByUser/" + pUserId;

            var response = await ApiService.GetInstance().GetList<Remark>(url, prefix, controller, Settings.TokenType, Settings.AccessToken);

            return response;
        }
        public async Task<Response> SendComment(Comment pComment)
        {
            var url = Application.Current.Resources["UrlAPI"].ToString();
            var prefix = Application.Current.Resources["UrlPrefix"].ToString();
            var controller = Application.Current.Resources["UrlCommentsController"].ToString();

            var response = await ApiService.GetInstance().Post<Comment>(url, prefix, controller, pComment, Settings.TokenType, Settings.AccessToken);

            return response;
        }
        public async Task<Response> PostUser(string pUserId, string pEmail, string pName, string pSurname, string pImagePath)
        {
            User user = new User();
            user.UserId = pUserId;
            user.Name = pName;
            user.Surname = pSurname;
            user.Email = pEmail;
            user.Image = pImagePath;
            var url = Application.Current.Resources["UrlAPI"].ToString();
            var prefix = Application.Current.Resources["UrlPrefix"].ToString();
            var controller = Application.Current.Resources["UrlUsers1Controller"].ToString();

            var response = await ApiService.GetInstance().Post<User>(url, prefix, controller, user, Settings.TokenType, Settings.AccessToken);

            return response;
        }
        public async Task<Response> GetMeetsByPlan(int pPlanId)
        {
            var url = Application.Current.Resources["UrlAPI"].ToString();
            var prefix = Application.Current.Resources["UrlPrefix"].ToString();
            var controller = Application.Current.Resources["UrlMeetsController"].ToString();
            controller += "/ByPlan/" + pPlanId;

            var response = await ApiService.GetInstance().GetList<Meet>( url, prefix, controller, Settings.TokenType, Settings.AccessToken );

            return response;
        }

        public async Task<Response> GetSkillLevels()
        {
            var url = Application.Current.Resources["UrlAPI"].ToString();
            var prefix = Application.Current.Resources["UrlPrefix"].ToString();
            var controller = Application.Current.Resources["UrlSkillLevelsController"].ToString();

            var response = await ApiService.GetInstance().GetList<SkillLevel>( url, prefix, controller, Settings.TokenType, Settings.AccessToken );

            return response;
        }

        public async Task<Response> GetSports()
        {
            var url = Application.Current.Resources["UrlAPI"].ToString();
            var prefix = Application.Current.Resources["UrlPrefix"].ToString();
            var controller = Application.Current.Resources["UrlSportsController"].ToString();

            var response = await ApiService.GetInstance().GetList<Sport>( url, prefix, controller, Settings.TokenType, Settings.AccessToken );

            return response;
        }

        public async Task<Response> GetLanguages()
        {
            var url = Application.Current.Resources["UrlAPI"].ToString();
            var prefix = Application.Current.Resources["UrlPrefix"].ToString();
            var controller = Application.Current.Resources["UrlLanguagesController"].ToString();

            var response = await ApiService.GetInstance().GetList<Language>( url, prefix, controller, Settings.TokenType, Settings.AccessToken );

            return response;
        }

        public async Task<Response> GetFoodTypes()
        {
            var url = Application.Current.Resources["UrlAPI"].ToString();
            var prefix = Application.Current.Resources["UrlPrefix"].ToString();
            var controller = Application.Current.Resources["UrlFoodTypesController"].ToString();

            var response = await ApiService.GetInstance().GetList<FoodType>( url, prefix, controller, Settings.TokenType, Settings.AccessToken );

            return response;
        }

    }
}
