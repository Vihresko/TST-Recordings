using Microsoft.EntityFrameworkCore;
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

        public async Task<TrackModel> GetTrackById(int trackId)
        {
            var track = await database.Tracks.FirstAsync(t => t.Id == trackId);
            var trackModel = new TrackModel()
            {
                Id = track.Id,
                Description = track.Description,
                Name = track.Name,
                TrackData = track.TrackData
            };
            return trackModel;
        }

        public async Task<ICollection<TrackModel>> GetTracks()
        {
            var tracks = await database.Tracks.ToListAsync();
            var model = new List<TrackModel>();
            foreach (var track in tracks)
            {
                model.Add(new TrackModel()
                {
                    Id = track.Id,
                    Name = track.Name,
                    Description = track.Description,
                    TrackData = track.TrackData
                });
            }
            return model;
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
            await database.SaveChangesAsync();
            if(result != null)
            {
                return true;
            }
            return false;
        }

    }
}
