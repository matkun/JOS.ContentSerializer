﻿using EPiServer;
using JOS.ContentSerializer.Internal.Default;
using NSubstitute;
using Shouldly;
using Xunit;

namespace JOS.ContentSerializer.Tests
{
    public class UrlPropertyHandlerTests
    {
        private readonly UrlPropertyHandler _sut;
        private readonly IUrlHelper _urlHelper;
        
        public UrlPropertyHandlerTests()
        {
            this._urlHelper = Substitute.For<IUrlHelper>();
            this._sut = new UrlPropertyHandler(this._urlHelper);
        }

        [Fact]
        public void GivenNullUrl_WhenHandle_ThenReturnsNull()
        {
            var result = this._sut.Handle(null, null, null, null);

            result.ShouldBeNull();
        }

        [Fact]
        public void GivenMailToUrl_WhenHandle_ThenReturnsCorrectValue()
        {
            var contentSerializerSettings = new ContentSerializerSettings();
            var value = "mailto:mail@example.com";
            var url = new Url(value);

            var result = this._sut.Handle(url, null, null, contentSerializerSettings);

            result.ShouldBe(value);
        }

        [Fact]
        public void GivenExternalLink_WhenHandle_ThenReturnsAbsoulteUrlWithQuery()
        {
            var contentSerializerSettings = new ContentSerializerSettings();
            var value = "https://josef.guru/example/page?anyQueryString=true&anyOtherQuery";
            var url = new Url(value);

            var result = this._sut.Handle(url, null, null, contentSerializerSettings);

            result.ShouldBe(value);
        }

        [Fact]
        public void GivenExternalLink_WhenHandleWithAbsoluteUrlSetToFalse_ThenReturnsRelativeUrlWithQuery()
        {
            var contentSerializerSettings = new ContentSerializerSettings();
            contentSerializerSettings.UrlSettings.UseAbsoluteUrls = false;
            var value = "https://josef.guru/example/page?anyQueryString=true&anyOtherQuery";
            var url = new Url(value);

            var result = this._sut.Handle(url, null, null, contentSerializerSettings);

            result.ShouldBe(url.PathAndQuery);
        }

        [Fact]
        public void GivenEpiserverPage_WhenHandle_ThenReturnsRewrittenPrettyAbsoluteUrl()
        {
            var contentSerializerSettings = new ContentSerializerSettings();
            var siteUrl = "https://example.com";
            var prettyPath = "/rewritten/pretty-url/";
            var value = "/link/d40d0056ede847d5a2f3b4a02778d15b.aspx";
            var url = new Url(value);
            this._urlHelper.ContentUrl(Arg.Any<Url>(), contentSerializerSettings.UrlSettings).Returns($"{siteUrl}{prettyPath}");
         
            var result = this._sut.Handle(url, null, null, contentSerializerSettings);

            result.ShouldBe($"{siteUrl}{prettyPath}");
        }

        [Fact]
        public void GivenEpiserverPage_WhenHandleWithAbsoluteUrlSetToFalse_ThenReturnsRewrittenPrettyRelativeUrl()
        {
            var contentSerializerSettings = new ContentSerializerSettings();
            var prettyPath = "/rewritten/pretty-url/";
            var value = "/link/d40d0056ede847d5a2f3b4a02778d15b.aspx";
            var url = new Url(value);
            contentSerializerSettings.UrlSettings.UseAbsoluteUrls = false;
            this._urlHelper.ContentUrl(Arg.Any<Url>(), contentSerializerSettings.UrlSettings).Returns(prettyPath);

            var result = this._sut.Handle(url, null, null, contentSerializerSettings);

            result.ShouldBe(prettyPath);
        }
    }
}
