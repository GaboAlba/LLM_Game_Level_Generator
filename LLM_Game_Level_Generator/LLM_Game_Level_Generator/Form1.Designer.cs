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
            ListViewItem listViewItem1 = new ListViewItem(new string[] { "X", "Test Tile", "Test Description" }, -1);
            ListViewItem listViewItem2 = new ListViewItem("");
            ListViewItem listViewItem3 = new ListViewItem("");
            ListViewItem listViewItem4 = new ListViewItem("");
            ListViewItem listViewItem5 = new ListViewItem("");
            ListViewItem listViewItem6 = new ListViewItem("");
            ListViewItem listViewItem7 = new ListViewItem("");
            ListViewItem listViewItem8 = new ListViewItem("");
            ListViewItem listViewItem9 = new ListViewItem("");
            ListViewItem listViewItem10 = new ListViewItem("");
            ListViewItem listViewItem11 = new ListViewItem("");
            ListViewItem listViewItem12 = new ListViewItem("");
            ListViewItem listViewItem13 = new ListViewItem("");
            ListViewItem listViewItem14 = new ListViewItem("");
            ListViewItem listViewItem15 = new ListViewItem("");
            ListViewItem listViewItem16 = new ListViewItem("");
            ListViewItem listViewItem17 = new ListViewItem("");
            ListViewItem listViewItem18 = new ListViewItem("");
            ListViewItem listViewItem19 = new ListViewItem("");
            ListViewItem listViewItem20 = new ListViewItem("");
            ListViewItem listViewItem21 = new ListViewItem("");
            ListViewItem listViewItem22 = new ListViewItem("");
            ListViewItem listViewItem23 = new ListViewItem("");
            ListViewItem listViewItem24 = new ListViewItem("");
            ListViewItem listViewItem25 = new ListViewItem("");
            statusStrip1 = new StatusStrip();
            toolStripProgressBar1 = new ToolStripProgressBar();
            toolStripStatusLabel1 = new ToolStripStatusLabel();
            label1 = new Label();
            contextMenuStrip1 = new ContextMenuStrip(components);
            toolStrip1 = new ToolStrip();
            toolStripDropDownButton1 = new ToolStripDropDownButton();
            newFileButton = new ToolStripMenuItem();
            openFileMenuButton = new ToolStripMenuItem();
            saveFileMenuButton = new ToolStripMenuItem();
            toolStripMenuItem1 = new ToolStripMenuItem();
            Help = new ToolStripDropDownButton();
            tabControl1 = new TabControl();
            generalInfoTab = new TabPage();
            tilesTab = new TabPage();
            deleteTileButton = new Button();
            listView1 = new ListView();
            Character = new ColumnHeader();
            TileName = new ColumnHeader();
            TileDescription = new ColumnHeader();
            addTileButton = new Button();
            mapConstraintTab = new TabPage();
            customInstructionsTab = new TabPage();
            outputTab = new TabPage();
            ClearButton = new Button();
            GenerateButton = new Button();
            statusStrip1.SuspendLayout();
            toolStrip1.SuspendLayout();
            tabControl1.SuspendLayout();
            tilesTab.SuspendLayout();
            SuspendLayout();
            // 
            // statusStrip1
            // 
            statusStrip1.Items.AddRange(new ToolStripItem[] { toolStripProgressBar1, toolStripStatusLabel1 });
            statusStrip1.Location = new Point(0, 659);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(1264, 22);
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
            toolStrip1.Size = new Size(1264, 25);
            toolStrip1.TabIndex = 3;
            toolStrip1.Text = "toolStrip1";
            toolStrip1.ItemClicked += toolStrip1_ItemClicked;
            // 
            // toolStripDropDownButton1
            // 
            toolStripDropDownButton1.DisplayStyle = ToolStripItemDisplayStyle.Text;
            toolStripDropDownButton1.DropDownItems.AddRange(new ToolStripItem[] { newFileButton, openFileMenuButton, saveFileMenuButton, toolStripMenuItem1 });
            toolStripDropDownButton1.ImageTransparentColor = Color.Magenta;
            toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            toolStripDropDownButton1.Size = new Size(38, 22);
            toolStripDropDownButton1.Text = "File";
            toolStripDropDownButton1.ToolTipText = "File";
            toolStripDropDownButton1.Click += toolStripDropDownButton1_Click;
            // 
            // newFileButton
            // 
            newFileButton.Name = "newFileButton";
            newFileButton.Size = new Size(180, 22);
            newFileButton.Text = "New";
            // 
            // openFileMenuButton
            // 
            openFileMenuButton.Name = "openFileMenuButton";
            openFileMenuButton.Size = new Size(180, 22);
            openFileMenuButton.Text = "Open";
            // 
            // saveFileMenuButton
            // 
            saveFileMenuButton.Name = "saveFileMenuButton";
            saveFileMenuButton.Size = new Size(180, 22);
            saveFileMenuButton.Text = "Save";
            // 
            // toolStripMenuItem1
            // 
            toolStripMenuItem1.Name = "toolStripMenuItem1";
            toolStripMenuItem1.Size = new Size(180, 22);
            toolStripMenuItem1.Text = "LLM API Keys";
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
            tabControl1.Controls.Add(generalInfoTab);
            tabControl1.Controls.Add(tilesTab);
            tabControl1.Controls.Add(mapConstraintTab);
            tabControl1.Controls.Add(customInstructionsTab);
            tabControl1.Controls.Add(outputTab);
            tabControl1.Dock = DockStyle.Fill;
            tabControl1.Location = new Point(0, 25);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(1264, 634);
            tabControl1.TabIndex = 4;
            // 
            // generalInfoTab
            // 
            generalInfoTab.Location = new Point(4, 24);
            generalInfoTab.Name = "generalInfoTab";
            generalInfoTab.Padding = new Padding(3);
            generalInfoTab.Size = new Size(1256, 606);
            generalInfoTab.TabIndex = 4;
            generalInfoTab.Text = "General Information";
            generalInfoTab.UseVisualStyleBackColor = true;
            // 
            // tilesTab
            // 
            tilesTab.AutoScroll = true;
            tilesTab.Controls.Add(deleteTileButton);
            tilesTab.Controls.Add(listView1);
            tilesTab.Controls.Add(addTileButton);
            tilesTab.Location = new Point(4, 24);
            tilesTab.Name = "tilesTab";
            tilesTab.Size = new Size(1256, 606);
            tilesTab.TabIndex = 2;
            tilesTab.Text = "Tiles";
            tilesTab.UseVisualStyleBackColor = true;
            tilesTab.Click += TilesTab_Click;
            // 
            // deleteTileButton
            // 
            deleteTileButton.AutoSize = true;
            deleteTileButton.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            deleteTileButton.BackColor = Color.Khaki;
            deleteTileButton.Dock = DockStyle.Top;
            deleteTileButton.Location = new Point(0, 28);
            deleteTileButton.Name = "deleteTileButton";
            deleteTileButton.Size = new Size(1256, 28);
            deleteTileButton.TabIndex = 5;
            deleteTileButton.Text = "Delete Tile";
            deleteTileButton.UseCompatibleTextRendering = true;
            deleteTileButton.UseVisualStyleBackColor = false;
            deleteTileButton.Click += button1_Click_1;
            // 
            // listView1
            // 
            listView1.AllowColumnReorder = true;
            listView1.CheckBoxes = true;
            listView1.Columns.AddRange(new ColumnHeader[] { Character, TileName, TileDescription });
            listView1.Dock = DockStyle.Fill;
            listView1.Font = new Font("Segoe UI", 9F);
            listView1.GridLines = true;
            listViewItem1.StateImageIndex = 0;
            listViewItem2.StateImageIndex = 0;
            listViewItem3.StateImageIndex = 0;
            listViewItem4.StateImageIndex = 0;
            listViewItem5.StateImageIndex = 0;
            listViewItem6.StateImageIndex = 0;
            listViewItem7.StateImageIndex = 0;
            listViewItem8.StateImageIndex = 0;
            listViewItem9.StateImageIndex = 0;
            listViewItem10.StateImageIndex = 0;
            listViewItem11.StateImageIndex = 0;
            listViewItem12.StateImageIndex = 0;
            listViewItem13.StateImageIndex = 0;
            listViewItem14.StateImageIndex = 0;
            listViewItem15.StateImageIndex = 0;
            listViewItem16.StateImageIndex = 0;
            listViewItem17.StateImageIndex = 0;
            listViewItem18.StateImageIndex = 0;
            listViewItem19.StateImageIndex = 0;
            listViewItem20.StateImageIndex = 0;
            listViewItem21.StateImageIndex = 0;
            listViewItem22.StateImageIndex = 0;
            listViewItem23.StateImageIndex = 0;
            listViewItem24.StateImageIndex = 0;
            listViewItem25.StateImageIndex = 0;
            listView1.Items.AddRange(new ListViewItem[] { listViewItem1, listViewItem2, listViewItem3, listViewItem4, listViewItem5, listViewItem6, listViewItem7, listViewItem8, listViewItem9, listViewItem10, listViewItem11, listViewItem12, listViewItem13, listViewItem14, listViewItem15, listViewItem16, listViewItem17, listViewItem18, listViewItem19, listViewItem20, listViewItem21, listViewItem22, listViewItem23, listViewItem24, listViewItem25 });
            listView1.Location = new Point(0, 28);
            listView1.Name = "listView1";
            listView1.Size = new Size(1256, 578);
            listView1.TabIndex = 4;
            listView1.UseCompatibleStateImageBehavior = false;
            listView1.View = View.Details;
            listView1.SelectedIndexChanged += listView1_SelectedIndexChanged;
            // 
            // Character
            // 
            Character.Text = "Character";
            Character.Width = 70;
            // 
            // TileName
            // 
            TileName.Text = "Tile Name";
            TileName.Width = 180;
            // 
            // TileDescription
            // 
            TileDescription.Text = "Tile Description";
            TileDescription.Width = 670;
            // 
            // addTileButton
            // 
            addTileButton.AutoSize = true;
            addTileButton.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            addTileButton.BackColor = Color.Turquoise;
            addTileButton.Dock = DockStyle.Top;
            addTileButton.Location = new Point(0, 0);
            addTileButton.Name = "addTileButton";
            addTileButton.Size = new Size(1256, 28);
            addTileButton.TabIndex = 0;
            addTileButton.Text = "Add Tile";
            addTileButton.UseCompatibleTextRendering = true;
            addTileButton.UseVisualStyleBackColor = false;
            addTileButton.Click += button1_Click;
            // 
            // mapConstraintTab
            // 
            mapConstraintTab.Location = new Point(4, 24);
            mapConstraintTab.Name = "mapConstraintTab";
            mapConstraintTab.Padding = new Padding(3);
            mapConstraintTab.Size = new Size(1256, 606);
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
            customInstructionsTab.Size = new Size(1256, 606);
            customInstructionsTab.TabIndex = 1;
            customInstructionsTab.Text = "Custom Instructions";
            customInstructionsTab.UseVisualStyleBackColor = true;
            // 
            // outputTab
            // 
            outputTab.Location = new Point(4, 24);
            outputTab.Name = "outputTab";
            outputTab.Size = new Size(1256, 606);
            outputTab.TabIndex = 3;
            outputTab.Text = "Output";
            outputTab.UseVisualStyleBackColor = true;
            // 
            // ClearButton
            // 
            ClearButton.AutoSize = true;
            ClearButton.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            ClearButton.BackColor = Color.FromArgb(255, 192, 192);
            ClearButton.Dock = DockStyle.Bottom;
            ClearButton.ForeColor = SystemColors.ControlText;
            ClearButton.Location = new Point(0, 634);
            ClearButton.Name = "ClearButton";
            ClearButton.Size = new Size(1264, 25);
            ClearButton.TabIndex = 5;
            ClearButton.Text = "Clear";
            ClearButton.UseVisualStyleBackColor = false;
            // 
            // GenerateButton
            // 
            GenerateButton.AutoSize = true;
            GenerateButton.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            GenerateButton.BackColor = Color.FromArgb(192, 255, 192);
            GenerateButton.Dock = DockStyle.Bottom;
            GenerateButton.Location = new Point(0, 609);
            GenerateButton.Name = "GenerateButton";
            GenerateButton.Size = new Size(1264, 25);
            GenerateButton.TabIndex = 6;
            GenerateButton.Text = "Generate";
            GenerateButton.UseVisualStyleBackColor = false;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            ClientSize = new Size(1264, 681);
            Controls.Add(GenerateButton);
            Controls.Add(ClearButton);
            Controls.Add(tabControl1);
            Controls.Add(toolStrip1);
            Controls.Add(label1);
            Controls.Add(statusStrip1);
            MaximumSize = new Size(3840, 2160);
            MinimumSize = new Size(1280, 720);
            Name = "Form1";
            Text = "`";
            Load += Form1_Load;
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            tabControl1.ResumeLayout(false);
            tilesTab.ResumeLayout(false);
            tilesTab.PerformLayout();
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
        private TabPage tilesTab;
        private TabPage outputTab;
        private Button addTileButton;
        private TabPage generalInfoTab;
        private ListView listView1;
        private ColumnHeader Character;
        private ColumnHeader TileName;
        private ColumnHeader TileDescription;
        private Button deleteTileButton;
        private ToolStripMenuItem toolStripMenuItem1;
        private ToolStripMenuItem newFileButton;
        private ToolStripMenuItem openFileMenuButton;
        private ToolStripMenuItem saveFileMenuButton;
    }
}
