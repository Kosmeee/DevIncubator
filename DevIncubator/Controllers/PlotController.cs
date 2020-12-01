using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DevIncubator.Models;
using DevIncubator.PresentationServices;

namespace DevIncubator.Controllers
{
    public class PlotController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View("View", new UserData ());
        }
        [HttpPost]
        public ActionResult Index(UserData Plot)
        {
            if (ModelState.IsValid)
            {
                var plotPresentation = new PlotPresentationService();
                Plot.Points = new List<Point>();
                plotPresentation.AddData(Plot);
                return View("View", Plot);
            }
            else
                return View("View", new UserData());
        }
    }
}