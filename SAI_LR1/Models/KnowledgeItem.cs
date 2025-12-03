namespace SAI_LR1.Models
{
    public class KnowledgeItem
    {
        public string Text { get; set; } = "";
        public bool IsQuestion { get; set; }

        public KnowledgeItem(string text, bool isQuestion)
        {
            Text = text;
            IsQuestion = isQuestion;
        }
    }
}

