// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Microsoft.AspNetCore.Razor.Language
{
    public class SimpleParserTest
    {
        [Fact]
        public void BasicSyntaxTreeSerializerTest()
        {
            // Arrange
            var sourceDocument = TestRazorSourceDocument.Create("@is foo");
            var syntaxTree = RazorSyntaxTree.Parse(sourceDocument, RazorParserOptions.CreateDefault());

            // Act
            //while (!System.Diagnostics.Debugger.IsAttached);
            var result = SyntaxTreeNodeSerializer.Serialize(syntaxTree.Root);

            // Assert
            Assert.False(true, result);
        }
    }
}
