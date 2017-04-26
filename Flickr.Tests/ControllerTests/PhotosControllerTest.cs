using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Flickr.Models;
using Flickr.ViewModels;
using Xunit;
using Microsoft.AspNetCore.Identity;

namespace Flickr.Tests
{
    //public class PhotosControllerTest
    //{
    //    public ApplicationDbContext _db;
    //    public UserManager<ApplicationUser> _userManager;
    //    [Fact]
    //    public void Get_ViewResult_UserPage_Test()
    //    {
    //        //Arrange
    //        FlickrController controller = new FlickrController(_userManager, _db);

    //        //Act
    //        var result = controller.UserPage();

    //        //Assert
    //        Assert.IsType<ViewResult>(result);
    //    }

        //[Fact]
        //public void Get_ModelList_UserPage_Test()
        //{
        //    //Arrange
        //    FlickrController controller = new FlickrController(_userManager, _db);
        //    IActionResult actionResult = new FlickrController.UserPage();
        //    ViewResult indexView = new FlickrController.UserPage() as ViewResult;

        //    //Act
        //    var result = indexView.ViewData.Model;

        //    //Assert
        //    Assert.IsType<List<Photo>>(result);
        //}
    }
}