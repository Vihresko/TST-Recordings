using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using TST.Core.Interfaces;
using TST.Database.ViewModels.Track;

namespace TST.Web.Controllers
{
    public class TrackController : Controller
    {
        private readonly ITrackService trackService;
        private readonly IMemoryCache memoryCash;
        public TrackController(ITrackService _trackService, IMemoryCache _memoryCash)
        {
            trackService = _trackService;
            memoryCash = _memoryCash;
        }
        public async Task<IActionResult> Tracks()
        {
            var tracks = await trackService.GetTracks();
            return View(tracks);
        }

        public async Task<IActionResult> Play(int trackId)
        {
            TrackModel track = new TrackModel();
            if (!memoryCash.TryGetValue($"trackId{trackId}", out track))
            {
                track = await trackService.GetTrackById(trackId);
                memoryCash.Set($"trackId{trackId}", track, TimeSpan.FromSeconds(300));
            }
            var tracks = await trackService.GetTracks();
            ViewBag.Data = "data:audio/wav;base64," + Convert.ToBase64String(track.TrackData);
            return View("~/Views/Track/Tracks.cshtml", tracks);
        }

        public IActionResult UploadTrack()
        {
            return View(new TrackModel());
        }
        [HttpPost]
        public async Task<IActionResult> UploadTrack(TrackModel model, IFormFile file)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            if (file != null && file.Length > 0)
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
            return File(track.TrackData, "audio/mpeg", $"{track.Name}.mp3");
        }
    }
}

