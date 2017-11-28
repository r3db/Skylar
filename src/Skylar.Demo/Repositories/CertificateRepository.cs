using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Epiphany.Extensions;

namespace Skylar
{
    public static class CertificateRepository
    {
        #region Internal Static Data

        private static readonly IList<Certificate> _certificates = new Certificate[]
        {
            new Permit
            {
                Number = "P-005",
                SignedAt = DateTime.Now,
                SignedBy = "evision",
                State = "draft",
                Type = "cold work",
                WorkDescription = "Some description",
                Created = DateTime.Now - TimeSpan.FromDays(60),
            },

            new Permit
            {
                Number = "P-009",
                SignedAt = DateTime.Now,
                SignedBy = "evision",
                State = "draft",
                Type = "cold work",
                WorkDescription = "Some description",
                Created = DateTime.Now - TimeSpan.FromDays(60),
            },

            PermitBuilder.Begin.Build(),
            PermitBuilder.Begin.Build(),
            PermitBuilder.Begin.Build(),
            PermitBuilder.Begin.Build(),
            PermitBuilder.Begin.Build(),
            PermitBuilder.Begin.Build(),
            PermitBuilder.Begin.Build(),
            PermitBuilder.Begin.Build(),
            PermitBuilder.Begin.Build(),
            PermitBuilder.Begin.Build(),
            PermitBuilder.Begin.Build(),
            PermitBuilder.Begin.Build(),
            PermitBuilder.Begin.Build(),
            PermitBuilder.Begin.Build(),
            PermitBuilder.Begin.Build(),
            PermitBuilder.Begin.Build(),
            PermitBuilder.Begin.Build(),
            PermitBuilder.Begin.Build(),
            PermitBuilder.Begin.Build(),
            PermitBuilder.Begin.Build(),
            PermitBuilder.Begin.Build(),
            PermitBuilder.Begin.Build(),
            PermitBuilder.Begin.Build(),
            PermitBuilder.Begin.Build(),
            PermitBuilder.Begin.Build(),

            IsolationBuilder.Begin.Build(),
            IsolationBuilder.Begin.Build(),
            IsolationBuilder.Begin.Build(),
            IsolationBuilder.Begin.Build(),
            IsolationBuilder.Begin.Build(),
            IsolationBuilder.Begin.Build(),
            IsolationBuilder.Begin.Build(),
            IsolationBuilder.Begin.Build(),
            IsolationBuilder.Begin.Build(),
            IsolationBuilder.Begin.Build(),
            IsolationBuilder.Begin.Build(),
        };

        #endregion

        #region Methods

        public static IEnumerable<Certificate> Find(QueryDefinitionInfo query)
        {
            var result = new List<Certificate>();
            var cache = new Dictionary<CertificateKind, IDictionary<string, PropertyInfo>>();

            if (query.Target == null)
            {
                return Enumerable.Empty<Certificate>();
            }

            if (query.Properties.IsEmpty())
            {
                return _certificates.Where(x => "{0}".ToInvariantFormat(x.Kind).ToLowerInvariant().OrdinalEquals(query.Target.ToLowerInvariant())).ToList();
            }

            foreach (var item in _certificates)
            {
                if ("{0}".ToInvariantFormat(item.Kind).ToLowerInvariant().OrdinalEquals(query.Target.ToLowerInvariant()) == false)
                {
                    continue;
                }

                if (cache.ContainsKey(item.Kind) == false)
                {
                    cache[item.Kind] = item.GetType()
                        .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                        .ToDictionary(x => x.Name.ToLowerInvariant(), x => x);
                }

                bool canAdd = true;

                foreach (var attribute in query.Properties)
                {
                    var key1 = attribute.Key.Content.ToLowerInvariant();
                    var key2 = CreateClrProperty(key1).ToLowerInvariant();

                    if (cache[item.Kind].ContainsKey(key1))
                    {
                        if (attribute.Value.Type == null)
                        {
                            var value = cache[item.Kind][key1].GetValue(item);

                            if (value == null || value.Equals(attribute.Value.Content) == false)
                            {
                                canAdd = false;
                                continue;
                            }
                        }
                        
                        if (attribute.Value.Type == typeof(TimeSpan))
                        {
                            var value = cache[item.Kind][key1].GetValue(item);

                            if ((value is DateTime) == false)
                            {
                                canAdd = false;
                            }

                            var dbDate = ((DateTime)value).Date;
                            var qdDate = (DateTime.Now - (TimeSpan)attribute.Value.Hint).Date;

                            if (dbDate != qdDate)
                            {
                                canAdd = false;
                                continue;
                            }
                        }
                    }
                    else if (cache[item.Kind].ContainsKey(key2))
                    {
                        var value = cache[item.Kind][key2].GetValue(item);

                        if (value == null || value.Equals(attribute.Value.Content) == false)
                        {
                            canAdd = false;
                        }
                    }
                }

                if (canAdd)
                {
                    result.Add(item);
                }
            }

            return result;
        }

        private static string CreateClrProperty(string name)
        {
            var parts = name.Split(new[] {" "}, StringSplitOptions.RemoveEmptyEntries);
            var sb = new StringBuilder();

            foreach (var item in parts)
            {
                switch (item.Length)
                {
                    case 1:
                    {
                        sb.Append(item);
                        break;
                    }
                    default:
                    {
                        sb.Append(char.ToUpperInvariant(item[0]) + item.Substring(1).ToLowerInvariant());
                        break;
                    }
                }
            }

            return sb.ToString();
        }

        #endregion
    }
}