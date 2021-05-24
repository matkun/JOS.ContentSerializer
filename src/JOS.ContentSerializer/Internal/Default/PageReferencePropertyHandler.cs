﻿using System;
using System.Reflection;
using EPiServer.Core;

namespace JOS.ContentSerializer.Internal.Default
{
    public class PageReferencePropertyHandler : IPropertyHandler<PageReference>
    {
        private readonly IPropertyHandler<ContentReference> _contentReferencePropertyHandler;

        public PageReferencePropertyHandler(IPropertyHandler<ContentReference> contentReferencePropertyHandler)
        {
            _contentReferencePropertyHandler = contentReferencePropertyHandler ?? throw new ArgumentNullException(nameof(contentReferencePropertyHandler));
        }

        public object Handle(PageReference value, PropertyInfo property, IContentData contentData, IContentSerializerSettings contentSerializerSettings)
        {
            return this._contentReferencePropertyHandler.Handle(value, property, contentData, contentSerializerSettings);
        }
    }
}
