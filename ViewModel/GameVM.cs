using Cars.EntityFramework.Interfaces;
using Cars.Models;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace Cars.ViewModel
{
    public class GameVM : ViewModelBase
    {
        private IUserService userservice;
        private ICarService carService;
        private ITrackService trackService;

        private ObservableCollection<OpenTrack> openTracks;
        public ObservableCollection<OpenTrack> OpenTracks
        {
            get { return openTracks; }
            set
            {
                openTracks = value;
                RaisePropertyChanged();
            }
        }

        private ObservableCollection<OpenCar> openCars;
        public ObservableCollection<OpenCar> OpenCars
        {
            get { return openCars; }
            set
            {
                openCars = value;
                RaisePropertyChanged();
            }
        }

        private User cur_user;
        public User Cur_user
        {
            get { return cur_user; }
            set
            {
                if (value != null)
                {
                    cur_user = value;
                    RaisePropertyChanged();
                }
            }
        }

        private OpenCar selected_OpenCar;
        public OpenCar Selected_OpenCar
        {
            get { return selected_OpenCar; }
            set
            {
                if (value != null)
                {
                    selected_OpenCar = value;
                    Messenger.Default.Send<OpenCar>(Selected_OpenCar, "SelectedCar_Updated");
                    RaisePropertyChanged();
                }
            }
        }

        private OpenTrack selected_OpenTrack;
        public OpenTrack Selected_OpenTrack
        {
            get { return selected_OpenTrack; }
            set
            {
                if (value != null)
                {
                    selected_OpenTrack = value;
                    GetRecord = GetRecord;
                    RaisePropertyChanged();
                }
            }
        }

        public int GetCoins
        {
            get { return Cur_user.Coins; }
            set
            {
                Cur_user.Coins = value;
                Messenger.Default.Send<string>("", "CoinsUpdated");
                RaisePropertyChanged();
            }
        }

        public int GetRecord
        {
            get { return Selected_OpenTrack.MostTraveled; }
            set
            {
                trackService.SetNewRecord(Selected_OpenTrack, Cur_user, value);
                GetCoins = GetCoins;
                RaisePropertyChanged();
            }
        }

        private int lastGame;

        public int LastGame
        {
            get { return lastGame; }
            set
            {
                lastGame = value;
                RaisePropertyChanged();
            }
        }


        public GameVM(IUserService userservice, ICarService carService, ITrackService trackService)
        {
            this.userservice = userservice;
            this.carService = carService;
            this.trackService = trackService;
            ChangeUser = new RelayCommand(() => changeUser());
            StartGame = new RelayCommand(() => startGame());


            Cur_user = this.userservice.GetUser(Properties.Settings.Default.UserId);
            Cur_user.OpenCars = this.carService.GetOpenCars(Cur_user.Id);
            Cur_user.OpenTracks = this.trackService.GetOpenTracks(Cur_user.Id);

            OpenCars = Cur_user.OpenCars;
            OpenTracks = Cur_user.OpenTracks;

            Selected_OpenCar = OpenCars[0];
            Selected_OpenTrack = OpenTracks[0];
            messangerRegister();
        }


        public ICommand ChangeUser { get; set; }
        public ICommand StartGame { get; set; }
        private void messangerRegister()
        {
            Messenger.Default.Register<string>(this, "I'm ready", (obj) =>
            {
                Messenger.Default.Send<User>(Cur_user, "Take user");
                Messenger.Default.Send<OpenCar>(Selected_OpenCar, "SelectedCar_Updated");
            });

            Messenger.Default.Register<string>(this, "I'm readyCarShop", (obj) =>
            {
                Messenger.Default.Send<User>(Cur_user, "Take user CarShop");
            });

            Messenger.Default.Register<string>(this, "I'm readyTrackShop", (obj) =>
            {
                Messenger.Default.Send<User>(Cur_user, "Take user TrackShop");
            });

            Messenger.Default.Register<string>(this, "UpgradeCoinsUpgrade", (obj) =>
            {
                GetCoins = GetCoins;
            });

            Messenger.Default.Register<string>(this, "UpgradeCoinsTrackShop", (obj) =>
           {
               GetCoins = GetCoins;
               OpenTracks = new ObservableCollection<OpenTrack>(Cur_user.OpenTracks.OrderBy(mt => mt.TrackId.Price));
           });

            Messenger.Default.Register<string>(this, "UpgradeCoinsCarsShop", (obj) =>
            {
                GetCoins = GetCoins;
                OpenCars = new ObservableCollection<OpenCar>(Cur_user.OpenCars.OrderBy(mt => mt.CarId.Price));
            });
        }

        private void startGame()
        {
            Random random = new Random();
            LastGame = random.Next(500, Selected_OpenTrack.TrackId.Length);
            GetRecord = LastGame;
        }
        private void changeUser()
        {
            Messenger.Default.Send<string>("", "ChangeUser");
            ViewModelLocator.CleanupGameVM();
        }

    }
}
