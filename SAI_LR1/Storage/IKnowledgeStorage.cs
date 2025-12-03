using SAI_LR1.Models;

namespace SAI_LR1.Storage
{
    public interface IKnowledgeStorage
    {
        void Save(Node<KnowledgeItem>? root, string filename);
        Node<KnowledgeItem>? Load(string filename);
    }
}

