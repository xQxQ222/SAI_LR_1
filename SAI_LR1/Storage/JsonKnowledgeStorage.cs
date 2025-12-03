using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using SAI_LR1.Models;

namespace SAI_LR1.Storage
{
    public class JsonKnowledgeStorage : IKnowledgeStorage
    {
        public void Save(Node<KnowledgeItem>? root, string filename)
        {
            var options = new JsonSerializerOptions 
            { 
                WriteIndented = true,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
            };
            
            var data = SerializeNode(root);
            var json = JsonSerializer.Serialize(data, options);
            File.WriteAllText(filename, json);
        }

        public Node<KnowledgeItem>? Load(string filename)
        {
            if (!File.Exists(filename))
                return null;

            var json = File.ReadAllText(filename);
            var data = JsonSerializer.Deserialize<NodeData>(json);
            if (data != null)
            {
                return DeserializeNode(data);
            }
            return null;
        }

        private NodeData? SerializeNode(Node<KnowledgeItem>? node)
        {
            if (node == null) return null;

            return new NodeData
            {
                Text = node.Value.Text,
                IsQuestion = node.Value.IsQuestion,
                TrueChild = SerializeNode(node.TrueChildNode),
                FalseChild = SerializeNode(node.FalseChildNode)
            };
        }

        private Node<KnowledgeItem>? DeserializeNode(NodeData? data)
        {
            if (data == null) return null;

            var node = new Node<KnowledgeItem>(new KnowledgeItem(data.Text, data.IsQuestion));
            
            if (data.TrueChild != null)
            {
                var trueChild = DeserializeNode(data.TrueChild);
                if (trueChild != null)
                {
                    node.SetChildNode(trueChild, true);
                    trueChild.SetParentNode(node);
                }
            }
            
            if (data.FalseChild != null)
            {
                var falseChild = DeserializeNode(data.FalseChild);
                if (falseChild != null)
                {
                    node.SetChildNode(falseChild, false);
                    falseChild.SetParentNode(node);
                }
            }

            return node;
        }
    }
}

