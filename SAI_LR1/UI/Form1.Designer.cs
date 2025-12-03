namespace SAI_LR1.UI
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.dialogTextBox = new System.Windows.Forms.TextBox();
            this.userInputTextBox = new System.Windows.Forms.TextBox();
            this.btnYes = new System.Windows.Forms.Button();
            this.btnNo = new System.Windows.Forms.Button();
            this.btnSend = new System.Windows.Forms.Button();
            this.groupBoxGame = new System.Windows.Forms.GroupBox();
            this.btnPlay = new System.Windows.Forms.Button();
            this.groupBoxAnalysis = new System.Windows.Forms.GroupBox();
            this.btnWhy = new System.Windows.Forms.Button();
            this.btnShowAll = new System.Windows.Forms.Button();
            this.btnShowTool = new System.Windows.Forms.Button();
            this.btnHasTool = new System.Windows.Forms.Button();
            this.groupBoxManagement = new System.Windows.Forms.GroupBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnLoad = new System.Windows.Forms.Button();
            this.groupBoxGame.SuspendLayout();
            this.groupBoxAnalysis.SuspendLayout();
            this.groupBoxManagement.SuspendLayout();
            this.SuspendLayout();
            this.dialogTextBox.Location = new System.Drawing.Point(12, 12);
            this.dialogTextBox.Multiline = true;
            this.dialogTextBox.Name = "dialogTextBox";
            this.dialogTextBox.ReadOnly = true;
            this.dialogTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dialogTextBox.Size = new System.Drawing.Size(776, 300);
            this.dialogTextBox.TabIndex = 0;
            this.userInputTextBox.Location = new System.Drawing.Point(12, 318);
            this.userInputTextBox.Name = "userInputTextBox";
            this.userInputTextBox.Size = new System.Drawing.Size(400, 27);
            this.userInputTextBox.TabIndex = 1;
            this.userInputTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.userInputTextBox_KeyDown);
            this.btnYes.Enabled = false;
            this.btnYes.Location = new System.Drawing.Point(418, 318);
            this.btnYes.Name = "btnYes";
            this.btnYes.Size = new System.Drawing.Size(75, 27);
            this.btnYes.TabIndex = 2;
            this.btnYes.Text = "Да";
            this.btnYes.UseVisualStyleBackColor = true;
            this.btnYes.Click += new System.EventHandler(this.btnYes_Click);
            this.btnNo.Enabled = false;
            this.btnNo.Location = new System.Drawing.Point(499, 318);
            this.btnNo.Name = "btnNo";
            this.btnNo.Size = new System.Drawing.Size(75, 27);
            this.btnNo.TabIndex = 3;
            this.btnNo.Text = "Нет";
            this.btnNo.UseVisualStyleBackColor = true;
            this.btnNo.Click += new System.EventHandler(this.btnNo_Click);
            this.btnSend.Location = new System.Drawing.Point(580, 318);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(100, 27);
            this.btnSend.TabIndex = 4;
            this.btnSend.Text = "Отправить";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            this.groupBoxGame.Controls.Add(this.btnPlay);
            this.groupBoxGame.Location = new System.Drawing.Point(12, 360);
            this.groupBoxGame.Name = "groupBoxGame";
            this.groupBoxGame.Size = new System.Drawing.Size(150, 60);
            this.groupBoxGame.TabIndex = 5;
            this.groupBoxGame.TabStop = false;
            this.groupBoxGame.Text = "Игра";
            this.btnPlay.Location = new System.Drawing.Point(6, 22);
            this.btnPlay.Name = "btnPlay";
            this.btnPlay.Size = new System.Drawing.Size(138, 30);
            this.btnPlay.TabIndex = 0;
            this.btnPlay.Text = "Играть";
            this.btnPlay.UseVisualStyleBackColor = true;
            this.btnPlay.Click += new System.EventHandler(this.btnPlay_Click);
            this.groupBoxAnalysis.Controls.Add(this.btnWhy);
            this.groupBoxAnalysis.Controls.Add(this.btnShowAll);
            this.groupBoxAnalysis.Controls.Add(this.btnShowTool);
            this.groupBoxAnalysis.Controls.Add(this.btnHasTool);
            this.groupBoxAnalysis.Location = new System.Drawing.Point(168, 360);
            this.groupBoxAnalysis.Name = "groupBoxAnalysis";
            this.groupBoxAnalysis.Size = new System.Drawing.Size(240, 120);
            this.groupBoxAnalysis.TabIndex = 7;
            this.groupBoxAnalysis.TabStop = false;
            this.groupBoxAnalysis.Text = "Анализ базы знаний";
            this.btnWhy.Location = new System.Drawing.Point(6, 22);
            this.btnWhy.Name = "btnWhy";
            this.btnWhy.Size = new System.Drawing.Size(110, 30);
            this.btnWhy.TabIndex = 0;
            this.btnWhy.Text = "Почему?";
            this.btnWhy.UseVisualStyleBackColor = true;
            this.btnWhy.Click += new System.EventHandler(this.btnWhy_Click);
            this.btnShowAll.Location = new System.Drawing.Point(122, 22);
            this.btnShowAll.Name = "btnShowAll";
            this.btnShowAll.Size = new System.Drawing.Size(110, 30);
            this.btnShowAll.TabIndex = 1;
            this.btnShowAll.Text = "Вся база";
            this.btnShowAll.UseVisualStyleBackColor = true;
            this.btnShowAll.Click += new System.EventHandler(this.btnShowAll_Click);
            this.btnShowTool.Location = new System.Drawing.Point(6, 58);
            this.btnShowTool.Name = "btnShowTool";
            this.btnShowTool.Size = new System.Drawing.Size(110, 30);
            this.btnShowTool.TabIndex = 2;
            this.btnShowTool.Text = "Инфо об инструменте";
            this.btnShowTool.UseVisualStyleBackColor = true;
            this.btnShowTool.Click += new System.EventHandler(this.btnShowTool_Click);
            this.btnHasTool.Location = new System.Drawing.Point(122, 58);
            this.btnHasTool.Name = "btnHasTool";
            this.btnHasTool.Size = new System.Drawing.Size(110, 30);
            this.btnHasTool.TabIndex = 3;
            this.btnHasTool.Text = "Есть ли?";
            this.btnHasTool.UseVisualStyleBackColor = true;
            this.btnHasTool.Click += new System.EventHandler(this.btnHasTool_Click);
            this.groupBoxManagement.Controls.Add(this.btnSave);
            this.groupBoxManagement.Controls.Add(this.btnLoad);
            this.groupBoxManagement.Location = new System.Drawing.Point(570, 360);
            this.groupBoxManagement.Name = "groupBoxManagement";
            this.groupBoxManagement.Size = new System.Drawing.Size(218, 60);
            this.groupBoxManagement.TabIndex = 8;
            this.groupBoxManagement.TabStop = false;
            this.groupBoxManagement.Text = "Управление";
            this.btnSave.Location = new System.Drawing.Point(6, 22);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(100, 30);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "Сохранить";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            this.btnLoad.Location = new System.Drawing.Point(112, 22);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(100, 30);
            this.btnLoad.TabIndex = 1;
            this.btnLoad.Text = "Загрузить";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 500);
            this.Controls.Add(this.groupBoxManagement);
            this.Controls.Add(this.groupBoxAnalysis);
            this.Controls.Add(this.groupBoxGame);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.btnNo);
            this.Controls.Add(this.btnYes);
            this.Controls.Add(this.userInputTextBox);
            this.Controls.Add(this.dialogTextBox);
            this.Name = "Form1";
            this.Text = "Акинатор - Инструменты";
            this.groupBoxGame.ResumeLayout(false);
            this.groupBoxAnalysis.ResumeLayout(false);
            this.groupBoxManagement.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.TextBox dialogTextBox;
        private System.Windows.Forms.TextBox userInputTextBox;
        private System.Windows.Forms.Button btnYes;
        private System.Windows.Forms.Button btnNo;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.GroupBox groupBoxGame;
        private System.Windows.Forms.Button btnPlay;
        private System.Windows.Forms.GroupBox groupBoxAnalysis;
        private System.Windows.Forms.Button btnWhy;
        private System.Windows.Forms.Button btnShowAll;
        private System.Windows.Forms.Button btnShowTool;
        private System.Windows.Forms.Button btnHasTool;
        private System.Windows.Forms.GroupBox groupBoxManagement;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnLoad;
    }
}
