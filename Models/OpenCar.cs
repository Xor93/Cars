namespace Cars.Models
{
    public class OpenCar
    {
        public int Id { get; set; }
        public int MotorLvl { get; set; }
        public int ClutchLvl { get; set; }
        public int TransmissionLvl { get; set; }
        public int TankСapacityLvl { get; set; }
        public virtual User UserId { get; set; }
        public virtual Car CarId { get; set; }
    }
}
