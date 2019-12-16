using Cars.EntityFramework.Interfaces;
using Cars.Models;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Cars.ViewModel
{
    public class AuthenticationVM : ViewModelBase
    {
        private IUserService userservice;
        private Page cur_Page;
        public Page Cur_Page
        {
            get { return cur_Page; }
            set
            {
                cur_Page = value;
                RaisePropertyChanged();
            }
        }

        private bool pas_Requirement;

        public bool Pas_Requirement
        {
            get { return pas_Requirement; }
            set
            {
                pas_Requirement = value;
                RaisePropertyChanged();
            }
        }

        private bool login_Requirement;

        public bool Login_Requirement
        {
            get { return login_Requirement; }
            set
            {
                login_Requirement = value;
                RaisePropertyChanged();
            }
        }

        private string regLogin;
        public string RegLogin
        {
            get { return regLogin; }
            set
            {
                regLogin = value;
                new Thread(() =>
                {
                    Login_Requirement = (userservice.LoginVerification(RegLogin));
                }).Start();
                RaisePropertyChanged();
            }
        }

        private string regPassword;
        public string RegPassword
        {
            get { return regPassword; }
            set
            {
                regPassword = value;
                if (regPassword.Length >= 8) { Pas_Requirement = true; }
                else Pas_Requirement = false;
                RaisePropertyChanged();
            }
        }


        private string login;
        public string Login
        {
            get { return login; }
            set
            {
                login = value;
                RaisePropertyChanged();
            }
        }

        private string password;

        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                RaisePropertyChanged();
            }
        }



        public AuthenticationVM(IUserService userservice)
        {
            this.userservice = userservice;
            Cur_Page = Page.Login;
            Messenger.Default.Register<string>(this, "ChangeUser", (obj) => Cur_Page = Page.Login);
        }




        private ICommand log_in;
        public ICommand Log_in => log_in ?? (log_in = new RelayCommand<PasswordBox>((obj) =>
        {
            Password = obj.Password;
            User user = userservice.Authentication(Login, Password);
            if (user != null)
            {
                Properties.Settings.Default.UserId = user.Id;
                Messenger.Default.Send<string>("", "GO");
            }
            else
            {
                MessageBox.Show("Incorrect login or password");
                obj.Password = "";
            }
        }));



        private ICommand registration;
        public ICommand Registration => registration ?? (registration = new RelayCommand<PasswordBox>((obj) =>
        {
            RegPassword = obj.Password;
            User user = userservice.Registration(RegLogin, RegPassword);
            if (user != null)
            {
                Properties.Settings.Default.UserId = user.Id;
                Messenger.Default.Send<string>("", "GO");
            }
        }));


        private ICommand registrationPage;
        public ICommand RegistrationPage => registrationPage ?? (registrationPage = new RelayCommand(() =>
        {
            Cur_Page = Page.Registration;
        }));

        private ICommand loginPage;
        public ICommand LoginPage => loginPage ?? (loginPage = new RelayCommand(() =>
        {
            Cur_Page = Page.Login;
        }));
        public enum Page
        {
            Login,
            Registration
        }
    }

}


