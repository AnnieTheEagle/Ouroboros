namespace Ouroboros
{
    partial class CardBrowser
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CardBrowser));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveDatabaseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.collectorReportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setBrowser = new System.Windows.Forms.ListView();
            this.cardNameLabel = new System.Windows.Forms.Label();
            this.cardAttribute = new System.Windows.Forms.Label();
            this.cardATK = new System.Windows.Forms.Label();
            this.cardDEF = new System.Windows.Forms.Label();
            this.nameLabel = new System.Windows.Forms.Label();
            this.attributeLabel = new System.Windows.Forms.Label();
            this.typeLabel = new System.Windows.Forms.Label();
            this.cardType = new System.Windows.Forms.Label();
            this.subTypeLabel = new System.Windows.Forms.Label();
            this.cardSubType = new System.Windows.Forms.Label();
            this.cardTypingBox = new System.Windows.Forms.GroupBox();
            this.monsterDetailsBox = new System.Windows.Forms.GroupBox();
            this.cardLevel = new System.Windows.Forms.Label();
            this.levelLabel = new System.Windows.Forms.Label();
            this.defenseLabel = new System.Windows.Forms.Label();
            this.attackLabel = new System.Windows.Forms.Label();
            this.noPendulumTextGroup = new System.Windows.Forms.GroupBox();
            this.textLabel_np = new System.Windows.Forms.Label();
            this.cardText_np = new System.Windows.Forms.TextBox();
            this.collectionBox = new System.Windows.Forms.GroupBox();
            this.statusLabel = new System.Windows.Forms.Label();
            this.numberOwnedLabel = new System.Windows.Forms.Label();
            this.collectionStatus = new System.Windows.Forms.Label();
            this.havePercentage = new System.Windows.Forms.Label();
            this.forwardSlashLabel = new System.Windows.Forms.Label();
            this.outOfLabel = new System.Windows.Forms.Label();
            this.haveCount = new System.Windows.Forms.Label();
            this.setsLabel = new System.Windows.Forms.Label();
            this.pendulumTextGroup = new System.Windows.Forms.GroupBox();
            this.pendulumTextLabel = new System.Windows.Forms.Label();
            this.pendulumText = new System.Windows.Forms.TextBox();
            this.textLabel_p = new System.Windows.Forms.Label();
            this.cardText_p = new System.Windows.Forms.TextBox();
            this.cardList = new Ouroboros.CardListBox();
            this.addCardSetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.cardTypingBox.SuspendLayout();
            this.monsterDetailsBox.SuspendLayout();
            this.noPendulumTextGroup.SuspendLayout();
            this.collectionBox.SuspendLayout();
            this.pendulumTextGroup.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.settingsToolStripMenuItem,
            this.settingsToolStripMenuItem1});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1481, 24);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addCardSetToolStripMenuItem,
            this.saveDatabaseToolStripMenuItem,
            this.aboutToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(76, 20);
            this.settingsToolStripMenuItem.Text = "Ouroboros";
            // 
            // saveDatabaseToolStripMenuItem
            // 
            this.saveDatabaseToolStripMenuItem.Name = "saveDatabaseToolStripMenuItem";
            this.saveDatabaseToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveDatabaseToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
            this.saveDatabaseToolStripMenuItem.Text = "Save Database";
            this.saveDatabaseToolStripMenuItem.Click += new System.EventHandler(this.saveDatabaseToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // settingsToolStripMenuItem1
            // 
            this.settingsToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.collectorReportToolStripMenuItem});
            this.settingsToolStripMenuItem1.Name = "settingsToolStripMenuItem1";
            this.settingsToolStripMenuItem1.Size = new System.Drawing.Size(58, 20);
            this.settingsToolStripMenuItem1.Text = "Utilities";
            // 
            // collectorReportToolStripMenuItem
            // 
            this.collectorReportToolStripMenuItem.Name = "collectorReportToolStripMenuItem";
            this.collectorReportToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.R)));
            this.collectorReportToolStripMenuItem.Size = new System.Drawing.Size(201, 22);
            this.collectorReportToolStripMenuItem.Text = "Collector Report";
            this.collectorReportToolStripMenuItem.Click += new System.EventHandler(this.collectorReportToolStripMenuItem_Click);
            // 
            // setBrowser
            // 
            this.setBrowser.CheckBoxes = true;
            this.setBrowser.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.setBrowser.FullRowSelect = true;
            this.setBrowser.GridLines = true;
            this.setBrowser.HideSelection = false;
            this.setBrowser.Location = new System.Drawing.Point(421, 740);
            this.setBrowser.Name = "setBrowser";
            this.setBrowser.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.setBrowser.ShowItemToolTips = true;
            this.setBrowser.Size = new System.Drawing.Size(1048, 286);
            this.setBrowser.TabIndex = 3;
            this.setBrowser.UseCompatibleStateImageBehavior = false;
            this.setBrowser.View = System.Windows.Forms.View.Details;
            this.setBrowser.ColumnWidthChanging += new System.Windows.Forms.ColumnWidthChangingEventHandler(this.setBrowser_ColumnWidthChanging);
            this.setBrowser.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.setBrowser_ItemCheck);
            this.setBrowser.KeyDown += new System.Windows.Forms.KeyEventHandler(this.setBrowser_KeyDown);
            // 
            // cardNameLabel
            // 
            this.cardNameLabel.AutoSize = true;
            this.cardNameLabel.BackColor = System.Drawing.Color.Transparent;
            this.cardNameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cardNameLabel.Location = new System.Drawing.Point(423, 50);
            this.cardNameLabel.Name = "cardNameLabel";
            this.cardNameLabel.Size = new System.Drawing.Size(493, 55);
            this.cardNameLabel.TabIndex = 4;
            this.cardNameLabel.Text = "Nulleronius Nulleronia";
            this.cardNameLabel.UseMnemonic = false;
            // 
            // cardAttribute
            // 
            this.cardAttribute.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cardAttribute.Location = new System.Drawing.Point(6, 45);
            this.cardAttribute.Name = "cardAttribute";
            this.cardAttribute.Size = new System.Drawing.Size(328, 44);
            this.cardAttribute.TabIndex = 5;
            this.cardAttribute.Text = "null";
            this.cardAttribute.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cardATK
            // 
            this.cardATK.AutoSize = true;
            this.cardATK.Font = new System.Drawing.Font("Microsoft Sans Serif", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cardATK.Location = new System.Drawing.Point(6, 44);
            this.cardATK.Name = "cardATK";
            this.cardATK.Size = new System.Drawing.Size(176, 73);
            this.cardATK.TabIndex = 6;
            this.cardATK.Text = "9999";
            // 
            // cardDEF
            // 
            this.cardDEF.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cardDEF.Font = new System.Drawing.Font("Microsoft Sans Serif", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cardDEF.Location = new System.Drawing.Point(663, 44);
            this.cardDEF.Name = "cardDEF";
            this.cardDEF.Size = new System.Drawing.Size(384, 73);
            this.cardDEF.TabIndex = 7;
            this.cardDEF.Text = "9999";
            this.cardDEF.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // nameLabel
            // 
            this.nameLabel.AutoSize = true;
            this.nameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nameLabel.Location = new System.Drawing.Point(417, 31);
            this.nameLabel.Name = "nameLabel";
            this.nameLabel.Size = new System.Drawing.Size(106, 24);
            this.nameLabel.TabIndex = 8;
            this.nameLabel.Text = "Card Name";
            // 
            // attributeLabel
            // 
            this.attributeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.attributeLabel.Location = new System.Drawing.Point(8, 16);
            this.attributeLabel.Name = "attributeLabel";
            this.attributeLabel.Size = new System.Drawing.Size(227, 28);
            this.attributeLabel.TabIndex = 9;
            this.attributeLabel.Text = "Card Attribute";
            // 
            // typeLabel
            // 
            this.typeLabel.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.typeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.typeLabel.Location = new System.Drawing.Point(397, 16);
            this.typeLabel.Name = "typeLabel";
            this.typeLabel.Size = new System.Drawing.Size(225, 24);
            this.typeLabel.TabIndex = 11;
            this.typeLabel.Text = "Card Type";
            this.typeLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // cardType
            // 
            this.cardType.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.cardType.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cardType.Location = new System.Drawing.Point(353, 45);
            this.cardType.Name = "cardType";
            this.cardType.Size = new System.Drawing.Size(308, 44);
            this.cardType.TabIndex = 10;
            this.cardType.Text = "null";
            this.cardType.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // subTypeLabel
            // 
            this.subTypeLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.subTypeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.subTypeLabel.Location = new System.Drawing.Point(819, 16);
            this.subTypeLabel.Name = "subTypeLabel";
            this.subTypeLabel.Size = new System.Drawing.Size(223, 28);
            this.subTypeLabel.TabIndex = 13;
            this.subTypeLabel.Text = "Card Subtype";
            this.subTypeLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // cardSubType
            // 
            this.cardSubType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cardSubType.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cardSubType.Location = new System.Drawing.Point(684, 45);
            this.cardSubType.Name = "cardSubType";
            this.cardSubType.Size = new System.Drawing.Size(358, 44);
            this.cardSubType.TabIndex = 12;
            this.cardSubType.Text = "null";
            this.cardSubType.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cardTypingBox
            // 
            this.cardTypingBox.Controls.Add(this.attributeLabel);
            this.cardTypingBox.Controls.Add(this.subTypeLabel);
            this.cardTypingBox.Controls.Add(this.cardAttribute);
            this.cardTypingBox.Controls.Add(this.cardSubType);
            this.cardTypingBox.Controls.Add(this.cardType);
            this.cardTypingBox.Controls.Add(this.typeLabel);
            this.cardTypingBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cardTypingBox.Location = new System.Drawing.Point(421, 119);
            this.cardTypingBox.Name = "cardTypingBox";
            this.cardTypingBox.Size = new System.Drawing.Size(1048, 100);
            this.cardTypingBox.TabIndex = 14;
            this.cardTypingBox.TabStop = false;
            this.cardTypingBox.Text = "Card Type Details";
            // 
            // monsterDetailsBox
            // 
            this.monsterDetailsBox.Controls.Add(this.cardLevel);
            this.monsterDetailsBox.Controls.Add(this.levelLabel);
            this.monsterDetailsBox.Controls.Add(this.defenseLabel);
            this.monsterDetailsBox.Controls.Add(this.attackLabel);
            this.monsterDetailsBox.Controls.Add(this.cardATK);
            this.monsterDetailsBox.Controls.Add(this.cardDEF);
            this.monsterDetailsBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.monsterDetailsBox.Location = new System.Drawing.Point(421, 234);
            this.monsterDetailsBox.Name = "monsterDetailsBox";
            this.monsterDetailsBox.Size = new System.Drawing.Size(1048, 120);
            this.monsterDetailsBox.TabIndex = 15;
            this.monsterDetailsBox.TabStop = false;
            this.monsterDetailsBox.Text = "Monster Details";
            // 
            // cardLevel
            // 
            this.cardLevel.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.cardLevel.Font = new System.Drawing.Font("Microsoft Sans Serif", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cardLevel.Location = new System.Drawing.Point(422, 44);
            this.cardLevel.Name = "cardLevel";
            this.cardLevel.Size = new System.Drawing.Size(173, 73);
            this.cardLevel.TabIndex = 16;
            this.cardLevel.Text = "13";
            this.cardLevel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // levelLabel
            // 
            this.levelLabel.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.levelLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.levelLabel.Location = new System.Drawing.Point(397, 18);
            this.levelLabel.Name = "levelLabel";
            this.levelLabel.Size = new System.Drawing.Size(225, 37);
            this.levelLabel.TabIndex = 15;
            this.levelLabel.Text = "Level";
            this.levelLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // defenseLabel
            // 
            this.defenseLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.defenseLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.defenseLabel.Location = new System.Drawing.Point(887, 18);
            this.defenseLabel.Name = "defenseLabel";
            this.defenseLabel.Size = new System.Drawing.Size(145, 37);
            this.defenseLabel.TabIndex = 14;
            this.defenseLabel.Text = "DEF";
            this.defenseLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // attackLabel
            // 
            this.attackLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.attackLabel.Location = new System.Drawing.Point(13, 18);
            this.attackLabel.Name = "attackLabel";
            this.attackLabel.Size = new System.Drawing.Size(147, 37);
            this.attackLabel.TabIndex = 10;
            this.attackLabel.Text = "ATK";
            // 
            // noPendulumTextGroup
            // 
            this.noPendulumTextGroup.Controls.Add(this.textLabel_np);
            this.noPendulumTextGroup.Controls.Add(this.cardText_np);
            this.noPendulumTextGroup.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.noPendulumTextGroup.Location = new System.Drawing.Point(421, 370);
            this.noPendulumTextGroup.Name = "noPendulumTextGroup";
            this.noPendulumTextGroup.Size = new System.Drawing.Size(532, 331);
            this.noPendulumTextGroup.TabIndex = 16;
            this.noPendulumTextGroup.TabStop = false;
            this.noPendulumTextGroup.Text = "Card Text";
            // 
            // textLabel_np
            // 
            this.textLabel_np.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textLabel_np.Location = new System.Drawing.Point(7, 26);
            this.textLabel_np.Name = "textLabel_np";
            this.textLabel_np.Size = new System.Drawing.Size(227, 28);
            this.textLabel_np.TabIndex = 14;
            this.textLabel_np.Text = "Card Text";
            // 
            // cardText_np
            // 
            this.cardText_np.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cardText_np.Location = new System.Drawing.Point(7, 55);
            this.cardText_np.Multiline = true;
            this.cardText_np.Name = "cardText_np";
            this.cardText_np.ReadOnly = true;
            this.cardText_np.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.cardText_np.Size = new System.Drawing.Size(518, 270);
            this.cardText_np.TabIndex = 0;
            // 
            // collectionBox
            // 
            this.collectionBox.Controls.Add(this.statusLabel);
            this.collectionBox.Controls.Add(this.numberOwnedLabel);
            this.collectionBox.Controls.Add(this.collectionStatus);
            this.collectionBox.Controls.Add(this.havePercentage);
            this.collectionBox.Controls.Add(this.forwardSlashLabel);
            this.collectionBox.Controls.Add(this.outOfLabel);
            this.collectionBox.Controls.Add(this.haveCount);
            this.collectionBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.collectionBox.Location = new System.Drawing.Point(959, 370);
            this.collectionBox.Name = "collectionBox";
            this.collectionBox.Size = new System.Drawing.Size(509, 331);
            this.collectionBox.TabIndex = 17;
            this.collectionBox.TabStop = false;
            this.collectionBox.Text = "Card Collection Status";
            // 
            // statusLabel
            // 
            this.statusLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.statusLabel.Location = new System.Drawing.Point(6, 236);
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(322, 31);
            this.statusLabel.TabIndex = 22;
            this.statusLabel.Text = "Collector Status";
            // 
            // numberOwnedLabel
            // 
            this.numberOwnedLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numberOwnedLabel.Location = new System.Drawing.Point(6, 24);
            this.numberOwnedLabel.Name = "numberOwnedLabel";
            this.numberOwnedLabel.Size = new System.Drawing.Size(350, 28);
            this.numberOwnedLabel.TabIndex = 21;
            this.numberOwnedLabel.Text = "Number of this card owned";
            // 
            // collectionStatus
            // 
            this.collectionStatus.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.collectionStatus.BackColor = System.Drawing.Color.Transparent;
            this.collectionStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.collectionStatus.Location = new System.Drawing.Point(10, 267);
            this.collectionStatus.Name = "collectionStatus";
            this.collectionStatus.Size = new System.Drawing.Size(484, 45);
            this.collectionStatus.TabIndex = 18;
            this.collectionStatus.Text = "null";
            this.collectionStatus.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // havePercentage
            // 
            this.havePercentage.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.havePercentage.BackColor = System.Drawing.Color.Transparent;
            this.havePercentage.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.havePercentage.Location = new System.Drawing.Point(285, 95);
            this.havePercentage.Name = "havePercentage";
            this.havePercentage.Size = new System.Drawing.Size(218, 76);
            this.havePercentage.TabIndex = 20;
            this.havePercentage.Text = "(100.00%)";
            this.havePercentage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // forwardSlashLabel
            // 
            this.forwardSlashLabel.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.forwardSlashLabel.BackColor = System.Drawing.Color.Transparent;
            this.forwardSlashLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 72F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.forwardSlashLabel.Location = new System.Drawing.Point(107, 55);
            this.forwardSlashLabel.Name = "forwardSlashLabel";
            this.forwardSlashLabel.Size = new System.Drawing.Size(86, 150);
            this.forwardSlashLabel.TabIndex = 19;
            this.forwardSlashLabel.Text = "/";
            this.forwardSlashLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // outOfLabel
            // 
            this.outOfLabel.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.outOfLabel.BackColor = System.Drawing.Color.Transparent;
            this.outOfLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.outOfLabel.Location = new System.Drawing.Point(166, 95);
            this.outOfLabel.Name = "outOfLabel";
            this.outOfLabel.Size = new System.Drawing.Size(129, 90);
            this.outOfLabel.TabIndex = 18;
            this.outOfLabel.Text = "0";
            this.outOfLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // haveCount
            // 
            this.haveCount.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.haveCount.BackColor = System.Drawing.Color.Transparent;
            this.haveCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.haveCount.Location = new System.Drawing.Point(5, 52);
            this.haveCount.Name = "haveCount";
            this.haveCount.Size = new System.Drawing.Size(123, 85);
            this.haveCount.TabIndex = 17;
            this.haveCount.Text = "0";
            this.haveCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // setsLabel
            // 
            this.setsLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.setsLabel.Location = new System.Drawing.Point(421, 709);
            this.setsLabel.Name = "setsLabel";
            this.setsLabel.Size = new System.Drawing.Size(227, 28);
            this.setsLabel.TabIndex = 15;
            this.setsLabel.Text = "Card Sets";
            // 
            // pendulumTextGroup
            // 
            this.pendulumTextGroup.Controls.Add(this.pendulumTextLabel);
            this.pendulumTextGroup.Controls.Add(this.pendulumText);
            this.pendulumTextGroup.Controls.Add(this.textLabel_p);
            this.pendulumTextGroup.Controls.Add(this.cardText_p);
            this.pendulumTextGroup.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pendulumTextGroup.Location = new System.Drawing.Point(421, 370);
            this.pendulumTextGroup.Name = "pendulumTextGroup";
            this.pendulumTextGroup.Size = new System.Drawing.Size(532, 331);
            this.pendulumTextGroup.TabIndex = 17;
            this.pendulumTextGroup.TabStop = false;
            this.pendulumTextGroup.Text = "Card Text";
            // 
            // pendulumTextLabel
            // 
            this.pendulumTextLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pendulumTextLabel.Location = new System.Drawing.Point(6, 174);
            this.pendulumTextLabel.Name = "pendulumTextLabel";
            this.pendulumTextLabel.Size = new System.Drawing.Size(227, 28);
            this.pendulumTextLabel.TabIndex = 16;
            this.pendulumTextLabel.Text = "Pendulum Effect";
            // 
            // pendulumText
            // 
            this.pendulumText.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pendulumText.Location = new System.Drawing.Point(6, 203);
            this.pendulumText.Multiline = true;
            this.pendulumText.Name = "pendulumText";
            this.pendulumText.ReadOnly = true;
            this.pendulumText.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.pendulumText.Size = new System.Drawing.Size(518, 116);
            this.pendulumText.TabIndex = 15;
            // 
            // textLabel_p
            // 
            this.textLabel_p.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textLabel_p.Location = new System.Drawing.Point(7, 26);
            this.textLabel_p.Name = "textLabel_p";
            this.textLabel_p.Size = new System.Drawing.Size(227, 28);
            this.textLabel_p.TabIndex = 14;
            this.textLabel_p.Text = "Card Text";
            // 
            // cardText_p
            // 
            this.cardText_p.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cardText_p.Location = new System.Drawing.Point(7, 55);
            this.cardText_p.Multiline = true;
            this.cardText_p.Name = "cardText_p";
            this.cardText_p.ReadOnly = true;
            this.cardText_p.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.cardText_p.Size = new System.Drawing.Size(518, 116);
            this.cardText_p.TabIndex = 0;
            // 
            // cardList
            // 
            this.cardList.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cardList.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cardList.FormattingEnabled = true;
            this.cardList.ItemHeight = 24;
            this.cardList.Location = new System.Drawing.Point(1, 26);
            this.cardList.Name = "cardList";
            this.cardList.Size = new System.Drawing.Size(414, 1012);
            this.cardList.TabIndex = 1;
            this.cardList.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.cardList_DrawItem);
            this.cardList.SelectedIndexChanged += new System.EventHandler(this.cardList_SelectedIndexChanged);
            this.cardList.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cardList_KeyDown);
            // 
            // addCardSetToolStripMenuItem
            // 
            this.addCardSetToolStripMenuItem.Name = "addCardSetToolStripMenuItem";
            this.addCardSetToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
            this.addCardSetToolStripMenuItem.Text = "Add Card Set";
            this.addCardSetToolStripMenuItem.Click += new System.EventHandler(this.addCardSetToolStripMenuItem_Click);
            // 
            // CardBrowser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1481, 1038);
            this.Controls.Add(this.pendulumTextGroup);
            this.Controls.Add(this.setsLabel);
            this.Controls.Add(this.collectionBox);
            this.Controls.Add(this.noPendulumTextGroup);
            this.Controls.Add(this.monsterDetailsBox);
            this.Controls.Add(this.cardTypingBox);
            this.Controls.Add(this.nameLabel);
            this.Controls.Add(this.cardNameLabel);
            this.Controls.Add(this.setBrowser);
            this.Controls.Add(this.cardList);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "CardBrowser";
            this.Text = "Ouroboros";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CardBrowser_FormClosing);
            this.Load += new System.EventHandler(this.CardBrowser_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.cardTypingBox.ResumeLayout(false);
            this.monsterDetailsBox.ResumeLayout(false);
            this.monsterDetailsBox.PerformLayout();
            this.noPendulumTextGroup.ResumeLayout(false);
            this.noPendulumTextGroup.PerformLayout();
            this.collectionBox.ResumeLayout(false);
            this.pendulumTextGroup.ResumeLayout(false);
            this.pendulumTextGroup.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private CardListBox cardList;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem1;
        private System.Windows.Forms.ListView setBrowser;
        private System.Windows.Forms.Label cardNameLabel;
        private System.Windows.Forms.Label cardAttribute;
        private System.Windows.Forms.Label cardATK;
        private System.Windows.Forms.Label cardDEF;
        private System.Windows.Forms.Label nameLabel;
        private System.Windows.Forms.Label attributeLabel;
        private System.Windows.Forms.Label typeLabel;
        private System.Windows.Forms.Label cardType;
        private System.Windows.Forms.Label subTypeLabel;
        private System.Windows.Forms.Label cardSubType;
        private System.Windows.Forms.GroupBox cardTypingBox;
        private System.Windows.Forms.GroupBox monsterDetailsBox;
        private System.Windows.Forms.Label defenseLabel;
        private System.Windows.Forms.Label attackLabel;
        private System.Windows.Forms.Label cardLevel;
        private System.Windows.Forms.Label levelLabel;
        private System.Windows.Forms.GroupBox noPendulumTextGroup;
        private System.Windows.Forms.Label textLabel_np;
        private System.Windows.Forms.TextBox cardText_np;
        private System.Windows.Forms.GroupBox collectionBox;
        private System.Windows.Forms.Label setsLabel;
        private System.Windows.Forms.Label havePercentage;
        private System.Windows.Forms.Label forwardSlashLabel;
        private System.Windows.Forms.Label outOfLabel;
        private System.Windows.Forms.Label haveCount;
        private System.Windows.Forms.Label statusLabel;
        private System.Windows.Forms.Label numberOwnedLabel;
        private System.Windows.Forms.Label collectionStatus;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem collectorReportToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveDatabaseToolStripMenuItem;
        private System.Windows.Forms.GroupBox pendulumTextGroup;
        private System.Windows.Forms.Label pendulumTextLabel;
        private System.Windows.Forms.TextBox pendulumText;
        private System.Windows.Forms.Label textLabel_p;
        private System.Windows.Forms.TextBox cardText_p;
        private System.Windows.Forms.ToolStripMenuItem addCardSetToolStripMenuItem;
    }
}