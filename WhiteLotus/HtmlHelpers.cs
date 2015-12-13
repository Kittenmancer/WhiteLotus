using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using WhiteLotus.Models.Entities;
using MarkEmbling.Utils.Extensions;
using NHibernate;

namespace MapleGroup.Web
{
    public static class HtmlHelpers
    {
        public static MvcHtmlString NavLinkControllerOnly(this HtmlHelper helper, string linkText, string action, string controller)
        {
            var linkTag = new TagBuilder("a");
            var liTag = new TagBuilder("li");

            var urlHelper = new UrlHelper(helper.ViewContext.RequestContext);
            var url = urlHelper.Action(action, controller);
            var currentController = (string)helper.ViewContext.RouteData.Values["controller"];

            if (currentController.EqualsWithoutCase(controller))
            {

                liTag.MergeAttribute("class", "active");
                linkTag.Attributes.Add("href", url);
                linkTag.InnerHtml = linkText;
                liTag.InnerHtml = linkTag.ToString(TagRenderMode.Normal);
            }
            else
            {
                linkTag.Attributes.Add("href", url);
                linkTag.InnerHtml = linkText;
                liTag.InnerHtml = linkTag.ToString(TagRenderMode.Normal);
            }

            return MvcHtmlString.Create(liTag.ToString(TagRenderMode.Normal));
        }

        public static MvcHtmlString NavLinkControllerOnly(this HtmlHelper helper, string linkText, string action, string controller, object parameters, object htmlAttributes)
        {
            var linkTag = new TagBuilder("a");
            var liTag = new TagBuilder("li");

            var urlHelper = new UrlHelper(helper.ViewContext.RequestContext);
            var url = urlHelper.Action(action, controller, parameters);
            var attribute = new RouteValueDictionary();
            var currentController = (string)helper.ViewContext.RouteData.Values["controller"];

            if (htmlAttributes != null)
            {
                foreach (System.ComponentModel.PropertyDescriptor property in System.ComponentModel.TypeDescriptor.GetProperties(htmlAttributes))
                {
                    attribute.Add(property.Name.Replace('_', '-'), property.GetValue(htmlAttributes));
                }
            }


            if (currentController.EqualsWithoutCase(controller))
            {

                liTag.MergeAttribute("class", "active");
                linkTag.Attributes.Add("href", url);
                linkTag.MergeAttributes(attribute);
                linkTag.InnerHtml = linkText;
                liTag.InnerHtml = linkTag.ToString(TagRenderMode.Normal);
            }
            else
            {
                linkTag.Attributes.Add("href", url);
                linkTag.MergeAttributes(attribute);
                linkTag.InnerHtml = linkText;
                liTag.InnerHtml = linkTag.ToString(TagRenderMode.Normal);
            }

            return MvcHtmlString.Create(liTag.ToString(TagRenderMode.Normal));
        }


        public static MvcHtmlString NavLink(this HtmlHelper helper, string linkText, string action, string controller)
        {
            var linkTag = new TagBuilder("a");
            var liTag = new TagBuilder("li");

            var urlHelper = new UrlHelper(helper.ViewContext.RequestContext);
            var url = urlHelper.Action(action, controller);
            var currentController = (string)helper.ViewContext.RouteData.Values["controller"];
            var currentActionName = (string)helper.ViewContext.RouteData.Values["action"];

            if (currentController.EqualsWithoutCase(controller) && currentActionName.EqualsWithoutCase(action))
            {

                liTag.MergeAttribute("class", "active");
                linkTag.Attributes.Add("href", url);
                linkTag.InnerHtml = linkText;
                liTag.InnerHtml = linkTag.ToString(TagRenderMode.Normal);
            }
            else
            {
                linkTag.Attributes.Add("href", url);
                linkTag.InnerHtml = linkText;
                liTag.InnerHtml = linkTag.ToString(TagRenderMode.Normal);
            }

            return MvcHtmlString.Create(liTag.ToString(TagRenderMode.Normal));
        }

        public static MvcHtmlString NavLink(this HtmlHelper helper, string linkText, string action, string controller, object parameters, object htmlAttributes)
        {
            var linkTag = new TagBuilder("a");
            var liTag = new TagBuilder("li");

            var urlHelper = new UrlHelper(helper.ViewContext.RequestContext);
            var url = urlHelper.Action(action, controller, parameters);
            var attribute = new RouteValueDictionary();
            var currentController = (string)helper.ViewContext.RouteData.Values["controller"];
            var currentActionName = (string)helper.ViewContext.RouteData.Values["action"];

            var dic = new System.Web.Routing.RouteValueDictionary(parameters);
            string newSlug = dic["slug"] as string;

            string currentSlug = (string)helper.ViewContext.RouteData.Values["slug"] ?? helper.ViewContext.RequestContext.HttpContext.Request.QueryString["slug"];
            if (htmlAttributes != null)
            {
                foreach (System.ComponentModel.PropertyDescriptor property in System.ComponentModel.TypeDescriptor.GetProperties(htmlAttributes))
                {
                    attribute.Add(property.Name.Replace('_', '-'), property.GetValue(htmlAttributes));
                }
            }


            if (currentController.EqualsWithoutCase(controller) && currentActionName.EqualsWithoutCase(action) && (currentSlug == null || currentSlug.EqualsWithoutCase(newSlug)))
            {

                liTag.MergeAttribute("class", "active");
                linkTag.Attributes.Add("href", url);
                linkTag.MergeAttributes(attribute);
                linkTag.InnerHtml = linkText;
                liTag.InnerHtml = linkTag.ToString(TagRenderMode.Normal);
            }
            else
            {
                linkTag.Attributes.Add("href", url);
                linkTag.MergeAttributes(attribute);
                linkTag.InnerHtml = linkText;
                liTag.InnerHtml = linkTag.ToString(TagRenderMode.Normal);
            }

            return MvcHtmlString.Create(liTag.ToString(TagRenderMode.Normal));
        }
    }
}