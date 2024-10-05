using FileSearch.Logic;
using FileSearch.Logic.Model.Engine;
using FileSearch.Logic.Model.Tree;
using FileSearch.Logic.Plugin;
using FileSearch.Logic.UI.Entries;
using FileSearch.Logic.UI.ViewBuilders;
using System.Diagnostics;
using System.Globalization;

namespace FileSearch
{
    internal partial class MainForm : Form
    {
        private ExceptionsForm _exceptionsForm;

        private event EventHandler<PathEventArgs> FileOpened;
        private event EventHandler<PathEventArgs> DirectoryOpened;

        private static FileSearcher _fileSearcher;
        private bool _resultsViewIsUpdated = false;

        private static CancellationTokenSource _tokenGetFilesSourceSearch = new CancellationTokenSource();

        private string _searchPath = string.Empty;

        public MainForm() => InitializeComponent();

        #region Logic

        /// <summary>
        /// Подготовка к запуску сеанса поиска
        /// </summary>
        private DirectoryInfo SearchInit()
        {
            toolStripProgressBar1.Style = ProgressBarStyle.Marquee;
            lstResults.ClearContent();
            _resultsViewIsUpdated = false;
            DirsTreeView.Controls.Clear();
            DirsTreeView.Nodes.Clear();
            _tokenGetFilesSourceSearch.Cancel();

            SaveInfo(comboBox: true);

            SearchBtn.Text = @"Остановить";
            statusLabel.Text = @"Поиск... Пожалуйста подождите.";
            statusLabelExceptions.Text = null;
            return new DirectoryInfo(DirPathTxt.Text.Trim('\\') + "\\");
        }

        /// <summary>
        /// Обработка события обнаружения данных
        /// </summary>
        /// <param name="searchResults">searchResults</param>
        private void LoadList(IEnumerable<SearchResult> searchResults)
        {
            if (lstResults.InvokeRequired)
            {
                lstResults.BeginInvoke((Action<IEnumerable<SearchResult>>)LoadList, searchResults);
                return;
            }

            if (!_resultsViewIsUpdated)
            {
                lstResults.SetViewBuilder(ViewBuilderFactory.Create());
                _resultsViewIsUpdated = true;
            }

            lstResults.AddSearchResults(searchResults);
            AddTimerTextInfo(_fileSearcher.CurrentTime.GetFriendlyNotation());  // обновляем таймер
            AddFindFilesCount(lstResults.Count);                                // Обновляем число обнаруженных файлов
            AddThreeViewData(searchResults);                                    // Обновляем дерево директорий
        }

        /// <summary>
        /// Обработка окончания сеанса поиска
        /// </summary>
        private void LoadListFinished()
        {
            if (lstResults.InvokeRequired)
            {
                lstResults.Invoke((Action)LoadListFinished);
                return;
            }

            var exceptions = _fileSearcher.Exceptions;
            if (exceptions.Count > 0)
                statusLabelExceptions.Text = string.Format(CultureInfo.InvariantCulture, "{0} ошибок в поиске", exceptions.Count);

            SearchBtn.Text = @"Поиск";
            PauseBtn.Text = @"Пауза";
            SearchBtn.Enabled = true;

            statusLabel.Text = @"Завершено за " + _fileSearcher.OperatingTime.GetFriendlyNotation() + @". Найдено: " + lstResults.Count;
            toolStripProgressBar1.Style = ProgressBarStyle.Blocks;
            if (DirsTreeView.Nodes.Count > 0)
                DirsTreeView.Focus();
        }

        /// <summary>
        /// Конфигурирование критерий поиска
        /// 
        /// TODO: В будущем можно сделать тонкую настройку 
        /// критерий поиска в UI указав Control для включения тех или иных
        /// </summary>
        /// <param name="directoryInfo">directoryInfo</param>
        /// <param name="text">text</param>
        /// <returns></returns>
        private EngineOptions BuildOptions(DirectoryInfo directoryInfo, string text)
        {
            var options = new EngineOptions(new[] { directoryInfo })
            {
                SearchName = text,
                SearchIncludesFolders = true,
                SearchNameIgnoreCasing = false,
                SearchNameMatchFullPath = false,
                SearchRecursive = true,
                SearchNameAsRegularExpression = true,
                SearchInArchives = false,
                ContentAsRegularExpression = true,
                ContentText = string.Empty,
                ContentIgnoreCasing = true,
                ContentWholeWordsOnly = false,
                ContentEncodingFactory = null,
                ContentForOfficeXml = false
            };

            return options;
        }

        /// <summary>
        /// Построение дерева директорий
        /// </summary>
        /// <param name="searchResults">IEnumerable</param>
        private void AddThreeViewData(IEnumerable<SearchResult> searchResults)
        {
            TreeViewBuilder.BuildTreeView(DirsTreeView, searchResults.Select(res => res.FileSystemInfo.FullName).ToArray());
        }

        /// <summary>
        /// Инициализация чтения максимального числа файлов в директории
        /// </summary>
        private void LoadingMaxFiles()
        {
            if (_searchPath == DirPathTxt.Text) return;
            _searchPath = DirPathTxt.Text;

            var task = Task.Run(async () =>
            {
                AddProgressBarLoadMax(true);
                await GetAllFilesExecute(_searchPath);
            });

        }

        /// <summary>
        /// Формирование async задачи на чтение максимального числа файлов в директории
        /// </summary>
        /// <param name="searchPath">path</param>
        public async Task GetAllFilesExecute(string searchPath)
        {
            AddMaxFilesTxtInfo("0");
            await Task.Factory.StartNew(() =>
            {
                GetAllFiles(searchPath);
            });
        }

        /// <summary>
        /// Получение максимального числа файлов в директории (с вложенными внутри)
        /// </summary>
        /// <param name="searchPath">path</param>
        private void GetAllFiles(string searchPath)
        {
            long count = 0;

            _tokenGetFilesSourceSearch = new CancellationTokenSource();

            var searchTask = Task.Run(async () =>
            {
                try
                {
                    count = await GetFileCountAsync(searchPath, _tokenGetFilesSourceSearch.Token);
                }
                catch (OperationCanceledException)
                {
                    // Обработка отмены операции
                    AddMaxFilesTxtInfo(count.ToString());
                    AddProgressBarLoadMax(false);
                }
            }, _tokenGetFilesSourceSearch.Token);

            searchTask.ContinueWith(t =>
            {
                searchTask = null;
                AddMaxFilesTxtInfo(count.ToString());
                AddProgressBarLoadMax(false);
            });
        }

        public async Task<long> GetFileCountAsync(string directory, CancellationToken cancellationToken)
        {
            try
            {
                long count = 0;

                foreach (var file in Directory.GetFiles(directory))
                {
                    if (cancellationToken.IsCancellationRequested)
                    {
                        cancellationToken.ThrowIfCancellationRequested();
                    }

                    count++;
                }

                foreach (var subDir in Directory.GetDirectories(directory))
                {
                    if (cancellationToken.IsCancellationRequested)
                    {
                        cancellationToken.ThrowIfCancellationRequested();
                    }

                    count += await GetFileCountAsync(subDir, cancellationToken);
                }

                return count;
            }
            catch (OperationCanceledException)
            {
                throw; // Пробрасываем исключение дальше для обработки в вызывающем коде
            }
            catch (UnauthorizedAccessException)
            {
                return 0; // Пропускаем каталоги, к которым нет доступа
            }
            catch (DirectoryNotFoundException)
            {
                return 0; // Пропускаем отсутствующие каталоги
            }
        }

        /// <summary>
        /// Сохранение полей (простейшее)
        /// </summary>
        /// <param name="comboBox">сохранение выбранного пути в comboBox в текущей сессии</param>
        private void SaveInfo(bool comboBox = false)
        {
            File.WriteAllText(@"DirInfo.dat", DirPathTxt.Text);
            File.WriteAllText(@"FileRegex.dat", FileRegexPathTxt.Text);

            //сохранение выбранного пути для поиска в текущей сессии
            if (comboBox)
            {
                var content = DirPathTxt.SelectedItem != null ? (string)DirPathTxt.SelectedItem : DirPathTxt.Text;
                if (!string.IsNullOrEmpty(content))
                {
                    if (DirPathTxt.Items.Contains(content))
                        DirPathTxt.Items.Remove(content);
                    DirPathTxt.Items.Insert(0, content);
                    DirPathTxt.SelectedItem = 0;
                    DirPathTxt.Text = content;
                }
            }
        }

        #endregion

        #region Events UI

        private void MainForm_Load(object sender, EventArgs e)
        {
            if (File.Exists(@"DirInfo.dat"))
                DirPathTxt.Text = File.ReadAllText(@"DirInfo.dat");
            if (File.Exists(@"FileRegex.dat"))
                FileRegexPathTxt.Text = File.ReadAllText(@"FileRegex.dat");
        }


        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            SaveInfo();
            _tokenGetFilesSourceSearch.Cancel();
        }

        private void MainForm_DirectoryOpened(object? sender, PathEventArgs e)
        {
            Process.Start(new ProcessStartInfo("explorer.exe", "\"" + e.Entry.FileSystemInfo.FullName + "\""));
        }

        private void MainForm_FileOpened(object? sender, PathEventArgs e)
        {
            var file = e.Entry.FileSystemInfo.FullName;

            Process.Start(new ProcessStartInfo("explorer.exe", file));
        }

        private void SearchBtn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(DirPathTxt.Text)) return;

            if (_fileSearcher != null && _fileSearcher.IsRunning)
            {
                _fileSearcher.Stop();
                SearchBtn.Enabled = false;
                return;
            }

            var dirInfo = SearchInit();
            if (!dirInfo.Exists)
            {
                MessageBox.Show(@"Путь не существует.", @"Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            LoadingMaxFiles();
            _fileSearcher = new FileSearcher(BuildOptions(dirInfo, FileRegexPathTxt.Text), Plugins.All().Select(f => f.GetCriterion()).Where(f => f != null).SelectMany(f => f));
            _fileSearcher.Start(LoadList, LoadListFinished);
        }

        private void PauseBtn_Click(object sender, EventArgs e)
        {
            _fileSearcher?.Pause((state) =>
            {
                PauseBtn.Text = (state) ? "Далее" : "Пауза";
            }, (PauseBtn.Text == "Далее") ? false : true);
        }

        private void DirPathTxt_TextChanged(object sender, EventArgs e)
        {
            DirPathTxt.ForeColor = Directory.Exists(DirPathTxt.Text) ? SystemColors.WindowText : Color.Red;
        }


        private void DirsTreeView_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            TreeNode node = e.Node;
            var paths = e.Node.FullPath;
            DirectoryInfo dir = new DirectoryInfo(paths);
            FileSystemInfo fsi = dir as FileSystemInfo;

            var path = new PathEntry(fsi);

            if (path.IsDirectory)
            {
                if (DirectoryOpened != null)
                    DirectoryOpened(this, new PathEventArgs(path));
            }
            else if (FileOpened != null)
                FileOpened(this, new PathEventArgs(path));

        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            if (_fileSearcher != null && _fileSearcher.IsRunning)
            {
                MessageBox.Show("Поиск ещё идёт!");
                return;
            }

            folderBrowserDialog.SelectedPath = DirPathTxt.Text;
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                // Set the selected index to -1 so when the text is set the index is forgotten.
                DirPathTxt.SelectedIndex = -1;
                DirPathTxt.Text = folderBrowserDialog.SelectedPath;
            }
        }

        private void statusLabelExceptions_Click(object sender, EventArgs e)
        {
            if (_fileSearcher == null || _fileSearcher.Exceptions.Count <= 0)
                return;

            var form = _exceptionsForm ?? (_exceptionsForm = new ExceptionsForm());
            form.SetContent(_fileSearcher.Exceptions);
            form.ShowDialog();
        }

        #endregion

        #region Async UI

        /// <summary>
        /// Обновление AllFilesTxt
        /// </summary>
        /// <param name="text">count</param>
        private void AddMaxFilesTxtInfo(string text)
        {
            if (string.IsNullOrEmpty(text)) { return; }
            if (InvokeRequired)
            {
                Invoke((Action<string>)AddMaxFilesTxtInfo, text);
                return;
            }
            AllFilesTxt.Text = text;
            AllFilesTxt.Update();
        }

        /// <summary>
        /// Обновление pictureBoxLoading
        /// </summary>
        /// <param name="visible">bool</param>
        private void AddProgressBarLoadMax(bool visible)
        {
            if (InvokeRequired)
            {
                Invoke((Action<bool>)AddProgressBarLoadMax, visible);
                return;
            }
            pictureBoxLoading.Visible = visible;
            pictureBoxLoading.Update();
            pictureBoxLoading.Refresh();
        }

        /// <summary>
        /// Обновление timerTxt
        /// </summary>
        /// <param name="text">count</param>
        private void AddTimerTextInfo(string text)
        {
            if (string.IsNullOrEmpty(text)) { return; }
            if (InvokeRequired)
            {
                Invoke((Action<string>)AddTimerTextInfo, text);
                return;
            }
            timerTxt.Text = text;
            timerTxt.Update();
        }

        /// <summary>
        /// Обновление FindFilesTxt
        /// </summary>
        /// <param name="count">count</param>
        private void AddFindFilesCount(int count)
        {
            if (InvokeRequired)
            {
                Invoke((Action<int>)AddFindFilesCount, count);
                return;
            }

            FindFilesTxt.Text = count.ToString();
            FindFilesTxt.Update();
        }

        #endregion
    }
}