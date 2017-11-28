using System;
using System.Collections.Generic;
using Epiphany;
using Epiphany.Extensions;

namespace Skylar
{
    internal sealed class PermitBuilder : FluentBuilder<Permit>
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

        private static readonly IList<string> _availableTypes = new[]
        {
            "cold work",
            "spark potential",
            "hot work",
            "radiography",
            "driver permit",
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

        private PermitBuilder(Permit permit)
            : base(permit)
        {

        }

        #endregion

        #region Factory .Ctor

        internal static PermitBuilder Begin
        {
            get
            {
                var permit = new Permit
                {
                    Number = "{0}".ToInvariantFormat(BaseArbitrary.NextInt32(0, 1000)),
                    WorkDescription = BaseArbitrary.NextString(),
                    State = BaseArbitrary.NextItem(_availableStates),
                    Type = BaseArbitrary.NextItem(_availableTypes),
                    SignedBy = BaseArbitrary.NextItem(_availableUserNames),
                    SignedAt = DateTime.Now - TimeSpan.FromDays(BaseArbitrary.NextInt32(0, 10000)),
                    Created = DateTime.Now - TimeSpan.FromDays(BaseArbitrary.NextInt32(0, 1000)),
                };

                return new PermitBuilder(permit);
            }
        }

        #endregion
    }
}