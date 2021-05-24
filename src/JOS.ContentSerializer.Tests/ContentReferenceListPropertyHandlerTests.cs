﻿using System.Collections.Generic;
using System.Linq;
using EPiServer.Core;
using JOS.ContentSerializer.Internal.Default;
using NSubstitute;
using Shouldly;
using Xunit;

namespace JOS.ContentSerializer.Tests
{
    public class ContentReferenceListPropertyHandlerTests
    {
        private readonly ContentReferenceListPropertyHandler _sut;
        private readonly IUrlHelper _urlHelper;

        public ContentReferenceListPropertyHandlerTests()
        {
            this._urlHelper = Substitute.For<IUrlHelper>();
            this._sut = new ContentReferenceListPropertyHandler(new ContentReferencePropertyHandler(this._urlHelper));
        }

        [Fact]
        public void GivenNullList_WhenHandle_ThenReturnsNull()
        {
            var result = this._sut.Handle(null, null, null, null);

            result.ShouldBeNull();
        }

        [Fact]
        public void GivenEmptyList_WhenHandle_ThenReturnsEmptyList()
        {
            var contentSerializerSettings = new ContentSerializerSettings();
            var contentReferences = Enumerable.Empty<ContentReference>();

            var result = this._sut.Handle(contentReferences, null, null, contentSerializerSettings);

            ((IEnumerable<object>)result).ShouldBeEmpty();
        }

        [Fact]
        public void GivenContentReferences_WhenHandle_ThenReturnsEmptyList()
        {
            var contentSerializerSettings = new ContentSerializerSettings();
            var host = "example.com";
            var scheme = "https://";
            var baseUrl = $"{scheme}{host}";
            var prettyPath = "/any-path/to/page/?anyQueryParam=value&anotherQuery";
            var contentReference = new ContentReference(1000);
            var contentReferences = new List<ContentReference>{contentReference};

            this._urlHelper.ContentUrl(contentReference, contentSerializerSettings.UrlSettings)
                .Returns($"{baseUrl}{prettyPath}");

            var result = this._sut.Handle(contentReferences, null, null, contentSerializerSettings);
            var items = ((IEnumerable<object>)result).Cast<string>().ToList();

            items.Count.ShouldBe(1);
            items.First().ShouldBe($"{baseUrl}{prettyPath}");
        }
    }
}
