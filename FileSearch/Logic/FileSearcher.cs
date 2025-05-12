/*
 * Author: Nikolay Dvurechensky
 * Site: https://www.dvurechensky.pro/
 * Gmail: dvurechenskysoft@gmail.com
 * Last Updated: 12 мая 2025 06:51:53
 * Version: 1.0.7
 */

using FileSearch.Logic.Model.CriterionSchemas;
using FileSearch.Logic.Model.Engine;
using System.Collections.ObjectModel;

namespace FileSearch.Logic
{
    internal class FileSearcher
    {
        private readonly IList<ICriterion> _additionalCriterion;
        private readonly EngineOptions _engineOptions;
        private readonly IList<SearchException> _expections;
        private bool _stop;
        private bool _pause;
        private Task<IList<SearchResult>> _currentTask;
        private DateTime _startTime;

        public FileSearcher(EngineOptions engineOptions, IEnumerable<ICriterion> additionalCriterion)
        {
            if (engineOptions == null) throw new ArgumentNullException("engineOptions");

            _engineOptions = engineOptions;
            _additionalCriterion = additionalCriterion.ToList();
            this.RefreshTimer = TimeSpan.FromSeconds(1);
            _expections = new List<SearchException>();
        }

        /// <summary>
        /// Получает или задает интервал времени для тайм-аута обратного вызова сопоставления.
        /// </summary>
        public TimeSpan RefreshTimer { get; set; }

        /// <summary>
        /// Получает время, в течение которого поисковая система работала над последней операцией.
        /// </summary>
        public TimeSpan OperatingTime { get; private set; }

        /// <summary>
        /// Получает время, в течение которого поисковая система работает.
        /// </summary>
        public TimeSpan CurrentTime { get; private set; }

        /// <summary>
        /// Получает список всех исключений поиска последней операции.
        /// </summary>
        public IList<SearchException> Exceptions { get { return new ReadOnlyCollection<SearchException>(_expections); } }

        /// <summary>
        /// Значение, указывающее, работает ли поисковая система.
        /// </summary>
        public bool IsRunning { get { return _currentTask != null; } }

        /// <summary>
        /// Получает список всех критериев, которые использовались в текущей или последней операции.
        /// </summary>
        public IList<ICriterion> UsedCriteria { get; private set; }

        /// <summary>
        /// Запускает операцию поиска.
        /// </summary>
        /// <param name="matchCallback">Обратный вызов при обнаружении совпадений.</param>
        /// <param name="finishCallback">Обратный вызов после завершения поиска.</param>
        public void Start(Action<IEnumerable<SearchResult>> matchCallback, Action finishCallback)
        {
            this.OperatingTime = new TimeSpan();
            _startTime = DateTime.UtcNow;
            _expections.Clear();

            var timeout = new TimedCallback<SearchResult>(this.RefreshTimer, matchCallback);
            _stop = false;

            _currentTask = Task.Factory.StartNew<IList<SearchResult>>(() => Search(timeout));

            _currentTask.ContinueWith(t => {
                timeout.SetData(t.Result);
            })
            .ContinueWith(t => {
                _currentTask = null;
                OperatingTime = DateTime.UtcNow - _startTime;
                finishCallback();
            });
        }

        /// <summary>
        /// Прерывает текущую операцию поиска.
        /// </summary>
        public void Stop()
        {
            if (IsRunning)
            {
                _pause = false;
                _stop = true;
            }
        }

        /// <summary>
        /// Приостанавливает текущую операцию поиска.
        /// </summary>
        public void Pause(Action<bool> state, bool update)
        {
            if (IsRunning)
                _pause = update;
            state(_pause);
        }

        private IList<ICriterion> BuildCriteria()
        {
            // Разрешить только один IPostProcessingCriterion. В противном случае результаты будут странными.
            var criteria = CriteriaFactory.Build(_engineOptions).Union(_additionalCriterion).OrderBy(c => c is IPostProcessingCriterion).ThenBy(c => c.Weight).ToList();
            UsedCriteria = new ReadOnlyCollection<ICriterion>(criteria);
            return criteria;
        }

        private IList<SearchResult> Search(object state)
        {
            var timer = (TimedCallback<SearchResult>)state;

            var criteria = BuildCriteria();
            var list = new List<SearchResult>(64);
            var requiresPostProcessing = criteria.Any(c => c is IPostProcessingCriterion);

            foreach (var rootDirectory in _engineOptions.RootDirectories)
            {

                foreach (var fileSystemInfo in ListAllFileSystemInfo(rootDirectory, -1))
                {
                    var contexts = new Dictionary<Type, ICriterionContext>();
                    var isDir = fileSystemInfo is DirectoryInfo;
                    var match = true;
                    this.CurrentTime = DateTime.UtcNow - _startTime;

                    //Приостановить цикл
                    while (_pause)
                    {
                        Console.WriteLine("");
                    }

                    try
                    {
                        foreach (var c in criteria)
                        {
                            var context = c.BuildContext();

                            // Проверьте, поддерживает ли критерий тип целевой файловой системы.
                            if ((c.DirectorySupport && isDir) || (c.FileSupport && !isDir))
                            {
                                if (c.IsMatch(fileSystemInfo, context))
                                {
                                    // Добавьте контекст, если он совпадает.
                                    if (context != null)
                                        contexts.Add(c.GetType(), context);
                                }
                                else
                                {
                                    match = false;
                                    break;
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        _expections.Add(SearchExceptionFactory.Build(fileSystemInfo, ex));
                        match = false;
                    }

                    if (match && !requiresPostProcessing)
                        list.Add(new SearchResult(fileSystemInfo) { Metadata = null });

                    // Есть верно, и результат не важен.
                    if (list.Count > 0 && !requiresPostProcessing && timer.DataNeeded)
                    {
                        timer.SetData(list);
                        list = new List<SearchResult>(64);
                    }

                    // Остановить цикл
                    if (_stop)
                        break;
                }
                // Остановить цикл
                if (_stop)
                    break;
            }


            if (requiresPostProcessing)
            {
                // Выбираем последний, это самый интенсивный критерий.
                var resultLists = criteria.OfType<IPostProcessingCriterion>().Single();
                return resultLists.PostProcess().ToList();
            }

            return list;
        }

        private IEnumerable<FileSystemInfo> ListAllFileSystemInfo(FileSystemInfo fileSystemInfo, int level)
        {
            var directoryInfo = fileSystemInfo as DirectoryInfo;
            var isRoot = level == -1;

            // Возвращает папку или, если это файл, всегда. Пропускает корневой уровень.
            if (!isRoot && (directoryInfo == null))
                yield return fileSystemInfo;

            if (directoryInfo != null || isRoot)
            {
                FileSystemInfo[] infos = null;
                try
                {
                    infos = directoryInfo.GetFileSystemInfos();
                }
                catch (UnauthorizedAccessException ex)
                {
                    _expections.Add(SearchExceptionFactory.Build(directoryInfo, ex));
                }
                if (infos == null)
                    yield break;

                foreach (var item in infos.SelectMany(s => ListAllFileSystemInfo(s, level + 1)))
                {
                    if (_stop) yield break;
                    yield return item;
                }
            }
        }
    }
}
