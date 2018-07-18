using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyLibrary.Data;
using MyLibrary.Models;

namespace MyLibrary.Pages.Borrowers
{
    public class AddModel : BaseModelController
    {
        public AddModel(LibraryDbContext context)
            : base(context)
        {
        }

        [Required]
        [BindProperty]
        public string Name { get; set; }

        [Required]
        [BindProperty]
        public string Address { get; set; }

        public void OnPost()
        {
            Thread.Sleep(1000);
        }
       
        public IActionResult OnPostAddBorrower()
        {
            var borrower = this.Context
                .Borrowers.FirstOrDefault(b => b.Name == this.Name);

            if (borrower == null)
            {
                borrower = new Borrower()
                {
                    Name = this.Name,
                    Address = this.Address
                };

                this.Context.Borrowers.Add(borrower);
                this.Context.SaveChanges(); 
            }

            return RedirectToPage("/Index");
        }
    }
}