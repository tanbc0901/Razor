// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Reflection;
using Xunit;
using Xunit.Sdk;

namespace Microsoft.AspNetCore.Razor.Language.Legacy
{
    public class IntializeTestFileAttribute : BeforeAfterTestAttribute
    {
        private static int _callCount = 0;
        private static string _previousMethod;

        public override void Before(MethodInfo methodUnderTest)
        {
            if (typeof(ParserTestBase).GetTypeInfo().IsAssignableFrom(methodUnderTest.DeclaringType.GetTypeInfo()))
            {
                var typeName = methodUnderTest.DeclaringType.Name;
                ParserTestBase.FileName = $"TestFiles/ParserTests/{typeName}/{methodUnderTest.Name}";

                if (methodUnderTest.GetCustomAttributes(typeof(TheoryAttribute), inherit: false).Length > 0)
                {
                    if (!string.IsNullOrEmpty(_previousMethod) &&
                        !_previousMethod.Equals(methodUnderTest.Name, StringComparison.Ordinal))
                    {
                        // This is the start of a different Theory test. Reset the call count.
                        _callCount = 0;
                    }
                    
                    // Append the call count to the file name to differentiate test cases within a Theory test.
                    ParserTestBase.FileName += _callCount++;
                }
            }
        }

        public override void After(MethodInfo methodUnderTest)
        {
            if (typeof(ParserTestBase).GetTypeInfo().IsAssignableFrom(methodUnderTest.DeclaringType.GetTypeInfo()))
            {
                ParserTestBase.FileName = null;
                _previousMethod = methodUnderTest.Name;
            }
        }
    }
}
