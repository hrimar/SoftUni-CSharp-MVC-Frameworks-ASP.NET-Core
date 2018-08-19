using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SoftUniClone.ServiceModels.Student.BindingModels;

namespace SoftUniClone.Web.Controllers
{
    public class LecturesController : Controller
    {
        public LecturesController()
        {
            //add services!
        }

        [HttpGet]
        public IActionResult UploadHomework()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UploadHomework(HomeworkUploadingBindingModel model)
        {
            // TODO: get username after authorization and add lecture and user
            string fullFilePath = Path.Combine(Directory.GetCurrentDirectory(), "Files", $"{"username"}-{"lektureName"}-[{model.HomeworkFile.Name}]");
            var fileStream = new FileStream(fullFilePath, FileMode.Create);
            await model.HomeworkFile.CopyToAsync(fileStream);
            
            // redurect ti courseInstance or return partialView (homework upload succes)
            return View();
        }
    }
}