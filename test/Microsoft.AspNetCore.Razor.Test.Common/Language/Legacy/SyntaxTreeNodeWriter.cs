// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.IO;

namespace Microsoft.AspNetCore.Razor.Language.Legacy
{
    internal class SyntaxTreeNodeWriter : ParserVisitor
    {
        private readonly TextWriter _writer;

        public int Depth { get; set; }

        public SyntaxTreeNodeWriter(TextWriter writer)
        {
            _writer = writer;
        }

        protected override void VisitDefault(Block block)
        {
            WriteBlock(block);
        }

        public override void VisitBlock(Block block)
        {
            WriteBlock(block);
        }

        public override void VisitSpan(Span span)
        {
            WriteSpan(span);
        }

        public override void VisitTagHelperBlock(TagHelperChunkGenerator chunkGenerator, Block block)
        {
            WriteBlock(block);
            
            if (block is TagHelperBlock tagHelperBlock)
            {
                WriteSeparator();
                _writer.Write(tagHelperBlock.TagName);
            }
        }

        public override void VisitMarkupSpan(MarkupChunkGenerator chunkGenerator, Span span)
        {
            WriteSpan(span);
        }

        protected void WriteBlock(Block block)
        {
            WriteIndent();
            _writer.Write($"{block.Type} block");
            WriteSeparator();
            _writer.Write(block.ChunkGenerator.GetType().Name);
            WriteSeparator();
            _writer.Write(block.Length);
            WriteSeparator();
            _writer.Write(block.Start);
        }

        protected void WriteSpan(Span span)
        {
            WriteIndent();
            _writer.Write($"{span.Kind} span");
            WriteSeparator();
            _writer.Write(span.ChunkGenerator.GetType().Name);
            WriteSeparator();
            _writer.Write(span.EditHandler);
            WriteSeparator();
            _writer.Write($"Accepts {span.EditHandler.AcceptedCharacters}");
            WriteSeparator();
            _writer.Write(span.Content);
            WriteSeparator();
            _writer.Write(span.Start);
        }

        protected void WriteIndent()
        {
            for (var i = 0; i < Depth; i++)
            {
                for (var j = 0; j < 4; j++)
                {
                    _writer.Write(' ');
                }
            }
        }

        protected void WriteSeparator()
        {
            _writer.Write(" - ");
        }

        protected void WriteNewLine()
        {
            _writer.WriteLine();
        }
    }
}