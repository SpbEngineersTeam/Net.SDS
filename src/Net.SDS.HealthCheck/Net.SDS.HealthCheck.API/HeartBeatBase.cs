using System;
using System.Threading;
using System.Threading.Tasks;

namespace Net.SDS.HealthCheck.API
{
	/// <inheritdoc />
	/// <summary>
	/// Базовый класс сообщающий реестру сервисов о существовании сервиса.
	/// </summary>
	internal abstract class HeartBeatBase: IDisposable
	{
		/// <summary>
		/// Выполняет отправку данных о сервисе в реестр.
		/// </summary>
		protected abstract Task Beat(CancellationToken token);

		/// <inheritdoc />
		public void Dispose()
		{
			CheckDisposed();
			lock (_lock) { 
				Dispose(true);
			}
		}

		/// <summary>
		/// Выполняет освобождение упровляемых ресурсов.
		/// <remarks>Не является потоко-безопасным.</remarks>
		/// </summary>
		/// <param name="disposing"></param>
		protected virtual void Dispose(bool disposing)
		{
			_cancellationTokenSource.Cancel();
			_cancellationTokenSource.Dispose();
			_disposed = true;
		}

		/// <summary>
		/// Проверяет, был ли класс удален.
		/// </summary>
		protected void CheckDisposed()
		{
			lock (_lock)
			{
				if (_disposed)
				{
					throw new ObjectDisposedException("HeartBeatBase");
				}
			}
		}

		/// <summary>
		/// Выполняет первичный запуск.
		/// </summary>
		internal void RunRegistrationLoop()
		{
			CheckDisposed();

			if (Interlocked.CompareExchange(ref _isRegistrationLoopStarted, 1, 0) == 0) {
				var token = _cancellationTokenSource.Token;
				RegisterFirstTime(token).ContinueWith(_ => HeartBeatCycle(token), token);
			}
		}

		private async Task RegisterFirstTime(CancellationToken token)
		{
			while (!await RegisterInternal(token)) {
				await Task.Delay(_firstTimeRegistrationTimeoutMs, token);
			}
		}

		private async void HeartBeatCycle(CancellationToken token)
		{
			while (!token.IsCancellationRequested) {
				await RegisterInternal(token);
				await Task.Delay(_invokeRegistrationInterval, token);
			}
		}

		private async Task<bool> RegisterInternal(CancellationToken token)
		{
			try {
				await Beat(token);
				return true;
			} catch (Exception) {
				//TODO: что с логами?
				return false;
			}
		}

		private readonly int _invokeRegistrationInterval = 60 * 1000;//TODO: в настройки
		private readonly int _firstTimeRegistrationTimeoutMs = 1000;
		private int _isRegistrationLoopStarted;
		private bool _disposed;
		private readonly object _lock = new object();
		private readonly CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();
	}

	//TODO: брать из нугета Net.SDS.Registry.Core.Abstractions
	public class ServiceInstanceDto
	{
		public string Url { get; set; }
	}
}
