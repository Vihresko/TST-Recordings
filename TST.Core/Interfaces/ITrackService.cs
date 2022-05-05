using TST.Database.ViewModels.Track;

namespace TST.Core.Interfaces
{
    public interface ITrackService
    {
        public Task<bool> UploadTrack(TrackModel trackModel);
    }
}
