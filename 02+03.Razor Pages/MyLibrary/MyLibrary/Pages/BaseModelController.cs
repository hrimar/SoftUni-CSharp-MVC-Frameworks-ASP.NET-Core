using Microsoft.AspNetCore.Mvc.RazorPages;
using MyLibrary.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyLibrary.Pages
{
    public class BaseModelController : PageModel
    {
        public BaseModelController(LibraryDbContext context)
        {
            this.Context = context;
        }

        public LibraryDbContext Context { get; private set; }
    }
}
