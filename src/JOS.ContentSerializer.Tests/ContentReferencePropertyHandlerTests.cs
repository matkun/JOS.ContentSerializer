using EPiServer.Core;
using JOS.ContentSerializer.Internal.Default;
using NSubstitute;
using Shouldly;
using Xunit;

namespace JOS.ContentSerializer.Tests
{
    public class ContentReferencePropertyHandlerTests
    {
        private readonly ContentReferencePropertyHandler _sut;
        private readonly IUrlHelper _urlHelper;

        public ContentReferencePropertyHandlerTests()
        {
            this._urlHelper = Substitute.For<IUrlHelper>();
            this._sut = new ContentReferencePropertyHandler(this._urlHelper);
        }

        [Fact]
        public void GivenNullContentReference_WhenHandle_ThenReturnsNull()
        {
            var contentSerializerSettings = new ContentSerializerSettings();
            var result = this._sut.Handle(null, null, null, contentSerializerSettings);

            result.ShouldBeNull();
        }

        [Fact]
        public void GivenEmptyContentReference_WhenHandle_ThenReturnsNull()
        {
            var contentSerializerSettings = new ContentSerializerSettings();
            var result = this._sut.Handle(ContentReference.EmptyReference, null, null, contentSerializerSettings);

            result.ShouldBeNull();
        }

        [Fact]
        public void GivenContentReference_WhenHandle_ThenReturnsAbsoluteUrlString()
        {
            var contentSerializerSettings = new ContentSerializerSettings();
            var host = "example.com";
            var scheme = "https://";
            var baseUrl = $"{scheme}{host}";
            var prettyPath = "/any-path/to/page/?anyQueryParam=value&anotherQuery";
            var contentReference = new ContentReference(1000);
            this._urlHelper.ContentUrl(contentReference, contentSerializerSettings.UrlSettings)
                .Returns($"{baseUrl}{prettyPath}");

            var result = this._sut.Handle(contentReference, null, null, contentSerializerSettings);

            result.ShouldBe($"{baseUrl}{prettyPath}");
        }

        [Fact]
        public void GivenContentReference_WhenHandleWithUseAbsoluteUrlsSetToFalse_ThenReturnsRelativeUrlString()
        {
            var contentSerializerSettings = new ContentSerializerSettings();
            var host = "example.com";
            var scheme = "https://";
            var baseUrl = $"{scheme}{host}";
            var prettyPath = "/any-path/to/page/?anyQueryParam=value&anotherQuery";
            var contentReference = new ContentReference(1000);
            contentSerializerSettings.UrlSettings.UseAbsoluteUrls = false;
            this._urlHelper.ContentUrl(Arg.Any<ContentReference>(), contentSerializerSettings.UrlSettings)
                .Returns($"{baseUrl}{prettyPath}");

            var result = this._sut.Handle(contentReference, null, null, contentSerializerSettings);

            result.ShouldBe(prettyPath);
        }
    }
}
