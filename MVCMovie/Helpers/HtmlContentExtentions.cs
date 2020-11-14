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
        public static IHtmlContent MyEditor<TModel, TResult>(this IHtmlHelper<TModel> helper, 
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
                if (type == typeof(int) || type==typeof(long))
                {
                    
                    
                    content.Append($"<div class=\"editor-field\">" +
                                   $"<input class=\"text-box single-line\" " +
                                   $"name=\"{type.Name}\" type=\"number\" value=\"{model}\"> " +
                                   $"</div>");
                    return content.ToString();
                }
                if (type == typeof(string))
                {
                    content.Append($"<div class=\"editor-field\"><input class=\"text-box single-line\"" +
                                   $"name=\"[0].{type.Name}\" type=\"text\" value=\"{model}\">" +
                                   $"</div>");
                    return content.ToString();
                }

                if (type == typeof(bool))
                {
                    if ((bool) model)
                    {
                        content.Append($"<div class=\"editor-field\"><input checked=\"checked\" class=\"check-box\" " +
                                       $"name=\"{type.Name}\" " +
                                       $"type=\"checkbox\" value=\"{model}\">" +
                                       $"</div>");
                    }
                    else
                    {
                        content.Append($"<div class=\"editor-field\"><input class=\"check-box\" " +
                                       $"name=\"{type.Name}\" " +
                                       $"type=\"checkbox\" value=\"{model}\">" +
                                       $"</div>");
                    }

                    return content.ToString();
                }

                if (type.IsEnum)
                {
                    content.Append($"<div class=\"editor-field\">" +
                                   $"<select name=\"{type.Name}\">");
                    foreach (var i in type.GetEnumValues())
                    {
                        if (i.ToString() == model.ToString())
                        {
                            content.Append($"<option selected=\"selected\">{i}</option>");
                        }
                        else
                        {
                            content.Append($"<option>{i}</option>");
                        }
                    }

                    content.Append($"</select></div>");
                    return content.ToString();
                }

                if (!VisitedClasses.Contains(type))
                {
                    VisitedClasses.Add(type);
                    foreach (var property in type.GetProperties())
                    {
                        content.Append($"<div class=\"display-label\">{property.Name}</div>");
                        content.Append(GenerateHtmlString(property.GetValue(model)));

                    }
                }

                return content.ToString();
            }

            return "<div class=\"display-label\">None</div>";
        }
        
    }
}