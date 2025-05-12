/*
 * Author: Nikolay Dvurechensky
 * Site: https://www.dvurechensky.pro/
 * Gmail: dvurechenskysoft@gmail.com
 * Last Updated: 12 мая 2025 06:51:53
 * Version: 1.0.7
 */

namespace FileSearch.Logic.Model.Tree
{
    public class TreeViewBuilder
    {
        public static void BuildTreeView(TreeView treeView, string[] paths)
        {
            if(paths.Length == 0) return;

            // Проверяем, существует ли уже корневой узел
            TreeNode rootNode = treeView.Nodes.Cast<TreeNode>().FirstOrDefault();

            // Если корневой узел еще не создан, создаем его
            if (rootNode == null)
            {
                // Извлекаем первое слово из первого пути
                string rootName = GetRootName(paths[0]);
                rootNode = new TreeNode(rootName);
                treeView.Nodes.Add(rootNode);
            }
            treeView.BeginUpdate();
            // Добавляем каждый путь как узел в TreeView
            foreach (string path in paths)
            {
                AddPathToTree(rootNode, path);
            }
            treeView.EndUpdate();
        }

        private static string GetRootName(string path)
        {
            // Извлекаем первое слово из пути
            string[] parts = path.Split('\\');
            return parts[0];
        }

        private static void AddPathToTree(TreeNode parentNode, string path)
        {
            // Разбиваем путь на части
            string[] parts = path.Split('\\');

            // Проходим по каждой части пути
            TreeNode currentNode = parentNode;

            // Пропускаем первое слово, так как оно является корневым узлом
            for (int i = 1; i < parts.Length; i++)
            {
                // Проверяем, есть ли узел с текущим именем среди дочерних узлов текущего узла
                TreeNode existingNode = currentNode.Nodes.Cast<TreeNode>()
                    .FirstOrDefault(node => node.Text == parts[i]);

                // Если узел с таким именем уже существует, переходим к следующему уровню
                if (existingNode != null)
                {
                    currentNode = existingNode;
                }

                // Если узел не существует, создаем новый
                else
                {
                    TreeNode newNode = new TreeNode(parts[i]);
                    currentNode.Nodes.Add(newNode);
                    currentNode = newNode;
                }
            }
        }
    }
}
