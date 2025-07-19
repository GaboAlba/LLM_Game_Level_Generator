namespace LLM_Game_Level_Generator
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            statusStrip1 = new StatusStrip();
            toolStripProgressBar1 = new ToolStripProgressBar();
            toolStripStatusLabel1 = new ToolStripStatusLabel();
            label1 = new Label();
            contextMenuStrip1 = new ContextMenuStrip(components);
            toolStrip1 = new ToolStrip();
            toolStripDropDownButton1 = new ToolStripDropDownButton();
            Help = new ToolStripDropDownButton();
            tabControl1 = new TabControl();
            TilesTab = new TabPage();
            mapConstraintTab = new TabPage();
            customInstructionsTab = new TabPage();
            outputTab = new TabPage();
            ClearButton = new Button();
            GenerateButton = new Button();
            statusStrip1.SuspendLayout();
            toolStrip1.SuspendLayout();
            tabControl1.SuspendLayout();
            SuspendLayout();
            // 
            // statusStrip1
            // 
            statusStrip1.Items.AddRange(new ToolStripItem[] { toolStripProgressBar1, toolStripStatusLabel1 });
            statusStrip1.Location = new Point(0, 479);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(944, 22);
            statusStrip1.TabIndex = 0;
            statusStrip1.Text = "statusStrip1";
            // 
            // toolStripProgressBar1
            // 
            toolStripProgressBar1.Name = "toolStripProgressBar1";
            toolStripProgressBar1.Size = new Size(100, 16);
            // 
            // toolStripStatusLabel1
            // 
            toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            toolStripStatusLabel1.Size = new Size(118, 17);
            toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            toolStripStatusLabel1.Click += toolStripStatusLabel1_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 20F);
            label1.Location = new Point(0, 25);
            label1.Name = "label1";
            label1.Size = new Size(358, 37);
            label1.TabIndex = 1;
            label1.Text = "Video Game Level Generator";
            label1.Click += label1_Click;
            // 
            // contextMenuStrip1
            // 
            contextMenuStrip1.Name = "contextMenuStrip1";
            contextMenuStrip1.Size = new Size(61, 4);
            // 
            // toolStrip1
            // 
            toolStrip1.Items.AddRange(new ToolStripItem[] { toolStripDropDownButton1, Help });
            toolStrip1.Location = new Point(0, 0);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Size = new Size(944, 25);
            toolStrip1.TabIndex = 3;
            toolStrip1.Text = "toolStrip1";
            toolStrip1.ItemClicked += toolStrip1_ItemClicked;
            // 
            // toolStripDropDownButton1
            // 
            toolStripDropDownButton1.DisplayStyle = ToolStripItemDisplayStyle.Text;
            toolStripDropDownButton1.ImageTransparentColor = Color.Magenta;
            toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            toolStripDropDownButton1.Size = new Size(38, 22);
            toolStripDropDownButton1.Text = "File";
            toolStripDropDownButton1.ToolTipText = "File";
            // 
            // Help
            // 
            Help.DisplayStyle = ToolStripItemDisplayStyle.Text;
            Help.ImageTransparentColor = Color.Magenta;
            Help.Name = "Help";
            Help.Size = new Size(45, 22);
            Help.Text = "Help";
            Help.ToolTipText = "Help";
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(TilesTab);
            tabControl1.Controls.Add(mapConstraintTab);
            tabControl1.Controls.Add(customInstructionsTab);
            tabControl1.Controls.Add(outputTab);
            tabControl1.Location = new Point(0, 65);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(932, 376);
            tabControl1.TabIndex = 4;
            // 
            // TilesTab
            // 
            TilesTab.Location = new Point(4, 24);
            TilesTab.Name = "TilesTab";
            TilesTab.Size = new Size(924, 348);
            TilesTab.TabIndex = 2;
            TilesTab.Text = "Tiles";
            TilesTab.UseVisualStyleBackColor = true;
            // 
            // mapConstraintTab
            // 
            mapConstraintTab.Location = new Point(4, 24);
            mapConstraintTab.Name = "mapConstraintTab";
            mapConstraintTab.Padding = new Padding(3);
            mapConstraintTab.Size = new Size(924, 348);
            mapConstraintTab.TabIndex = 0;
            mapConstraintTab.Text = "Map Constraints";
            mapConstraintTab.UseVisualStyleBackColor = true;
            mapConstraintTab.Click += mainParamTab_Click;
            // 
            // customInstructionsTab
            // 
            customInstructionsTab.Location = new Point(4, 24);
            customInstructionsTab.Name = "customInstructionsTab";
            customInstructionsTab.Padding = new Padding(3);
            customInstructionsTab.Size = new Size(924, 348);
            customInstructionsTab.TabIndex = 1;
            customInstructionsTab.Text = "Custom Instructions";
            customInstructionsTab.UseVisualStyleBackColor = true;
            // 
            // outputTab
            // 
            outputTab.Location = new Point(4, 24);
            outputTab.Name = "outputTab";
            outputTab.Size = new Size(924, 348);
            outputTab.TabIndex = 3;
            outputTab.Text = "Output";
            outputTab.UseVisualStyleBackColor = true;
            // 
            // ClearButton
            // 
            ClearButton.BackColor = Color.FromArgb(255, 192, 192);
            ClearButton.ForeColor = SystemColors.ControlText;
            ClearButton.Location = new Point(4, 447);
            ClearButton.Name = "ClearButton";
            ClearButton.Size = new Size(427, 29);
            ClearButton.TabIndex = 5;
            ClearButton.Text = "Clear";
            ClearButton.UseVisualStyleBackColor = false;
            // 
            // GenerateButton
            // 
            GenerateButton.BackColor = Color.FromArgb(192, 255, 192);
            GenerateButton.Location = new Point(437, 447);
            GenerateButton.Name = "GenerateButton";
            GenerateButton.Size = new Size(499, 29);
            GenerateButton.TabIndex = 6;
            GenerateButton.Text = "Generate";
            GenerateButton.UseVisualStyleBackColor = false;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(944, 501);
            Controls.Add(GenerateButton);
            Controls.Add(ClearButton);
            Controls.Add(tabControl1);
            Controls.Add(toolStrip1);
            Controls.Add(label1);
            Controls.Add(statusStrip1);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            tabControl1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private StatusStrip statusStrip1;
        private ToolStripProgressBar toolStripProgressBar1;
        private ToolStripStatusLabel toolStripStatusLabel1;
        private Label label1;
        private ContextMenuStrip contextMenuStrip1;
        private ToolStrip toolStrip1;
        private ToolStripDropDownButton Help;
        private ToolStripDropDownButton toolStripDropDownButton1;
        private TabControl tabControl1;
        private TabPage mapConstraintTab;
        private TabPage customInstructionsTab;
        private Button ClearButton;
        private Button GenerateButton;
        private TabPage TilesTab;
        private TabPage outputTab;
    }
}
