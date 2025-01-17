using FileDownloader.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace FileDownloader.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public IActionResult StartWpfApp()
        {
            try
            {
                // «м≥нити при потреб≥
                string wpfAppPath = @"E:\FileDownloader\FileDownloader.UI\bin\Debug\net8.0-windows\FileDownloader.UI.exe";

                Process.Start(new ProcessStartInfo
                {
                    FileName = wpfAppPath,
                    UseShellExecute = true
                });

                return Json(new { success = true, message = "WPF application started successfully!" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"Error: {ex.Message}" });
            }
        }
    }
}
