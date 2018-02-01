using System;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace Service.A
{
    internal abstract class RegistrationHandlerBase
    {
        private readonly System.Timers.Timer _periodicalRegistration;
        private double _invokeRegistrationInterval = TimeSpan.FromMinutes(1).TotalMilliseconds;//TODO: в настройки
        private int _firstTimeRegistrationTimeoutMs = 1000;
        private bool _isRegistrationLoopStarted = false;
        private readonly object _lock = new object();

        protected RegistrationHandlerBase()
        {
            _periodicalRegistration = new System.Timers.Timer();
            _periodicalRegistration.Elapsed += (sender, e) => RegisterInternal();
            _periodicalRegistration.AutoReset = true;
            _periodicalRegistration.Interval = _invokeRegistrationInterval;
            _periodicalRegistration.Enabled = false;
        }

        protected abstract void Register();

        internal void RunRegistrationLoop()
        {
            if (!_isRegistrationLoopStarted)
            {
                lock (_lock)
                {
                    if (!_isRegistrationLoopStarted)
                    {
                        Task.Run(() => RegisterFirstTime())
                            .ContinueWith(_ => _periodicalRegistration.Start());

                        _isRegistrationLoopStarted = true;
                    }
                }
            }
        }

        private void RegisterFirstTime()
        {
            while (!RegisterInternal())
            {
                Thread.Sleep(_firstTimeRegistrationTimeoutMs);
            }
        }

        private bool RegisterInternal()
        {
            try
            {
                Register();
                return true;
            }
            catch (Exception)
            {
                //TODO: что с логами?
                return false;
            }
        }


    }

    //TODO: брать из нугета Net.SDS.Registry.Core.Abstractions
    public class ServiceInstanceDto
    {
        public string Url { get; set; }
    }
}
