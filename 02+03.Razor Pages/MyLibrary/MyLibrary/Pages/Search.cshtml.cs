using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyLibrary.Data;
using MyLibrary.Models;
using MyLibrary.Models.ViewModels;

namespace MyLibrary.Pages
{
    public class SearchModel : BaseModelController
    {      
        public SearchModel(LibraryDbContext context)
            :base(context)
        {
            this.Authors = new List<SearchAuthorViewModel>();
            this.Books = new List<SearchBookViewModel>();
        }

        public List<SearchBookViewModel> Books { get; set; }

        public List<SearchAuthorViewModel> Authors { get; set; }

        [BindProperty(SupportsGet = true)] // var.1! or var 2.
        public string SearchTerm { get; set; }


        public void OnGet(string name)
        {
            ////var.2:
            //this.SearchTerm = this.Request.HttpContext.Request
            //    .QueryString.ToString().Split('=').Last();

            this.Authors = this.Context.Authors
                .Where(a => a.Name.Contains(this.SearchTerm))
                .Select(a => new SearchAuthorViewModel
                {
                    Id = a.Id,
                    Name = a.Name
                })
                .ToList();

            this.Books = this.Context.Books
                             .Where(a => a.Title.Contains(this.SearchTerm))
                            .Select(a => new SearchBookViewModel
                            {
                                Id = a.Id,
                                Title = a.Title
                            })
                            .ToList();

            foreach (var author in this.Authors)
            {
                string markedResult = Regex.Replace(
                    author.Name,
                    $"({Regex.Escape(this.SearchTerm)})",
                    match => $"<strong class=\"text-danger\">{match.Groups[0].Value}</strong>",
                    RegexOptions.IgnoreCase | RegexOptions.Compiled);

                author.Name = markedResult;
            }

            foreach (var book in this.Books)
            {
                string markedResult = Regex.Replace(
                    book.Title,
                    $"({Regex.Escape(this.SearchTerm)})",
                    match => $"<strong class=\"text-danger\">{match.Groups[0].Value}</strong>",
                    RegexOptions.IgnoreCase | RegexOptions.Compiled);

                book.Title = markedResult;
            }
        }
    }
}