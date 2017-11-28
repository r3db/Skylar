using System;

namespace Skylar
{
    public sealed class Isolation : Certificate
    {
        public Isolation()
            : base(CertificateKind.Isolation)
        {
        }

        public string TagNumber { get; set; }

        public string ReasonForIsolation { get; set; }

        public string LongTermIsolation { get; set; }
    }
}