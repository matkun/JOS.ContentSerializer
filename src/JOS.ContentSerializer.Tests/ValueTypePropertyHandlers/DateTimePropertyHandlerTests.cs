﻿using System;
using JOS.ContentSerializer.Internal.Default.ValueTypePropertyHandlers;
using JOS.ContentSerializer.Tests.Pages;
using Shouldly;
using Xunit;

namespace JOS.ContentSerializer.Tests.ValueTypePropertyHandlers
{
    public class DateTimePropertyHandlerTests
    {
        private readonly DateTimePropertyHandler _sut;
        public DateTimePropertyHandlerTests()
        {
            this._sut = new DateTimePropertyHandler();
        }

        [Fact]
        public void GivenDateTimeProperty_WhenHandle_ThenReturnsCorrectValue()
        {
            var contentSerializerSettings = new ContentSerializerSettings();
            var expected = new DateTime(2000, 01, 01, 12, 00, 30);
            var page = new ValueTypePropertyHandlerPage
            {
                DateTime = expected
            };

            var result = (DateTime)this._sut.Handle(
                page.DateTime,
                page.GetType().GetProperty(nameof(ValueTypePropertyHandlerPage.DateTime)),
                page,
                contentSerializerSettings);

            result.ShouldBe(expected);
        }
    }
}
