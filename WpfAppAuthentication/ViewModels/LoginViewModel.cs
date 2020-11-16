using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfAppAuthentication.APIServices;

namespace WpfAppAuthentication.ViewModels
{
    public class LoginViewModel : Screen
    {
        private string _userName;
        private string _password;

        private IAPIHelper _apiHelper;

        public LoginViewModel(IAPIHelper apiHelper)
        {
            _apiHelper = apiHelper;
        }

        public string UserName
        {
            get { return _userName; }
            set
            { 
                _userName = value;
                NotifyOfPropertyChange(() => UserName);
            }
        }

        public string Password
        {
            get { return _password; }
            set 
            { 
                _password = value;
                NotifyOfPropertyChange(() => Password);
                //PasswordBox funktioniert nicht Richtig mit Caliburn.Micro
                NotifyOfPropertyChange(() => CanLogIn);
            }
        }

        public bool CanLogIn
        {
            get
            {
                //return !string.IsNullOrEmpty(UserName) && !string.IsNullOrEmpty(Password) && UserName.Length > 0 && Password.Length > 0;
                return UserName?.Length > 0 && Password?.Length > 0;
            }
        }


        //Das lauft mit nit normale TextBox
        //public bool CanLogIn(string userName, string password)
        //{
        //    bool output = false;
        //    if (userName.Length > 0 && password.Length > 0)
        //    {
        //        output = true;
        //    }

        //    return output;
        //}

        public void LogIn()
        {
            //var apiHelper = new  APIServices.APIHelper();

            try
            {
                var token = _apiHelper.Authentication(UserName, Password);
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
        }
    }
}
