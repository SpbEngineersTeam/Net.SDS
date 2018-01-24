using System;

namespace Service.A
{
    public abstract class RegistrationInfoBase
    {
        public abstract Guid ServiceId { get; }
        public abstract Uri ServiceUri { get; }
    }
}
