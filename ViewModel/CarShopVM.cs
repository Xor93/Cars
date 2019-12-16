using Cars.EntityFramework.Interfaces;
using Cars.Models;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Cars.ViewModel
{
    public class CarShopVM : ViewModelBase
    {
        private ICarService carService;
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


        private ObservableCollection<Car> cars;
        public ObservableCollection<Car> Cars
        {
            get { return cars; }
            set
            {
                if (value != null)
                {
                    cars = value;
                    RaisePropertyChanged();
                }
            }
        }

        private Car selectedCar;
        public Car SelectedCar
        {
            get { return selectedCar; }
            set
            {
                selectedCar = value;
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
                priceValid = true;
                RaisePropertyChanged();
            }
        }


        public CarShopVM(ICarService carService)
        {
            this.carService = carService;
            messangerRegister();
            By_Car = new RelayCommand(() => { by_Car(); });
        }

        public ICommand By_Car { get; set; }

        private void by_Car()
        {
            if (SelectedCar != null)
            {
                carService.ByCar(SelectedCar, Cur_user);
                Cars.Remove(SelectedCar);
                SelectedCar = null;
                sendUpdgradeCoin();
            }
        }

        private void messangerRegister()
        {
            Messenger.Default.Register<string>(this, "CoinsUpdated", (obj) => UpdateValidPrice());
            Messenger.Default.Register<User>(this, "Take user CarShop", (obj) =>
            {
                Cur_user = obj;
                Cars = this.carService.GetShopCars(Cur_user.Id);
                if (Cars.Count > 0) SelectedCar = Cars[0];

            });
            Messenger.Default.Send<string>("", "I'm readyCarShop");
        }


        private void sendUpdgradeCoin()
        {
            Messenger.Default.Send<string>("", "UpgradeCoinsCarsShop");
        }
        private void UpdateValidPrice()
        {
            if (SelectedCar == null)
            {
                PriceValid = false;
            }
            else
            {
                if (Cur_user.Coins >= SelectedCar.Price)
                {
                    PriceValid = true;
                }
                else PriceValid = false;
            }
        }
    }
}
