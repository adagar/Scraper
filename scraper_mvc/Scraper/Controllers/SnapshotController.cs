using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Scraper.Services;
using Scraper.Models;

namespace Scraper.Controllers
{
    public class SnapshotController : Controller
    {
        private readonly ISnapshotService _snapshotService;

        public SnapshotController(ISnapshotService snapshotService)
        {
            _snapshotService = snapshotService;
        }

        public async Task<IActionResult> Index()
        {
            //Get snapshots from database
            var snapshots = await _snapshotService.GetSnapshotsAsync();

            //Put items into a model
            var model = new SnapshotViewModel()
            {
                Snapshots = snapshots
            };

            //Render view using the model

            return View(model);
        }
    }
}