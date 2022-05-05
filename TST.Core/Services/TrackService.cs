using TST.Core.Interfaces;
using TST.Database;
using TST.Database.Models;
using TST.Database.ViewModels.Track;

namespace TST.Core.Services
{
    public class TrackService : ITrackService
    {
        private readonly TstDbContext database;
        public TrackService(TstDbContext _database)
        {
            database = _database;
        }
        public async Task<bool> UploadTrack(TrackModel trackModel)
        {
            var newTrack = new Track()
            {
                Name = trackModel.Name,
                Description = trackModel.Description,
                TrackData = trackModel.TrackData
            };
            var result = await database.Tracks.AddAsync(newTrack);
            if(result != null)
            {
                return true;
            }
            return false;
        }
    }
}
