using System;
using System.Collections.Generic;
using System.Text;
using SAI_LR1.Models;

namespace SAI_LR1.Controllers
{
    public class NodeController
    {
        public static void ReplaceNode<T>(Node<T>? root, Node<T> oldNode, Node<T> newNode) where T : class
        {
            if (root == null) return;
            ReplaceNodeRecursive(root, oldNode, newNode);
        }

        private static bool ReplaceNodeRecursive<T>(Node<T>? node, Node<T> oldNode, Node<T> newNode) where T : class
        {
            if (node == null) return false;

            if (node.TrueChildNode == oldNode)
            {
                node.SetChildNode(newNode, true);
                newNode.SetParentNode(node);
                return true;
            }

            if (node.FalseChildNode == oldNode)
            {
                node.SetChildNode(newNode, false);
                newNode.SetParentNode(node);
                return true;
            }

            return ReplaceNodeRecursive(node.TrueChildNode, oldNode, newNode) ||
                   ReplaceNodeRecursive(node.FalseChildNode, oldNode, newNode);
        }

        public static void GetAllNodesRecursive<T>(Node<T>? node, StringBuilder sb, string indent, Func<T, string> toString) where T : class
        {
            if (node == null) return;

            string text = toString(node.Value);
            
            bool isQuestion = false;
            if (node.Value is KnowledgeItem item)
            {
                isQuestion = item.IsQuestion;
            }

            if (isQuestion)
            {
                sb.AppendLine($"{indent}{text}?");
                
                if (node.FalseChildNode != null)
                {
                    sb.Append($"{indent}  Нет -> ");
                    string falseIndent = indent + "        ";
                    GetAllNodesRecursive(node.FalseChildNode, sb, falseIndent, toString);
                }
                
                if (node.TrueChildNode != null)
                {
                    sb.Append($"{indent}  Да -> ");
                    string trueIndent = indent + "       ";
                    GetAllNodesRecursive(node.TrueChildNode, sb, trueIndent, toString);
                }
            }
            else
            {
                sb.AppendLine($"{indent}{text}");
            }
        }

        public static Node<T>? FindNodeByValue<T>(Node<T>? root, Func<T, bool> predicate, List<string> path, List<string> currentPath) where T : class
        {
            if (root == null) return null;

            if (root.IsLeaf && predicate(root.Value))
            {
                path.AddRange(currentPath);
                return root;
            }

            if (!root.IsLeaf)
            {
                currentPath.Add("Да");
                var result = FindNodeByValue(root.TrueChildNode, predicate, path, currentPath);
                if (result != null) return result;
                currentPath.RemoveAt(currentPath.Count - 1);

                currentPath.Add("Нет");
                result = FindNodeByValue(root.FalseChildNode, predicate, path, currentPath);
                if (result != null) return result;
                currentPath.RemoveAt(currentPath.Count - 1);
            }

            return null;
        }
    }
}
