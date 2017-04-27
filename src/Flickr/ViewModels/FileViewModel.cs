using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Flickr.Models;
using Microsoft.AspNetCore.Http;

namespace Flickr.ViewModels
{
    public class FileViewModel
    {
        public string Title { get; set; }
        //public string Image { get; set; }
        //public string Description { get; set; }

        public IFormFile File { get; set; }
    }
}
