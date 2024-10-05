using FileSearch.Logic.Model.EncodingDetection;
using FileSearch.Logic.Model.Engine;
using System.Text;

namespace FileSearch.Logic.Model.CriterionSchemas
{
    internal class ContentCriterion : CriterionBase, ICriterion
    {
        private const int BufferSize = 32 * 1024; // 32KB

        private readonly string _text;
        private readonly char[][] _textInChars;
        private readonly bool _ignoreCase;
        private readonly bool _matchFullWords;
        private readonly IEncodingFactory _encodingFactory;

        public ContentCriterion(string text, bool ignoreCase, bool matchFullWords, IEncodingFactory encodingFactory)
        {
            if (text == null) throw new ArgumentNullException("text");
            if (encodingFactory == null) throw new ArgumentNullException("encodingFactory");

            _text = text;
            _ignoreCase = ignoreCase;
            _matchFullWords = matchFullWords;
            _encodingFactory = encodingFactory;

            _textInChars = StringToCharArrays(text, ignoreCase);
        }

        public string Name { get { return "File content"; } }

        public CriterionWeight Weight { get { return CriterionWeight.Heavy; } }

        public bool DirectorySupport { get { return false; } }

        public bool FileSupport { get { return true; } }

        public bool IsMatch(FileSystemInfo fileSystemInfo, ICriterionContext context)
        {
            var fileInfo = (FileInfo)fileSystemInfo;

            var buffer = new byte[BufferSize];
            var textLength = _text.Length;

            Encoding[] encodings = new Encoding[1];

            // Проверить несколько кодировок
            for (int encodingIndex = 0; encodingIndex < encodings.Length; encodingIndex++)
            {
                // Кодирование текущего цикла. Первая попытка будет NULL.
                Encoding encoding = encodings[encodingIndex];
                bool characterShouldBeNonWord = false;
                char characterBefore = '\0';

                using (var stream = fileInfo.OpenRead())
                {
                    int length;
                    int foundMatchingSymbols = 0;
                    while ((length = stream.Read(buffer, 0, BufferSize)) > 0)
                    {
                        // Если кодировка еще не определена, определите ее сейчас.
                        if (encoding == null)
                        {
                            encodings = _encodingFactory.DetectEncoding(buffer);
                            encoding = encodings[encodingIndex];
                        }

                        var currentString = encoding.GetString(buffer, 0, length);
                        var currentStringLength = currentString.Length; // Кэш

                        bool startAtBegin = foundMatchingSymbols > 0;
                        var charIndex = 0;

                        // Первый символ должен быть символом, отличным от слова, если предыдущие прочитанные байты заканчиваются соответствующей строкой.
                        if (_matchFullWords && characterShouldBeNonWord && currentStringLength > 0)
                        {
                            characterShouldBeNonWord = false;
                            if (!CharIsWordChar(currentString[0]))
                                return true;
                        }

                        // Проверьте, находится ли первый или следующий совпадающий символ в текущей строке.
                        if ((charIndex = currentString.IndexOfAny(_textInChars[foundMatchingSymbols], charIndex)) >= 0)
                        {
                            do
                            {
                                // Назначьте персонажа заранее.
                                if (charIndex > 0 && foundMatchingSymbols == 0 && _matchFullWords)
                                    characterBefore = currentString[charIndex - 1];

                                // Не первый символ _text, поэтому проверьте, находится ли второй символ в первой позиции.
                                if (startAtBegin)
                                {
                                    startAtBegin = false;
                                    if (charIndex > 0) // Буква должна быть на первой позиции! Если нет, начните заново с первого символа в _text.
                                    {
                                        foundMatchingSymbols = 0;
                                        continue;
                                    }
                                }

                                // Скопируйте переменную, чтобы она не была изменена в приведенном ниже цикле.
                                var current = charIndex;
                                // Постарайтесь сопоставить как можно больше символов.
                                while (++foundMatchingSymbols < textLength && currentStringLength > ++current && (currentString[current] == _text[foundMatchingSymbols] || _ignoreCase && _textInChars[foundMatchingSymbols].Any(c => c == currentString[current]))) ;
                                // Нашел!
                                if (foundMatchingSymbols == textLength && (!_matchFullWords || !CharIsWordChar(characterBefore)))
                                {
                                    if (_matchFullWords)
                                    {
                                        // Попытайтесь определить следующее чтение, заканчивается ли строка символом, отличным от слова.
                                        if (current >= currentStringLength - 1)
                                            characterShouldBeNonWord = true;
                                        // Проверьте, не является ли следующая буква словом char. Если нет, верните true. В противном случае убедитесь, что индекс равен +1, чтобы он был сброшен в следующем операторе IF.
                                        else if (!CharIsWordChar(currentString[++current]))
                                            return true;
                                    }
                                    else
                                        // Возвращает true, если не проверяется слово вместо части строки.
                                        return true;
                                }
                                // Сбросить счетчик совпадающих символов, если конец текущей строки не достигнут. Если да, продолжайте тестирование при следующем чтении.
                                if (currentStringLength != current)
                                    foundMatchingSymbols = 0;
                                // Проверьте, есть ли следующий соответствующий символ в текущей строке.
                            } while ((charIndex = currentString.IndexOfAny(_textInChars[foundMatchingSymbols], ++charIndex)) >= 0);

                            // Назначьте последний символ, чтобы в следующем раунде он знал предыдущий символ.
                            if (foundMatchingSymbols == 0 && _matchFullWords)
                                characterBefore = currentString[currentStringLength - 1];
                        }
                        else
                        {
                            foundMatchingSymbols = 0;
                            // Переназначьте предыдущий символ последнему символу в строке.
                            if (_matchFullWords) characterBefore = currentString[currentStringLength - 1];
                        }
                    }
                    if (_matchFullWords && characterShouldBeNonWord)
                        return true;
                }
            }
            return false;
        }

        private static bool CharIsWordChar(char c)
        {
            return char.IsLetterOrDigit(c) || c == '_';
        }

        private static char[][] StringToCharArrays(string input, bool ignoreCase)
        {
            var list = new List<char[]>();
            foreach (var c in input)
            {
                if (!ignoreCase || !char.IsLetter(c))
                    list.Add(new[] { c });
                else
                {
                    var u = char.ToUpperInvariant(c);
                    var l = char.ToLowerInvariant(c);
                    list.Add(u != l ? new[] { u, l } : new[] { c });
                }
            }
            return list.ToArray();
        }
    }
}
