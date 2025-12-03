using SAI_LR1.Services;

namespace SAI_LR1.UI
{
    public partial class Form1 : Form
    {
        private KnowledgeBase knowledgeBase;
        private string currentMode = "";
        private string? newToolName;
        private string? newQuestion;
        private bool awaitingStartConfirmation = false;

        public Form1()
        {
            InitializeComponent();
            knowledgeBase = new KnowledgeBase();
            
            try
            {
                knowledgeBase.LoadFromFile("knowledge_base.json");
                AddDialog("Программа: База знаний загружена.");
            }
            catch
            {
                AddDialog("Программа: Используется начальная база знаний (молоток).");
            }
            
            AddDialog("Программа: Добро пожаловать в Акинатор! Выберите режим работы.");
            UpdateButtonStates();
        }

        private void AddDialog(string text)
        {
            dialogTextBox.AppendText(text + Environment.NewLine);
            dialogTextBox.SelectionStart = dialogTextBox.Text.Length;
            dialogTextBox.ScrollToCaret();
        }

        private void UpdateButtonStates()
        {
            bool inGame = currentMode == "game" || currentMode == "learning_question";
            btnYes.Enabled = inGame;
            btnNo.Enabled = inGame;
        }

        private string? lastGameResponse = null;

        private void btnPlay_Click(object sender, EventArgs e)
        {
            currentMode = "game";
            dialogTextBox.Clear();
            AddDialog("Программа: Вы загадали инструмент?");
            awaitingStartConfirmation = true;
            lastGameResponse = null;
            UpdateButtonStates();
        }

        private void btnYes_Click(object sender, EventArgs e)
        {
            if (currentMode == "game")
            {
                AddDialog("Игрок: Да");

                if (awaitingStartConfirmation)
                {
                    string response = knowledgeBase.StartGame();
                    lastGameResponse = response;
                    AddDialog($"Программа: {response}");
                    awaitingStartConfirmation = false;
                    UpdateButtonStates();
                    return;
                }
                
                if (lastGameResponse != null && lastGameResponse.Contains("Это") && lastGameResponse.EndsWith("!"))
                {
                    AddDialog("Программа: Отлично! Я угадал!");
                    currentMode = "";
                    lastGameResponse = null;
                    UpdateButtonStates();
                    return;
                }
                
                string response2 = knowledgeBase.ProcessAnswer(true);
                lastGameResponse = response2;
                AddDialog($"Программа: {response2}");
                
                if (response2.Contains("Сдаюсь"))
                {
                    currentMode = "learning";
                    AddDialog("Программа: Подскажите правильный ответ.");
                    UpdateButtonStates();
                }
            }
            else if (currentMode == "learning_question")
            {
                AddDialog("Игрок: Да");
                knowledgeBase.LearnNewItem(newToolName!, newQuestion!, true);
                AddDialog("Программа: Спасибо!");
                currentMode = "";
                UpdateButtonStates();
            }
        }

        private void btnNo_Click(object sender, EventArgs e)
        {
            if (currentMode == "game")
            {
                AddDialog("Игрок: Нет");

                if (awaitingStartConfirmation)
                {
                    AddDialog("Программа: Хорошо. Возвращаюсь в главное меню.");
                    currentMode = "";
                    awaitingStartConfirmation = false;
                    UpdateButtonStates();
                    return;
                }
                
                if (lastGameResponse != null && lastGameResponse.Contains("Это") && lastGameResponse.EndsWith("!"))
                {
                    AddDialog("Программа: Сдаюсь.");
                    currentMode = "learning";
                    AddDialog("Программа: Подскажите правильный ответ.");
                    lastGameResponse = null;
                    UpdateButtonStates();
                    return;
                }
                
                string response = knowledgeBase.ProcessAnswer(false);
                lastGameResponse = response;
                AddDialog($"Программа: {response}");
                
                if (response.Contains("Сдаюсь"))
                {
                    currentMode = "learning";
                    AddDialog("Программа: Подскажите правильный ответ.");
                    UpdateButtonStates();
                }
            }
            else if (currentMode == "learning_question")
            {
                AddDialog("Игрок: Нет");
                knowledgeBase.LearnNewItem(newToolName!, newQuestion!, false);
                AddDialog("Программа: Спасибо!");
                currentMode = "";
                UpdateButtonStates();
            }
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            string input = userInputTextBox.Text.Trim();
            if (string.IsNullOrEmpty(input))
                return;

            AddDialog($"Игрок: {input}");
            userInputTextBox.Clear();

            if (currentMode == "learning")
            {
                newToolName = input;
                string currentAnswer = knowledgeBase.GetCurrentAnswer();
                AddDialog($"Программа: Сформулируйте вопрос, ответ на который поможет отличить инструмент \"{newToolName}\" от инструмента \"{currentAnswer}\".");
                currentMode = "learning_question_input";
            }
            else if (currentMode == "learning_question_input")
            {
                newQuestion = input;
                AddDialog("Программа: Подскажите вариант правильного ответа: да или нет.");
                currentMode = "learning_question";
                UpdateButtonStates();
            }
            else if (currentMode == "learning_question")
            {
                string lowerInput = input.ToLowerInvariant();
                if (lowerInput == "да" || lowerInput == "yes")
                {
                    AddDialog("Игрок: Да");
                    knowledgeBase.LearnNewItem(newToolName!, newQuestion!, true);
                    AddDialog("Программа: Спасибо!");
                    currentMode = "";
                    UpdateButtonStates();
                }
                else if (lowerInput == "нет" || lowerInput == "no")
                {
                    AddDialog("Игрок: Нет");
                    knowledgeBase.LearnNewItem(newToolName!, newQuestion!, false);
                    AddDialog("Программа: Спасибо!");
                    currentMode = "";
                    UpdateButtonStates();
                }
                else
                {
                    AddDialog("Программа: Пожалуйста, введите 'да' или 'нет'.");
                }
            }
            else if (currentMode == "show_tool")
            {
                string info = knowledgeBase.GetItemInfo(input);
                AddDialog($"Программа: {info}");
                currentMode = "";
            }
            else if (currentMode == "has_tool")
            {
                bool has = knowledgeBase.HasItem(input);
                AddDialog($"Программа: {(has ? "Да, есть в базе знаний." : "Нет, такого инструмента нет в базе знаний.")}");
                currentMode = "";
            }
        }

        private void btnWhy_Click(object sender, EventArgs e)
        {
            dialogTextBox.Clear();
            AddDialog("Игрок: Почему такой ответ?");
            string reasoning = knowledgeBase.GetReasoning();
            AddDialog($"Программа: {reasoning}");
        }

        private void btnShowAll_Click(object sender, EventArgs e)
        {
            dialogTextBox.Clear();
            AddDialog("Игрок: Хочу видеть всю базу знаний.");
            string allKnowledge = knowledgeBase.GetAllKnowledge();
            AddDialog($"Программа: {allKnowledge}");
        }

        private void btnShowTool_Click(object sender, EventArgs e)
        {
            currentMode = "show_tool";
            dialogTextBox.Clear();
            AddDialog("Программа: Введите название инструмента:");
        }

        private void btnHasTool_Click(object sender, EventArgs e)
        {
            currentMode = "has_tool";
            dialogTextBox.Clear();
            AddDialog("Программа: Введите название инструмента для проверки:");
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                knowledgeBase.SaveToFile("knowledge_base.json");
                AddDialog("Программа: База знаний сохранена в файл knowledge_base.json");
            }
            catch (Exception ex)
            {
                AddDialog($"Программа: Ошибка при сохранении: {ex.Message}");
            }
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            try
            {
                knowledgeBase.LoadFromFile("knowledge_base.json");
                AddDialog("Программа: База знаний загружена из файла knowledge_base.json");
            }
            catch (Exception ex)
            {
                AddDialog($"Программа: Ошибка при загрузке: {ex.Message}");
            }
        }

        private void userInputTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnSend_Click(sender, e);
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }
    }
}
