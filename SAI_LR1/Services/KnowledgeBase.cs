using System;
using System.Collections.Generic;
using System.Text;
using SAI_LR1.Models;
using SAI_LR1.Controllers;
using SAI_LR1.Storage;

namespace SAI_LR1.Services
{
    public class KnowledgeBase
    {
        private Node<KnowledgeItem>? root;
        private List<string> currentPath = new();
        private Node<KnowledgeItem>? currentItem;
        private Node<KnowledgeItem>? lastAnswerItem;
        private Node<KnowledgeItem>? lastQuestionItem;
        private readonly IKnowledgeStorage storage;

        public KnowledgeBase() : this(new JsonKnowledgeStorage())
        {
        }

        public KnowledgeBase(IKnowledgeStorage storage)
        {
            this.storage = storage ?? throw new ArgumentNullException(nameof(storage));
            root = new Node<KnowledgeItem>(new KnowledgeItem("молоток", false));
        }

        public string StartGame()
        {
            currentPath.Clear();
            currentItem = root;
            lastAnswerItem = null;
            lastQuestionItem = null;
            
            if (root == null)
                return "База знаний пуста.";

            if (root.IsLeaf)
            {
                lastAnswerItem = root;
                return $"Это {root.Value.Text}!";
            }

            return AskQuestion(root);
        }

        public string ProcessAnswer(bool answer)
        {
            if (currentItem == null)
                return "Ошибка: игра не начата.";

            if (currentItem.IsLeaf)
            {
                return "Ошибка: это уже ответ, а не вопрос.";
            }

            currentPath.Add(answer ? "Да" : "Нет");

            lastQuestionItem = currentItem;

            if (answer)
            {
                currentItem = currentItem.TrueChildNode;
            }
            else
            {
                currentItem = currentItem.FalseChildNode;
            }

            if (currentItem == null)
            {
                lastAnswerItem = null;
                return "Сдаюсь.";
            }

            if (currentItem.IsLeaf)
            {
                lastAnswerItem = currentItem;
                return $"Это {currentItem.Value.Text}!";
            }

            return AskQuestion(currentItem);
        }

        private string AskQuestion(Node<KnowledgeItem> item)
        {
            if (item.Value.IsQuestion)
            {
                return $"{item.Value.Text}?";
            }
            return $"Это {item.Value.Text}!";
        }

        private Node<KnowledgeItem> CreateRootQuestion(string oldItem, string newItem, string question, bool answerForNewItem)
        {
            Node<KnowledgeItem> rootQuestion = new Node<KnowledgeItem>(new KnowledgeItem($"Это {oldItem}", true));
            
            Node<KnowledgeItem> oldItemNode = new Node<KnowledgeItem>(new KnowledgeItem(oldItem, false));
            
            Node<KnowledgeItem> newQuestion = new Node<KnowledgeItem>(new KnowledgeItem(question, true));
            Node<KnowledgeItem> newItemNode = new Node<KnowledgeItem>(new KnowledgeItem(newItem, false));
            
            rootQuestion.SetChildNode(oldItemNode, true);
            oldItemNode.SetParentNode(rootQuestion);
            
            rootQuestion.SetChildNode(newQuestion, false);
            newQuestion.SetParentNode(rootQuestion);
            
            if (answerForNewItem)
            {
                newQuestion.SetChildNode(newItemNode, true);
            }
            else
            {
                newQuestion.SetChildNode(newItemNode, false);
            }
            
            newItemNode.SetParentNode(newQuestion);
            
            return rootQuestion;
        }

        public string GetCurrentAnswer()
        {
            if (lastAnswerItem != null)
                return lastAnswerItem.Value.Text;
            
            if (currentItem != null && currentItem.IsLeaf)
                return currentItem.Value.Text;

            return "";
        }

        public void LearnNewItem(string newItem, string question, bool answerForNewItem)
        {
            Node<KnowledgeItem>? itemToReplace = lastAnswerItem;
            
            if (itemToReplace == null)
            {
                if (lastQuestionItem != null && currentPath.Count > 0)
                {
                    string lastAnswer = currentPath[currentPath.Count - 1];
                    bool wasTrueAnswer = lastAnswer == "Да";
                    
                    Node<KnowledgeItem> newQuestion = new Node<KnowledgeItem>(new KnowledgeItem(question, true));
                    Node<KnowledgeItem> newItemNode = new Node<KnowledgeItem>(new KnowledgeItem(newItem, false));
                    newItemNode.SetParentNode(newQuestion);
                    
                    string? oldItemText = null;
                    if (wasTrueAnswer && lastQuestionItem.FalseChildNode != null && lastQuestionItem.FalseChildNode.IsLeaf)
                    {
                        oldItemText = lastQuestionItem.FalseChildNode.Value.Text;
                    }
                    else if (!wasTrueAnswer && lastQuestionItem.TrueChildNode != null && lastQuestionItem.TrueChildNode.IsLeaf)
                    {
                        oldItemText = lastQuestionItem.TrueChildNode.Value.Text;
                    }
                    
                    if (oldItemText != null)
                    {
                        Node<KnowledgeItem> oldItemNode = new Node<KnowledgeItem>(new KnowledgeItem(oldItemText, false));
                        oldItemNode.SetParentNode(newQuestion);
                        
                        if (answerForNewItem)
                        {
                            newQuestion.SetChildNode(newItemNode, true);
                            newQuestion.SetChildNode(oldItemNode, false);
                        }
                        else
                        {
                            newQuestion.SetChildNode(newItemNode, false);
                            newQuestion.SetChildNode(oldItemNode, true);
                        }
                    }
                    else
                    {
                        if (answerForNewItem)
                        {
                            newQuestion.SetChildNode(newItemNode, true);
                        }
                        else
                        {
                            newQuestion.SetChildNode(newItemNode, false);
                        }
                    }
                    
                    NodeController.ReplaceNode(root, lastQuestionItem, newQuestion);
                    currentItem = newQuestion;
                    lastAnswerItem = null;
                    lastQuestionItem = null;
                    return;
                }
                
                if (currentItem != null && currentItem.IsLeaf)
                {
                    itemToReplace = currentItem;
                }
                else
                {
                    if (root != null && root.IsLeaf)
                    {
                        itemToReplace = root;
                    }
                    else
                    {
                        root = new Node<KnowledgeItem>(new KnowledgeItem(newItem, false));
                        return;
                    }
                }
            }

            if (itemToReplace == null || !itemToReplace.IsLeaf)
            {
                root = new Node<KnowledgeItem>(new KnowledgeItem(newItem, false));
                return;
            }

            string oldItem = itemToReplace.Value.Text;
            
            if (itemToReplace == root)
            {
                Node<KnowledgeItem> rootQuestion = CreateRootQuestion(oldItem, newItem, question, answerForNewItem);
                
                root = rootQuestion;
                currentItem = rootQuestion;
                lastAnswerItem = null;
            }
            else
            {
                Node<KnowledgeItem> newQuestion = new Node<KnowledgeItem>(new KnowledgeItem(question, true));
                Node<KnowledgeItem> newItemNode = new Node<KnowledgeItem>(new KnowledgeItem(newItem, false));
                Node<KnowledgeItem> oldItemNode = new Node<KnowledgeItem>(new KnowledgeItem(oldItem, false));

                if (answerForNewItem)
                {
                    newQuestion.SetChildNode(newItemNode, true);
                    newQuestion.SetChildNode(oldItemNode, false);
                }
                else
                {
                    newQuestion.SetChildNode(oldItemNode, true);
                    newQuestion.SetChildNode(newItemNode, false);
                }

                newItemNode.SetParentNode(newQuestion);
                oldItemNode.SetParentNode(newQuestion);

                NodeController.ReplaceNode(root, itemToReplace, newQuestion);
                
                currentItem = newQuestion;
                lastAnswerItem = null;
            }
        }

        public string GetReasoning()
        {
            if (root == null)
                return "База знаний пуста.";

            Node<KnowledgeItem>? targetItem = lastAnswerItem;
            
            if (targetItem == null)
            {
                if (currentItem != null && currentItem.IsLeaf)
                {
                    targetItem = currentItem;
                }
                else
                {
                    return "Игра не начата или предмет еще не угадан.";
                }
            }

            string itemName = targetItem.Value.Text;
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Путь к инструменту '{itemName}':");
            
            if (currentPath.Count == 0 && root.IsLeaf)
            {
                sb.AppendLine($"  Это {itemName}!");
                return sb.ToString();
            }
            
            Node<KnowledgeItem>? item = root;
            int pathIndex = 0;
            
            while (item != null && pathIndex < currentPath.Count)
            {
                if (item.Value.IsQuestion)
                {
                    sb.AppendLine($"  {item.Value.Text}? → {currentPath[pathIndex]}");
                    item = currentPath[pathIndex] == "Да" ? item.TrueChildNode : item.FalseChildNode;
                    pathIndex++;
                }
                else
                {
                    break;
                }
            }
            
            if (item != null && item.IsLeaf && item.Value.Text.Equals(itemName, StringComparison.OrdinalIgnoreCase))
            {
                sb.AppendLine($"  Это {item.Value.Text}!");
            }
            else if (targetItem != null)
            {
                sb.AppendLine($"  Это {targetItem.Value.Text}!");
            }
            
            return sb.ToString();
        }

        public string GetAllKnowledge()
        {
            if (root == null)
                return "База знаний пуста.";

            StringBuilder sb = new StringBuilder();
            NodeController.GetAllNodesRecursive(root, sb, "", (item) => item.Text);
            return sb.ToString();
        }

        public string GetItemInfo(string itemName)
        {
            if (root == null)
                return $"Инструмент '{itemName}' не найден в базе знаний.";

            List<string> path = new List<string>();
            var foundNode = NodeController.FindNodeByValue(root, 
                (item) => !item.IsQuestion && item.Text.Equals(itemName, StringComparison.OrdinalIgnoreCase),
                path, new List<string>());

            if (foundNode != null)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine($"Информация об инструменте '{itemName}':");
                sb.AppendLine($"Путь к инструменту:");
                
                Node<KnowledgeItem>? item = root;
                int pathIndex = 0;
                
                while (item != null && pathIndex < path.Count)
                {
                    if (item.Value.IsQuestion)
                    {
                        sb.AppendLine($"  {item.Value.Text}? → {path[pathIndex]}");
                        item = path[pathIndex] == "Да" ? item.TrueChildNode : item.FalseChildNode;
                        pathIndex++;
                    }
                    else
                    {
                        if (item.Value.Text.Equals(itemName, StringComparison.OrdinalIgnoreCase))
                        {
                            sb.AppendLine($"  Это {item.Value.Text}!");
                        }
                        break;
                    }
                }
                
                return sb.ToString();
            }

            return $"Инструмент '{itemName}' не найден в базе знаний.";
        }

        public bool HasItem(string itemName)
        {
            if (root == null) return false;
            var found = NodeController.FindNodeByValue(root,
                (item) => !item.IsQuestion && item.Text.Equals(itemName, StringComparison.OrdinalIgnoreCase),
                new List<string>(), new List<string>());
            return found != null;
        }

        public void SaveToFile(string filename)
        {
            storage.Save(root, filename);
        }

        public void LoadFromFile(string filename)
        {
            var loadedRoot = storage.Load(filename);
            if (loadedRoot != null)
            {
                root = loadedRoot;
            }
        }
    }
}
