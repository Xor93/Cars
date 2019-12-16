namespace Cars.Models
{
    public class OpenTrack
    {
        public int Id { get; set; }
        public virtual User UserId { get; set; }
        public virtual Track TrackId { get; set; }
        public int MostTraveled { get; set; }
    }
}
