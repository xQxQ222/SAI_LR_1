namespace SAI_LR1.Models
{
    public sealed class Node<T> where T : class
    {
        public T Value { get; set; }

        public Node<T>? ParentNode { get; private set; }

        public Node<T>? FalseChildNode { get; private set; }

        public Node<T>? TrueChildNode { get; private set; }

        public bool IsLeaf => FalseChildNode == null && TrueChildNode == null;

        public Node(T value)
        {
            Value = value;
        }

        public void SetParentNode(Node<T> parent)
        {
            ParentNode = parent;
        }

        public void SetChildNode(Node<T> childNode, bool isTrueNode)
        {
            if (isTrueNode)
            {
                TrueChildNode = childNode;
            }
            else
            {
                FalseChildNode = childNode;
            }
        }
    }
}
