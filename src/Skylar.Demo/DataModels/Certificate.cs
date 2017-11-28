using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Skylar
{
    public abstract class Certificate
    {
        protected Certificate(CertificateKind kind)
        {
            Kind = kind;
        }

        [JsonConverter(typeof(StringEnumConverter))]
        public CertificateKind Kind { get; private set; }

        public string Number { get; set; }

        public string SignedBy { get; set; }

        public DateTime SignedAt { get; set; }

        public string State { get; set; }

        public DateTime Created { get; set; }
    }
}