/*
 * Author: Nikolay Dvurechensky
 * Site: https://www.dvurechensky.pro/
 * Gmail: dvurechenskysoft@gmail.com
 * Last Updated: 12 мая 2025 06:51:53
 * Version: 1.0.7
 */

namespace FileSearch
{
    internal static class Program
    {
        /// <summary>
        /// Основная точка входа в приложение.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Чтобы настроить конфигурацию приложения, например установить настройки высокого разрешения или шрифт по умолчанию,
            // https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new MainForm());
        }
    }
}