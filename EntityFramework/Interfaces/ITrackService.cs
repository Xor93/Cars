using Cars.Models;
using System.Collections.ObjectModel;

namespace Cars.EntityFramework.Interfaces
{
    public interface ITrackService
    {
        ObservableCollection<OpenTrack> GetOpenTracks(int user_id);
        ObservableCollection<Track> GetShopTracks(int user_id);
        void ByTrack(Track track, User user);
        void SetNewRecord(OpenTrack track, User user, int leght);
    }
}
