using Cars.EntityFramework.Interfaces;
using Cars.Models;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;

namespace Cars.EntityFramework
{
    class TrackService : ITrackService
    {
        private CarsContext context;

        public TrackService(CarsContext context)
        {
            this.context = context;
        }

        public ObservableCollection<OpenTrack> GetOpenTracks(int user_id)
        {
            var tracks = context.OpenTracks.Where<OpenTrack>(ot => context.Tracks.Any<Track>(t => user_id == ot.UserId.Id && ot.TrackId.Id == t.Id)).OrderBy(mc => mc.TrackId.Price).ToList();
            return new ObservableCollection<OpenTrack>(tracks);
        }

        public ObservableCollection<Track> GetShopTracks(int user_id)
        {
            SqlParameter param = new SqlParameter("@Id", user_id);
            var tracks = context.Database.SqlQuery<Track>("SELECT * FROM Tracks WHERE Tracks.Id != ALL(SELECT OpenTracks.TrackId_Id FROM OpenTracks WHERE OpenTracks.UserId_Id = @Id) ORDER BY Price", param);
            return new ObservableCollection<Track>(tracks);
        }

        public void ByTrack(Track track, User user)
        {
            Track newtrack = context.Tracks.Find(track.Id);
            OpenTrack openTrack = new OpenTrack() { TrackId = newtrack, MostTraveled = 0, UserId = user };
            user.OpenTracks.Add(openTrack);
            user.Coins -= track.Price;
            context.SaveChanges();
        }

        public void SetNewRecord(OpenTrack track, User user, int leght)
        {
            user.Coins += leght / 10;
            if (leght > track.MostTraveled)
            {
                track.MostTraveled = leght;
                context.SaveChanges();
            }

        }
    }
}
