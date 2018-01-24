using System;

namespace Service.A
{
    [Flags]
    enum HealthFlags//TODO: to SDS.Abstractions
    {
        All = 1 << 0
    }
}
