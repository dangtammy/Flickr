using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Flickr.Models;
using Xunit;


namespace Flickr.Tests
{
    public class PhotoTest
    {
        [Fact]
        public void GetTitleTest()
        {
            //Arrange
            var photo = new Photo();
            photo.Title = "Picture of Dog"; 

            //Act
            var result = photo.Title;

            //Assert
            Assert.Equal("Picture of Dog", result);
        }
    }
}
