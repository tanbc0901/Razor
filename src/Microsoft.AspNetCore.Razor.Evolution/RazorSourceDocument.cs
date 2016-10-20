﻿// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.IO;
using System.Text;

namespace Microsoft.AspNetCore.Razor.Evolution
{
    public abstract class RazorSourceDocument
    {
        public static RazorSourceDocument ReadFrom(Stream stream, string filename)
        {
            if (stream == null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            return ReadFromInternal(stream, filename, encoding: null);
        }

        public static RazorSourceDocument ReadFrom(Stream stream, string filename, Encoding encoding)
        {
            if (stream == null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            if (encoding == null)
            {
                throw new ArgumentNullException(nameof(encoding));
            }

            return ReadFromInternal(stream, filename, encoding);
        }

        private static RazorSourceDocument ReadFromInternal(Stream stream, string filename, Encoding encoding)
        {
            var memoryStream = new MemoryStream();
            stream.CopyTo(memoryStream);

            return new DefaultRazorSourceDocument(memoryStream, encoding, filename);
        }

        public abstract string Filename { get; }

        public abstract TextReader CreateReader();
    }
}