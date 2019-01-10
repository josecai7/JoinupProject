using GalaSoft.MvvmLight.Command;
using Joinup.Common.Models;
using Joinup.Helpers;
using Joinup.Views;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Joinup.ViewModels
{
    public class MainViewModel : BaseViewModel
    {

        public MainViewModel()
        {
            //LoggedUser = JsonConvert.DeserializeObject<MyUserASP>( Settings.UserASP );
        }

        
    }
}