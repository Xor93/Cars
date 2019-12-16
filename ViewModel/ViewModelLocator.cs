using GalaSoft.MvvmLight.Ioc;
using CommonServiceLocator;
using Cars.EntityFramework.Interfaces;
using Cars.EntityFramework;

namespace Cars.ViewModel
{

    public class ViewModelLocator
    {
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            SimpleIoc.Default.Register<CarsContext>();
            SimpleIoc.Default.Register<IUserService, UserService>();
            SimpleIoc.Default.Register<ICarService, CarService>();
            SimpleIoc.Default.Register<ITrackService, TrackService>();
            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<AuthenticationVM>();
        }

        public MainViewModel Main
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainViewModel>();
            }
        }

        public MenuVM Menu
        {
            get
            {
                SimpleIoc.Default.Register<MenuVM>();
                return ServiceLocator.Current.GetInstance<MenuVM>();
            }
        }

        public UpgradeMenuVM UpgradeMenu
        {
            get
            {
                SimpleIoc.Default.Register<UpgradeMenuVM>();
                return ServiceLocator.Current.GetInstance<UpgradeMenuVM>();
            }
        }

        public CarShopVM CarShop
        {
            get
            {
                SimpleIoc.Default.Register<CarShopVM>();
                return ServiceLocator.Current.GetInstance<CarShopVM>();
            }
        }

        public TrackShopVM TrackShop
        {
            get
            {
                SimpleIoc.Default.Register<TrackShopVM>();
                return ServiceLocator.Current.GetInstance<TrackShopVM>();
            }
        }

        public GameVM GameMenu
        {
            get
            {
                SimpleIoc.Default.Register<GameVM>();
                return ServiceLocator.Current.GetInstance<GameVM>();
            }
        }

        public AuthenticationVM Authentication
        {
            get
            {
                return ServiceLocator.Current.GetInstance<AuthenticationVM>();
            }
        }


        public static void CleanupGameVM()
        {
            SimpleIoc.Default.Unregister<GameVM>();
            SimpleIoc.Default.Unregister<MenuVM>();
            SimpleIoc.Default.Unregister<UpgradeMenuVM>();
            SimpleIoc.Default.Unregister<CarShopVM>();
            SimpleIoc.Default.Unregister<TrackShopVM>();
        }
        public static void Cleanup()
        {
            SimpleIoc.Default.Unregister<GameVM>();
            SimpleIoc.Default.Unregister<MenuVM>();
            SimpleIoc.Default.Unregister<UpgradeMenuVM>();
            SimpleIoc.Default.Unregister<CarShopVM>();
            SimpleIoc.Default.Unregister<TrackShopVM>();
            SimpleIoc.Default.Unregister<AuthenticationVM>();
            SimpleIoc.Default.Unregister<MainViewModel>();
            SimpleIoc.Default.Unregister<CarsContext>();
            SimpleIoc.Default.Unregister<UserService>();
            SimpleIoc.Default.Unregister<CarService>();
            SimpleIoc.Default.Unregister<TrackService>();
        }
    }
}