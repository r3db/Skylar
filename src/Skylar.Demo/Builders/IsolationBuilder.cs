using System;
using System.Collections.Generic;
using Epiphany;
using Epiphany.Extensions;

namespace Skylar
{
    internal sealed class IsolationBuilder : FluentBuilder<Isolation>
    {
        #region Internal Static Data

        private static readonly IList<string> _availableStates = new[]
        {
            "draft",
            "authorized",
            "awaiting authorization",
            "awaiting issue",
            "closed",
            "closing",
            "closing (without wo)",
            "expired",
            "finalizing jha",
            "overdue (awaiting suspension)",
        };

        private static readonly IList<string> _availableUserNames = new[]
        {
            "evision",
            "ricardo",
            "neil.c",
            "john.m",
            "obama",
            "joão",
            "jordi",
            "ricardo.bartolomeu",
            "jordi.sabat",
        };

        #endregion

        #region .Ctor

        private IsolationBuilder(Isolation isolation)
            : base(isolation)
        {

        }

        #endregion

        #region Factory .Ctor

        internal static IsolationBuilder Begin
        {
            get
            {
                var isolation = new Isolation
                {
                    Number = "{0}".ToInvariantFormat(BaseArbitrary.NextInt32(0, 1000)),
                    TagNumber = "{0}".ToInvariantFormat(BaseArbitrary.NextInt32(0, 1000)),
                    ReasonForIsolation = BaseArbitrary.NextString(),
                    LongTermIsolation = BaseArbitrary.NextString(),
                    State = BaseArbitrary.NextItem(_availableStates),
                    SignedBy = BaseArbitrary.NextItem(_availableUserNames),
                    SignedAt = DateTime.Now - TimeSpan.FromDays(BaseArbitrary.NextInt32(0, 10000)),
                    Created = DateTime.Now - TimeSpan.FromDays(BaseArbitrary.NextInt32(0, 1000)),
                };

                return new IsolationBuilder(isolation);
            }
        }

        #endregion
    }
}