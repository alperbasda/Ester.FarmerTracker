﻿using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Hosting;
using Notification.Entities;

namespace Notification.Helpers
{
    public class MailTemplateHelper : IMailTemplateHelper
    {
        private IRazorViewEngine _razorViewEngine;
        private IServiceProvider _serviceProvider;
        private ITempDataProvider _tempDataProvider;
        private Microsoft.AspNetCore.Hosting.IHostingEnvironment _hostingEnvironment;

        public MailTemplateHelper(ITempDataProvider tempDataProvider, IServiceProvider serviceProvider, IRazorViewEngine razorViewEngine, Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment)
        {
            _tempDataProvider = tempDataProvider;
            _serviceProvider = serviceProvider;
            _razorViewEngine = razorViewEngine;
            _hostingEnvironment = hostingEnvironment;
        }

        public async Task<string> GetTemplateHtmlAsStringAsync(string viewName, IPostModel model)
        {
            var httpContext = new DefaultHttpContext()
            {
                RequestServices = _serviceProvider
            };
            var actionContext = new ActionContext(
                httpContext, new RouteData(), new ActionDescriptor());

            using (StringWriter sw = new StringWriter())
            {
                var viewResult = _razorViewEngine.FindView(
                     actionContext, viewName, false);

                if (!viewResult.Success)
                {
                    return string.Empty;
                }

                var viewDataDictionary = new ViewDataDictionary(
                    new EmptyModelMetadataProvider(),
                    new ModelStateDictionary()
                )
                {
                    Model = model
                };

                var viewContext = new ViewContext(
                    actionContext,
                    viewResult.View,
                    viewDataDictionary,
                    new TempDataDictionary(
                        actionContext.HttpContext, _tempDataProvider),
                    sw,
                    new HtmlHelperOptions()
                );

                await viewResult.View.RenderAsync(viewContext);

                return sw.ToString();
            }
        }

    }
}
