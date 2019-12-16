using Cars.Models;
using System.Collections.ObjectModel;

namespace Cars.EntityFramework.Interfaces
{
    public interface ICarService
    {
        ObservableCollection<Car> GetShopCars(int user_id);
        ObservableCollection<OpenCar> GetOpenCars(int user_id);
        OpenCar UpgradeTransmission(OpenCar mycar, User user);
        OpenCar UpgradeMotor(OpenCar mycar, User user);
        OpenCar UpgradeTank(OpenCar mycar, User user);
        OpenCar UpgradeClutch(OpenCar mycar, User user);
        void ByCar(Car mycar, User user);
    }
}
