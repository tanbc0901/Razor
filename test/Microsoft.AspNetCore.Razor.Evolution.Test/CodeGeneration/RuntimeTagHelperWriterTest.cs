﻿// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.AspNetCore.Razor.Evolution.Intermediate;
using Xunit;

namespace Microsoft.AspNetCore.Razor.Evolution.CodeGeneration
{
    public class RuntimeTagHelperWriterTest
    {
        [Fact]
        public void WriteDeclareTagHelperFields_DeclaresRequiredFields()
        {
            // Arrange
            var writer = new RuntimeTagHelperWriter();
            var context = new CSharpRenderingContext()
            {
                Writer = new Legacy.CSharpCodeWriter(),
            };
            var node = new DeclareTagHelperFieldsIRNode();

            // Act
            writer.WriteDeclareTagHelperFields(context, node);

            // Assert
            var csharp = context.Writer.Builder.ToString();
            Assert.Equal(
@"#line hidden
#pragma warning disable 0414
private string __tagHelperStringValueBuffer = null;
#pragma warning restore 0414
private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext = null;
private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
{
    get
    {
        if (__backed__tagHelperScopeManager == null)
        {
            __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
        }
        return __backed__tagHelperScopeManager;
    }
}
",
                csharp,
                ignoreLineEndingDifferences: true);
        }

        [Fact]
        public void WriteDeclareTagHelperFields_DeclaresUsedTagHelperTypes()
        {
            // Arrange
            var writer = new RuntimeTagHelperWriter();
            var context = new CSharpRenderingContext()
            {
                Writer = new Legacy.CSharpCodeWriter(),
            };
            var node = new DeclareTagHelperFieldsIRNode();
            node.UsedTagHelperTypeNames.Add("PTagHelper");
            node.UsedTagHelperTypeNames.Add("MyTagHelper");

            // Act
            writer.WriteDeclareTagHelperFields(context, node);

            // Assert
            var csharp = context.Writer.Builder.ToString();
            Assert.Equal(
@"#line hidden
#pragma warning disable 0414
private string __tagHelperStringValueBuffer = null;
#pragma warning restore 0414
private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext = null;
private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
{
    get
    {
        if (__backed__tagHelperScopeManager == null)
        {
            __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
        }
        return __backed__tagHelperScopeManager;
    }
}
private global::PTagHelper __PTagHelper = null;
private global::MyTagHelper __MyTagHelper = null;
",
                csharp,
                ignoreLineEndingDifferences: true);
        }

        [Fact]
        public void WriteInitializeTagHelperStructure_RendersCorrectly_UsesTagNameAndModeFromIRNode()
        {
            // Arrange
            var writer = new RuntimeTagHelperWriter();
            var context = new CSharpRenderingContext()
            {
                Writer = new Legacy.CSharpCodeWriter(),
                IdGenerator = () => "test",
                RenderChildren = n => { }
            };
            var node = new InitializeTagHelperStructureIRNode()
            {
                TagName = "p",
                TagMode = TagMode.SelfClosing
            };

            // Act
            writer.WriteInitializeTagHelperStructure(context, node);

            // Assert
            var csharp = context.Writer.Builder.ToString();
            Assert.Equal(
@"__tagHelperExecutionContext = __tagHelperScopeManager.Begin(""p"", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, ""test"", async() => {
}
);
",
                csharp,
                ignoreLineEndingDifferences: true);
        }
    }
}