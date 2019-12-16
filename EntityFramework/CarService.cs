using Cars.EntityFramework.Interfaces;
using Cars.Models;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;

namespace Cars.EntityFramework
{
    public class CarService : ICarService
    {
        private CarsContext context;

        public CarService(CarsContext context)
        {
            this.context = context;
        }

        public ObservableCollection<Car> GetShopCars(int user_id)
        {
            SqlParameter param = new SqlParameter("@Id", user_id);
            var cars = context.Database.SqlQuery<Car>("SELECT * FROM Cars WHERE Cars.Id != ALL(SELECT OpenCars.CarId_Id FROM OpenCars WHERE OpenCars.UserId_Id = @Id) ORDER BY Price", param);
            return new ObservableCollection<Car>(cars);
        }
        public ObservableCollection<OpenCar> GetOpenCars(int user_id)
        {
            var cars = context.OpenCars.Where(oc => context.Cars.Any<Car>(c => user_id == oc.UserId.Id && oc.CarId.Id == c.Id)).OrderBy(mc=> mc.CarId.Price).ToList();
            return new ObservableCollection<OpenCar>(cars);
        }

        public OpenCar UpgradeClutch(OpenCar mycar, User user)
        {
            mycar.ClutchLvl += 1;
            user.Coins -= mycar.CarId.ClutchPrice;
            context.SaveChanges();
            return mycar;
        }

        public OpenCar UpgradeMotor(OpenCar mycar, User user)
        {
            mycar.MotorLvl += 1;
            user.Coins -= mycar.CarId.MotorPrice;
            context.SaveChanges();
            return mycar;
        }

        public OpenCar UpgradeTank(OpenCar mycar, User user)
        {
            mycar.TankСapacityLvl += 1;
            user.Coins -= mycar.CarId.TankСapacityPrice;
            context.SaveChanges();
            return mycar;
        }

        public OpenCar UpgradeTransmission(OpenCar mycar, User user)
        {
            mycar.TransmissionLvl += 1;
            user.Coins -= mycar.CarId.TransmissionPrice;
            context.SaveChanges();
            return mycar;
        }

        public void ByCar(Car mycar, User user)
        {
            Car newcar = context.Cars.Find(mycar.Id);
            OpenCar car = new OpenCar() { ClutchLvl = 1, TankСapacityLvl = 1, TransmissionLvl = 1, MotorLvl = 1, CarId = newcar, UserId = user };
            user.OpenCars.Add(car);
            user.Coins -= mycar.Price;
            context.SaveChanges();
        }
    }
}
