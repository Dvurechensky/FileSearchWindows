/*
 * Author: Nikolay Dvurechensky
 * Site: https://www.dvurechensky.pro/
 * Gmail: dvurechenskysoft@gmail.com
 * Last Updated: 12 мая 2025 06:51:53
 * Version: 1.0.7
 */

namespace FileSearch.Logic.Model.Engine
{
    internal class TimedCallback<T>
    {
        private readonly TimeSpan _timeout;
        private readonly Action<IEnumerable<T>> _callback;
        private DateTime _lastTrigger;
        private bool _isRunning = false;

        public TimedCallback(TimeSpan timeout, Action<IEnumerable<T>> callback)
        {
            if (callback == null) throw new ArgumentNullException("callback");
            if (timeout.TotalSeconds < 0.1) throw new ArgumentException(@"The timeout should be minimal 0.1 second.", "timeout");

            _timeout = timeout;
            _callback = callback;
            _lastTrigger = DateTime.UtcNow;
        }

        /// <summary>
        /// Значение, указывающее, превышен ли период тайм-аута и можно ли получить новые данные.
        /// </summary>
        public bool DataNeeded
        {
            get { return !_isRunning && DateTime.UtcNow - _lastTrigger >= _timeout; }
        }

        /// <summary>
        /// Устанавливает данные для отправки делегату обратного вызова.
        /// </summary>
        /// <param name="collection">Сбор с данными.</param>
        public void SetData(IEnumerable<T> collection)
        {
            try
            {
                _isRunning = true;
                _callback(collection);
                _lastTrigger = DateTime.UtcNow;
            }
            finally
            {
                _isRunning = false;
            }
        }
    }
}
