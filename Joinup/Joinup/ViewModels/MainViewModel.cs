using System;
using System.Collections.Generic;
using System.Text;

namespace Joinup.ViewModels
{
    public class MainViewModel
    {
        public CommentsViewModel Comments { get; set; }

        public LoginViewModel Login { get; set; }

        public MainViewModel()
        {
            Comments = new CommentsViewModel();
            Login = new LoginViewModel();
        }
    }
}
