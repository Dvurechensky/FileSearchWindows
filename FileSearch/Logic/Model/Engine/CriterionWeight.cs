/*
 * Author: Nikolay Dvurechensky
 * Site: https://www.dvurechensky.pro/
 * Gmail: dvurechenskysoft@gmail.com
 * Last Updated: 12 мая 2025 06:51:53
 * Version: 1.0.7
 */

namespace FileSearch.Logic.Model.Engine
{
    public enum CriterionWeight
    {
        /// <summary>
        /// Простая проверка переменной
        /// </summary>
        None,
        /// <summary>
        /// Простая проверка переменной с помощью некоторых продвинутых алгоритмов.
        /// </summary>
        Light,
        /// <summary>
        /// Проверка переменной, которая еще не разрешена.
        /// </summary>
        Medium,
        /// <summary>
        /// Проверка очень большой переменной, которая еще не разрешена.
        /// </summary>
        Heavy,
        /// <summary>
        /// Проверка очень большой переменной со сложными вычислениями, которые лучше не использовать.
        /// </summary>
        Extreme
    }
}
