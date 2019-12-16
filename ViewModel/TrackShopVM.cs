using Cars.EntityFramework.Interfaces;
using Cars.Models;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Cars.ViewModel
{
    public class TrackShopVM : ViewModelBase
    {
        private ITrackService trackService;
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


        private ObservableCollection<Track> tracks;
        public ObservableCollection<Track> Tracks
        {
            get { return tracks; }
            set
            {
                if (value != null)
                {
                    tracks = value;
                    RaisePropertyChanged();
                }
            }
        }

        private Track selectedTrack;
        public Track SelectedTrack
        {
            get { return selectedTrack; }
            set
            {
                selectedTrack = value;
                UpdateValidPrice();
                RaisePropertyChanged();
            }
        }


        private bool priceValid;
        public bool PriceValid
        {
            get { return priceValid; }
            set
            {
                priceValid = value;
                RaisePropertyChanged();
            }
        }


        public TrackShopVM(ITrackService trackService)
        {
            this.trackService = trackService;
            messangerRegister();
            By_Track = new RelayCommand(() => { by_Track(); });
        }

        public ICommand By_Track { get; set; }

        private void by_Track()
        {
            if (SelectedTrack != null)
            {
                trackService.ByTrack(SelectedTrack, Cur_user);
                Tracks.Remove(SelectedTrack);
                SelectedTrack = null;
                sendUpdgradeCoin();
            }
        }

        private void messangerRegister()
        {
            Messenger.Default.Register<string>(this, "CoinsUpdated", (obj) => UpdateValidPrice());
            Messenger.Default.Register<User>(this, "Take user TrackShop", (obj) =>
            {
                Cur_user = obj;
                Tracks = this.trackService.GetShopTracks(Cur_user.Id);
                if (Tracks.Count > 0) SelectedTrack = Tracks[0];

            });
            Messenger.Default.Send<string>("", "I'm readyTrackShop");
        }


        private void sendUpdgradeCoin()
        {
            Messenger.Default.Send<string>("", "UpgradeCoinsTrackShop");
        }
        private void UpdateValidPrice()
        {
            if (SelectedTrack == null)
            {
                PriceValid = false;
            }
            else
            {
                if (Cur_user.Coins >= SelectedTrack.Price)
                {
                    PriceValid = true;
                }
                else PriceValid = false;
            }
        }
    }
}
