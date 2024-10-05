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