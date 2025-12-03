namespace SAI_LR1.Models
{
    public class NodeData
    {
        public string Text { get; set; } = "";
        public bool IsQuestion { get; set; }
        public NodeData? TrueChild { get; set; }
        public NodeData? FalseChild { get; set; }
    }
}

