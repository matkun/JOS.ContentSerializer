using System;
using System.Reflection;
using EPiServer.Core;

namespace JOS.ContentSerializer.Internal.Default
{
    public class BlockDataPropertyHandler : IPropertyHandler<BlockData>
    {
        private readonly IPropertyManager _propertyManager;

        public BlockDataPropertyHandler(IPropertyManager propertyManager)
        {
            _propertyManager = propertyManager ?? throw new ArgumentNullException(nameof(propertyManager));
        }

        public object Handle(BlockData value, PropertyInfo property, IContentData contentData, IContentSerializerSettings contentSerializerSettings)
        {
            return this._propertyManager.GetStructuredData(value, contentSerializerSettings);
        }
    }
}
