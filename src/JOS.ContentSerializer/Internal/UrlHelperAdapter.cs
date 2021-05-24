﻿using System;
using System.Web.Mvc;
using EPiServer;
using EPiServer.Core;
using EPiServer.Web;
using EPiServer.Web.Mvc.Html;

namespace JOS.ContentSerializer.Internal
{
    public class UrlHelperAdapter : IUrlHelper
    {
        private readonly UrlHelper _urlHelper;
        private readonly ISiteDefinitionResolver _siteDefinitionResolver;
        private readonly IRequestHostResolver _requestHostResolver;

        public UrlHelperAdapter(
            UrlHelper urlHelper,
            ISiteDefinitionResolver siteDefinitionResolver,
            IRequestHostResolver requestHostResolver)
        {
            _requestHostResolver = requestHostResolver ?? throw new ArgumentNullException(nameof(requestHostResolver));
            _urlHelper = urlHelper ?? throw new ArgumentNullException(nameof(urlHelper));
            _siteDefinitionResolver = siteDefinitionResolver ?? throw new ArgumentNullException(nameof(siteDefinitionResolver));
        }

        public string ContentUrl(Url url, IContentSerializerSettings contentSerializerSettings)
        {
            return Execute(url, contentSerializerSettings.UrlSettings);
        }

        public string ContentUrl(Url url, IUrlSettings urlSettings)
        {
            return Execute(url, urlSettings);
        }

        public string ContentUrl(ContentReference contentReference, IContentSerializerSettings contentSerializerSettings)
        {
            return Execute(contentReference, contentSerializerSettings.UrlSettings);
        }

        public string ContentUrl(ContentReference contentReference, IUrlSettings urlSettings)
        {
            return Execute(contentReference, urlSettings);
        }

        private string Execute(Url url, IUrlSettings urlSettings)
        {
            var prettyUrl = this._urlHelper.ContentUrl(url);
            if (urlSettings.UseAbsoluteUrls)
            {
                return CreateAbsoluteUrl(prettyUrl, urlSettings.FallbackToWildcard);
            }

            return prettyUrl;
        }

        private string Execute(ContentReference contentReference, IUrlSettings urlSettings)
        {
            var prettyUrl = this._urlHelper.ContentUrl(contentReference);
            if (urlSettings.UseAbsoluteUrls)
            {
                return CreateAbsoluteUrl(prettyUrl, urlSettings.FallbackToWildcard);
            }

            return prettyUrl;
        }

        private string CreateAbsoluteUrl(string relativeUrl, bool fallbackToWildcard)
        {
            var siteDefinition = this._siteDefinitionResolver.GetByHostname(
                this._requestHostResolver.HostName,
                fallbackToWildcard
            );
            var uri = new Uri(siteDefinition.SiteUrl, relativeUrl);
            return uri.AbsoluteUri;
        }
    }
}
