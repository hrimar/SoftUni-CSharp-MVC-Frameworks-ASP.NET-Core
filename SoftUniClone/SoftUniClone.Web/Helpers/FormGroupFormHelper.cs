using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace SoftUniClone.Web.Helpers
{
    public static class FormGroupFormHelper
    {
        public static IHtmlContent FormGroupFor<TModel, TResult>(
            this IHtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TResult>> expression)
        {
            using (var writer = new StringWriter())
            {
                var label = htmlHelper.LabelFor(expression, new { @class = "control-label col-sm-2" });
                var editor = htmlHelper.EditorFor(expression, new { htmlAttributes = new { @class = "form-control" } });
                var validationMessage = htmlHelper.ValidationMessageFor(expression, null, new { @class = "text-danger" });

                writer.Write("<div class=\"form-group\">");
                label.WriteTo(writer, HtmlEncoder.Default);
                writer.Write("<div class=\"col-sm-10\">");
                editor.WriteTo(writer, HtmlEncoder.Default);
                validationMessage.WriteTo(writer, HtmlEncoder.Default);
                writer.Write("</div></div>");

                return new HtmlString(writer.ToString());
            }
        }

        //public static IHtmlContent FormGroupFor<Tmodel, TResult>(
        //    this IHtmlHelper<Tmodel> htmlHelper,
        //    Expression<Func<Tmodel, TResult>> expression)
        //{
        //    /* @*<div class="form-group row">
        //        <label asp-for="Name" class="col-sm-2 col-form-label col-sm-push-1"></label>
        //        <div class="col-sm-10">
        //            <input asp-for="Name" class="form-control">
        //            <span asp-validation-for="Name" class="text-danger"></span>
        //        </div>
        //    </div>    */


        //    using (var writer = new StringWriter())
        //    {                
        //        var label = htmlHelper.LabelFor(expression, new { @class = "control-label col-sm-2" });
        //        var editor = htmlHelper.EditorFor(expression, new { htmlAttributes = new { @class = "form-control" } });
        //        var validationMessage = htmlHelper.ValidationMessageFor(expression, null, new { @class = "text-danger" });

        //        writer.Write("<div class=\"form-group\">");
        //        label.WriteTo(writer, HtmlEncoder.Default);
        //        writer.Write("<div class=\"col-sm-10\">");
        //        editor.WriteTo(writer, HtmlEncoder.Default);
        //        validationMessage.WriteTo(writer, HtmlEncoder.Default);
        //        writer.Write("</div></div>");

        //        return new HtmlString(writer.ToString());
        //    }
        //}
    }
}
