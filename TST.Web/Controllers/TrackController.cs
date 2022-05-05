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
        public IActionResult Tracks()
        {
            return View(new TrackModel());
        }

        [HttpPost]
        public async Task<IActionResult> UploadTrack(TrackModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await trackService.UploadTrack(model);
            return RedirectToAction("Tracks");
        } 
    }
}
