using Cars.EntityFramework.Interfaces;
using Cars.Models;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System.Windows.Input;

namespace Cars.ViewModel
{
    public class UpgradeMenuVM : ViewModelBase
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

        private OpenCar selected_OpenCar;

        public OpenCar Selected_OpenCar
        {
            get { return selected_OpenCar; }
            set
            {
                if (value != null)
                {
                    selected_OpenCar = value;
                    UpdateValidPrice();
                    RaisePropertyChanged();
                }
            }
        }

        public UpgradeMenuVM(ICarService carService)
        {
            this.carService = carService;

            Up_MotorLvl = new RelayCommand(() => up_MotorLvl());
            Up_ClutchLvl = new RelayCommand(() => up_ClutchLvl());
            Up_TankLvl = new RelayCommand(() => up_TankLvl());
            Up_TransmissionLvl = new RelayCommand(() => up_TransmissionLvl());
            messangerRegister();
        }

        private void messangerRegister()
        {
            Messenger.Default.Register<User>(this, "Take user", (obj) => Cur_user = obj);
            Messenger.Default.Register<OpenCar>(this, "SelectedCar_Updated", (obj) => Selected_OpenCar = obj);
            Messenger.Default.Register<string>(this, "CoinsUpdated", (obj) => UpdateValidPrice());
            Messenger.Default.Send<string>("", "I'm ready");
        }

        public ICommand Up_MotorLvl { get; set; }
        public ICommand Up_TransmissionLvl { get; set; }
        public ICommand Up_ClutchLvl { get; set; }
        public ICommand Up_TankLvl { get; set; }


        private bool motorPriceValid;
        public bool MotorPriceValid
        {
            get { return motorPriceValid; }
            set
            {
                if (Selected_OpenCar.MotorLvl == 20)
                {
                    motorPriceValid = false;
                }
                else
                {
                    if (Cur_user.Coins >= Selected_OpenCar.CarId.MotorPrice)
                    {
                        motorPriceValid = true;
                    }
                    else motorPriceValid = false;
                }
                RaisePropertyChanged();

            }
        }

        private bool transmissionPriceValid;
        public bool TransmissionPriceValid
        {
            get { return transmissionPriceValid; }
            set
            {
                if (Selected_OpenCar.TransmissionLvl == 20)
                {
                    transmissionPriceValid = false;
                }
                else
                {

                    if (Cur_user.Coins >= Selected_OpenCar.CarId.TransmissionPrice)
                    {
                        transmissionPriceValid = true;
                    }
                    else transmissionPriceValid = false;
                }
                RaisePropertyChanged();
            }
        }

        private bool clutchPriceValid;
        public bool ClutchPriceValid
        {
            get { return clutchPriceValid; }
            set
            {
                if (Selected_OpenCar.ClutchLvl == 20)
                {
                    clutchPriceValid = false;
                }
                else
                {
                    if (Cur_user.Coins >= Selected_OpenCar.CarId.ClutchPrice)
                    {
                        clutchPriceValid = true;
                    }
                    else clutchPriceValid = false;
                }
                RaisePropertyChanged();
            }
        }

        private bool tankPriceValid;
        public bool TankPriceValid
        {
            get { return tankPriceValid; }
            set
            {
                if (Selected_OpenCar.TankСapacityLvl == 20)
                {
                    tankPriceValid = false;
                }
                else
                {
                    if (Cur_user.Coins >= Selected_OpenCar.CarId.TankСapacityPrice)
                    {
                        tankPriceValid = true;
                    }
                    else tankPriceValid = false;
                }
                RaisePropertyChanged();
            }
        }


        private void up_MotorLvl()
        {
            Selected_OpenCar = carService.UpgradeMotor(Selected_OpenCar, Cur_user);
            sendUpdgradeCoin();
            UpdateValidPrice();
        }

        private void up_TransmissionLvl()
        {
            Selected_OpenCar = carService.UpgradeTransmission(Selected_OpenCar, Cur_user);
            sendUpdgradeCoin();
            UpdateValidPrice();
        }

        private void up_ClutchLvl()
        {
            Selected_OpenCar = carService.UpgradeClutch(Selected_OpenCar, Cur_user);
            sendUpdgradeCoin();
            UpdateValidPrice();
        }

        private void up_TankLvl()
        {
            Selected_OpenCar = carService.UpgradeTank(Selected_OpenCar, Cur_user);
            sendUpdgradeCoin();
            UpdateValidPrice();
        }



        private void sendUpdgradeCoin()
        {
            Messenger.Default.Send<string>("", "UpgradeCoinsUpgrade");
        }
        private void UpdateValidPrice()
        {
            MotorPriceValid = MotorPriceValid;
            TransmissionPriceValid = TransmissionPriceValid;
            ClutchPriceValid = ClutchPriceValid;
            TankPriceValid = TankPriceValid;
        }
    }
}
