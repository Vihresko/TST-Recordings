using Microsoft.AspNetCore.Mvc;
using TST.Core.Interfaces;
using TST.Database.ViewModels.Track;

namespace TST.Web.Controllers
{
    public class TrackController : Controller
    {
        private readonly ITrackService trackService;
        public TrackController(ITrackService _trackService)
        {
            trackService = _trackService;
        }
        public async Task<IActionResult> Tracks()
        {
            var tracks = await trackService.GetTracks();
            
            return View(tracks);
        }

        [HttpPost]
        public async Task<IActionResult> UploadTrack(TrackModel model, IFormFile file)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            if (file.Length > 0)
            {
                using (var ms = new MemoryStream())
                {
                    file.CopyTo(ms);
                    var fileBytes = ms.ToArray();
                    string s = Convert.ToBase64String(fileBytes); //this is important. Check it!
                    model.TrackData = fileBytes;
                    // act on the Base64 data
                }
            }
                await trackService.UploadTrack(model);
            return RedirectToAction("Tracks");
        }

        [HttpGet]
        public async Task<IActionResult> DownloadTrack(int trackId)
        {
            var track = await trackService.GetTrackById(trackId);
            return File(track.TrackData, "audio/mpeg", "track.mp3");
        }
    }
}

