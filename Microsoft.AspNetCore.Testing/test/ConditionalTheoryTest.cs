// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using Microsoft.AspNetCore.Testing.xunit;
using Xunit;
using Xunit.Abstractions;

namespace Microsoft.AspNetCore.Testing
{
    public class ConditionalTheoryTest
    {
        [ConditionalTheory]
        [MemberData(nameof(SkippableData))]
        public void WithSkipableData(Skippable skippable)
        {
            Assert.Null(skippable.Skip);
            Assert.Equal(1, skippable.Data);
        }

        public static TheoryData<Skippable> SkippableData => new TheoryData<Skippable>
        {
            new Skippable() { Data = 1 },
            new Skippable() { Data = 2, Skip = "This row should be skipped." }
        };

        public class Skippable : IXunitSerializable
        {
            public Skippable() { }
            public int Data { get; set; }
            public string Skip { get; set; }

            public void Serialize(IXunitSerializationInfo info)
            {
                info.AddValue(nameof(Data), Data, typeof(int));
            }

            public void Deserialize(IXunitSerializationInfo info)
            {
                Data = info.GetValue<int>(nameof(Data));
            }

            public override string ToString()
            {
                return Data.ToString();
            }
        }
    }
}
