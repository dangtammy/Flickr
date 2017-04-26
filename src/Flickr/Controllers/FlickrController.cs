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

namespace Flickr.Controllers
{
    [Authorize]
    public class FlickrController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;

        public FlickrController(UserManager<ApplicationUser> userManager, ApplicationDbContext db)
        {
            _userManager = userManager;
            _db = db;
        }

        public async Task<IActionResult> UserPage()
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var currentUser = await _userManager.FindByIdAsync(userId);
            return View(_db.Photos.Where(x => x.User.Id == currentUser.Id));
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Photo photo)
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var currentUser = await _userManager.FindByIdAsync(userId);
            photo.User = currentUser;
            _db.Photos.Add(photo);
            _db.SaveChanges();
            return RedirectToAction("UserPage", "Flickr");
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
