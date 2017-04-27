using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Flickr.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;
using System.IO;
using Flickr.ViewModels; 

namespace Flickr.Controllers
{
    [Authorize]
    public class FlickrController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHostingEnvironment _environment;

        public FlickrController(UserManager<ApplicationUser> userManager, ApplicationDbContext db, IHostingEnvironment environment)
        {
            _userManager = userManager;
            _db = db;
            _environment = environment;
        }

    public async Task<IActionResult> UserPage()
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var currentUser = await _userManager.FindByIdAsync(userId);
            return View(_db.Photos.Where(x => x.User.Id == currentUser.Id));
        }

        public IActionResult Create()
        {
            Photo photo = new Photo(); 
            return View(photo);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Photo photo, IFormFile files)
        {
            Byte[] m_bytes = ConvertToBytes(files);
            photo.File = m_bytes; 
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var currentUser = await _userManager.FindByIdAsync(userId);
            //var uploads = Path.Combine(_environment.WebRootPath, "wwwroot\\images");
            //IFormFile uploadedImage = files.FirstOrDefault();

            //if (uploadedImage == null || uploadedImage.ContentType.ToLower().StartsWith('image/'))
            //{
            //    MemoryStream ms = new MemoryStream();
            //    uploadedImage.OpenReadStream().CopyTo(ms);

            //    System.Drawing.Image image = System.Drawing.Image.FromStream(ms);
            //    Models.Image imageEntity = new Models.Image()
            //}
         

                photo.User = currentUser;
                _db.Photos.Add(photo);
                _db.SaveChanges();
                return RedirectToAction("UserPage", "Flickr");
        }

        private byte[] ConvertToBytes(IFormFile image)
        {
            byte[] CoverImageBytes = null;

            BinaryReader reader = new BinaryReader(image.OpenReadStream());
            CoverImageBytes = reader.ReadBytes((int)image.Length);
            return CoverImageBytes;
        }


        public IActionResult Edit(int id)
        {
            var thisPhoto = _db.Photos.FirstOrDefault(photos => photos.Id == id);
            return View(thisPhoto);
        }

        [HttpPost]
        public IActionResult Edit(Photo photo)
        {
            _db.Entry(photo).State = EntityState.Modified;
            _db.SaveChanges();
            return RedirectToAction("UserPage", "Flickr");
        }

        public IActionResult Details(int id)
        {
            var thisPhoto = _db.Photos.FirstOrDefault(photos => photos.Id == id);
            return View(thisPhoto);
        }

        //Get Delete
        public IActionResult Delete(int id)
        {
            var thisPhoto = _db.Photos.FirstOrDefault(photos => photos.Id == id);
            return View(thisPhoto);
        }

        //Post - Delete
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var thisPhoto = _db.Photos.FirstOrDefault(photos => photos.Id == id);
            _db.Remove(thisPhoto);
            _db.SaveChanges();
            return RedirectToAction("UserPage" ,"Flickr");
        }


    }
}
