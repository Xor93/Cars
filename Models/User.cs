using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.ObjectModel;

namespace Cars.Models
{
    public class User
    {
        [Index(IsUnique = true)]
        public int Id { get; set; }

        [Required]
        public string Login { get; set; }

        [Required]
        [MinLength(8)]
        [MaxLength(16)]
        public string Password { get; set; }

        public int Coins { get; set; }
        public ObservableCollection<OpenCar> OpenCars { get; set; }
        public ObservableCollection<OpenTrack> OpenTracks { get; set; }
    }
}
