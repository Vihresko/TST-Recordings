using System.ComponentModel.DataAnnotations;

namespace TST.Database.ViewModels.Track
{
    public class TrackModel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(Constant.Database.TRACK_NAME_MAX_LENGTH)]
        public string Name { get; set; }

        [MaxLength(Constant.Database.TRACK_DESCRIPTION_MAX_LENGHT)]
        public string? Description { get; set; }

       // [DataType(DataType.Upload)]
       // [Required]
        [MaxLength(Constant.Database.TRACK_DATA_MAX_LENGHT)]
        public byte[]? TrackData { get; set; }

        public ICollection<TrackModel> AllTracks { get; set; } = new List<TrackModel>();
    }
}