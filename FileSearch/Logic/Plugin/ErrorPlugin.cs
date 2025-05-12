/*
 * Author: Nikolay Dvurechensky
 * Site: https://www.dvurechensky.pro/
 * Gmail: dvurechenskysoft@gmail.com
 * Last Updated: 12 мая 2025 06:51:53
 * Version: 1.0.7
 */

namespace FileSearch.Logic.Plugin
{
    internal class ErrorPlugin : IPluginFacade
    {
        private readonly string _errorMessage;

        public ErrorPlugin(string tabTitle, string errorMessage = null)
        {
            _errorMessage = errorMessage;
            this.TabTitle = string.Concat(tabTitle, " [ERROR]");
        }

        public Guid PluginId { get { return Guid.Empty; } }

        public string TabTitle { get; private set; }

        public UserControl BuildTabPage()
        {
            var uc = new UserControl();
            if (!string.IsNullOrEmpty(_errorMessage))
            {
                var label = new Label
                {
                    Text = string.Concat("PLUGIN ERROR: ", _errorMessage),
                    Location = new Point(3, 3),
                    ForeColor = Color.Red,
                    Font = new Font(SystemFonts.DefaultFont, FontStyle.Italic),
                    AutoSize = true
                };
                uc.Controls.Add(label);
            }

            return uc;
        }

        public ICriterionPlugin[] GetCriterion()
        {
            return new ICriterionPlugin[0];
        }

        public IViewBuilderFactory GetViewBuilderFactory()
        {
            return null;
        }
    }
}
