using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using MvcMovie.Models;

namespace MvcMovie.Helpers
{
    public static class HtmlContentExtentions
    {
        public static IHtmlContent MyDisplay<TModel, TResult>(this IHtmlHelper<TModel> helper, 
            Expression<Func<TModel, TResult>> expression)
        {
            TResult model;
            model = expression.Compile()(helper.ViewData.Model);
            var content = new HtmlContentBuilder();
                if (!(model is IEnumerable))
                {

                    content.AppendHtml("<div>");
                    content.AppendHtml(GenerateHtmlString(model));
                    content.AppendHtml("</div>");
                    return content;
                }

                foreach (var i in (IEnumerable) model)
                {

                    content.AppendHtml("<div>");
                    content.AppendHtml(GenerateHtmlString(i));
                    content.AppendHtml("</div>");
                    VisitedClasses.Clear();
                }
                
                return content;
        }

        private static HashSet<Type> VisitedClasses = new HashSet<Type>();
        private static string GenerateHtmlString(object model)
        {
            if (model != null)
            {
                var type = model.GetType();
                var content = new StringBuilder();
                if (type == typeof(int) || type == typeof(string)
                                        || type == typeof(long) || type == typeof(bool) || type.IsEnum)
                {
                    content.Append($"<div class \"display-field\">{model}</div>");
                    return content.ToString();
                }

                if (VisitedClasses.Contains(type))
                {
                    throw new Exception("LoopDetected");
                }

                VisitedClasses.Add(type);

                foreach (var property in type.GetProperties())
                {
                    content.Append($"<div class=\"display-label\">{property.Name}</div>");
                    content.Append(GenerateHtmlString(property.GetValue(model)));
                }

                return content.ToString();
            }

            return "<div class=\"display-label\">None</div>";
        }
        
    }
}