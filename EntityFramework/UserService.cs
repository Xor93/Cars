using Cars.EntityFramework.Interfaces;
using Cars.Models;
using System.Linq;

namespace Cars.EntityFramework
{
    public class UserService : IUserService
    {
        private CarsContext context;

        public UserService(CarsContext context)
        {
            this.context = context;
        }

        public User Authentication(string login, string password)
        {
            User user = context.Users.Where(u => login == u.Login && password == u.Password).FirstOrDefault();
            if (user != null)
            {
                return user;
            }
            else return null;
        }

        public User Registration(string login, string password)
        {
            User user = context.Users.Add(new User() { Coins = 100, Login = login, Password = password });
            context.OpenCars.Add(new OpenCar { CarId = context.Cars.First(), UserId = user, ClutchLvl = 1, MotorLvl = 1, TankСapacityLvl = 1, TransmissionLvl = 1 });
            context.OpenTracks.Add(new OpenTrack { UserId = user, TrackId = context.Tracks.First() });
            context.SaveChanges();
            return user;
        }

        public bool LoginVerification(string login)
        {
            User user;
            using (CarsContext car = new CarsContext())
            {
                user = context.Users.Where(u => login == u.Login).FirstOrDefault();
            }
            return user != null;
        }

        public User GetUser(int id)
        {
            return context.Users.Where(u => u.Id == id).FirstOrDefault();
        }
    }
}
