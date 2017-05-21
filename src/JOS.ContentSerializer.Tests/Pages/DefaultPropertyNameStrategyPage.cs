﻿using EPiServer.Core;
using JOS.ContentSerializer.Attributes;

namespace JOS.ContentSerializer.Tests.Pages
{
    public class DefaultPropertyNameStrategyPage : PageData
    {
        public virtual string Heading { get; set; }

        [ContentSerializerName("customAuthor")]
        public virtual string Author { get; set; }
    }
}
