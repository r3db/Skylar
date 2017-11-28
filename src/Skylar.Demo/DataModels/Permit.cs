using System;

namespace Skylar
{
    public sealed class Permit : Certificate
    {
        public Permit()
            : base(CertificateKind.Permit)
        {
        }

        public string WorkDescription { get; set; }

        public string Type { get; set; }
    }
}