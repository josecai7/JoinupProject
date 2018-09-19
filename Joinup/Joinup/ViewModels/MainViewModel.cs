using System;
using System.Collections.Generic;
using System.Text;

namespace Joinup.ViewModels
{
    public class MainViewModel
    {
        public CommentsViewModel Comments { get; set; }

        public PlansViewModel Plans { get; set; }

        public LoginViewModel Login { get; set; }

        public MainViewModel()
        {
            Plans = new PlansViewModel();
            Comments = new CommentsViewModel();
            Login = new LoginViewModel();
        }
    }
}
