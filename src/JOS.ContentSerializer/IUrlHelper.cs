using EPiServer;
using EPiServer.Core;

namespace JOS.ContentSerializer
{
    public interface IUrlHelper
    {
        string ContentUrl(Url url, IContentSerializerSettings contentSerializerSettings);
        string ContentUrl(Url url, IUrlSettings urlSettings);
        string ContentUrl(ContentReference contentReference, IContentSerializerSettings contentSerializerSettings);
        string ContentUrl(ContentReference contentReference, IUrlSettings contentReferenceSettings);
    }
}