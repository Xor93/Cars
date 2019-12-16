using Cars.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Cars.EntityFramework
{
    public class MyStrategy 
        : DropCreateDatabaseIfModelChanges<CarsContext>
    {
        protected override void Seed(CarsContext context)
        {
            List<User> users = context.Users.AddRange(new List<User>() {
                new User() { Coins = 500, Login = "USER1", Password = "12345678" },
                new User() { Coins = 500, Login = "USER2", Password = "12345678" },
            }).ToList();

            List<Car> cars = context.Cars.AddRange(new List<Car>() {
                new Car() { Title = "Red Car", Price = 0, MotorPrice = 200, ClutchPrice = 200, TankСapacityPrice = 200, TransmissionPrice = 200},
                new Car() { Title = "Blue Car", Price = 6000, MotorPrice = 600, ClutchPrice = 600, TankСapacityPrice = 600, TransmissionPrice = 600},
                new Car() { Title = "Yellow Car", Price = 20000, MotorPrice = 1500, ClutchPrice = 1500, TankСapacityPrice = 1500, TransmissionPrice = 1500}
            }).ToList();

            List<OpenCar> openCars = context.OpenCars.AddRange(new List<OpenCar>()
            {
                new OpenCar() {CarId = cars[0], UserId = users[0], ClutchLvl = 1, MotorLvl = 1, TankСapacityLvl = 1, TransmissionLvl = 1},
                new OpenCar() {CarId = cars[0], UserId = users[1], ClutchLvl = 1, MotorLvl = 1, TankСapacityLvl = 1, TransmissionLvl = 1}
            }).ToList();

            List<Track> tracks = context.Tracks.AddRange(new List<Track>() {
               new Track(){Title = "Track_1", Price = 0, Length = 5000},
               new Track(){Title = "Track_2", Price = 5000, Length = 6000},
               new Track(){Title = "Track_3", Price = 15000, Length = 7000},
               new Track(){Title = "Track_4", Price = 35000, Length = 8000},
               new Track(){Title = "Track_5", Price = 500000, Length = 9000},
               new Track(){Title = "Track_6", Price = 90000, Length = 10000}
            }).ToList();


            List<OpenTrack> openTracks = context.OpenTracks.AddRange(new List<OpenTrack>()
            {
                new OpenTrack(){UserId = users[0], TrackId = tracks[0], MostTraveled = 0},
                new OpenTrack(){UserId = users[1], TrackId = tracks[0], MostTraveled = 0}
            }).ToList();

            context.SaveChanges();
          
            base.Seed(context);
        }
    }
}
