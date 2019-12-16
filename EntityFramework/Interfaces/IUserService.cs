using Cars.Models;

namespace Cars.EntityFramework.Interfaces
{
    public interface IUserService
    {
        User GetUser(int id);
        User Authentication(string login, string password);
        User Registration(string login, string password);
        bool LoginVerification(string login);
        //User UpdateMotor(User user, int price);

    }
}
