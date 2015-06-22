namespace Ouroboros
{
    partial class CollectorReport
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CollectorReport));
            this.collectorReportLabel = new System.Windows.Forms.Label();
            this.totalProgressBar = new System.Windows.Forms.ProgressBar();
            this.totalCardsCollected = new System.Windows.Forms.Label();
            this.uniqueCardsCollected = new System.Windows.Forms.Label();
            this.uniqueProgressBar = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // collectorReportLabel
            // 
            this.collectorReportLabel.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.collectorReportLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.collectorReportLabel.Location = new System.Drawing.Point(-3, 9);
            this.collectorReportLabel.Name = "collectorReportLabel";
            this.collectorReportLabel.Size = new System.Drawing.Size(701, 52);
            this.collectorReportLabel.TabIndex = 0;
            this.collectorReportLabel.Text = "Ouroboros Collector Report";
            this.collectorReportLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // totalProgressBar
            // 
            this.totalProgressBar.Location = new System.Drawing.Point(12, 111);
            this.totalProgressBar.Name = "totalProgressBar";
            this.totalProgressBar.Size = new System.Drawing.Size(674, 45);
            this.totalProgressBar.TabIndex = 1;
            // 
            // totalCardsCollected
            // 
            this.totalCardsCollected.AutoSize = true;
            this.totalCardsCollected.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.totalCardsCollected.Location = new System.Drawing.Point(7, 83);
            this.totalCardsCollected.Name = "totalCardsCollected";
            this.totalCardsCollected.Size = new System.Drawing.Size(390, 25);
            this.totalCardsCollected.TabIndex = 2;
            this.totalCardsCollected.Text = "Total Cards Collected: 0 of 0 (100.00%)";
            // 
            // uniqueCardsCollected
            // 
            this.uniqueCardsCollected.AutoSize = true;
            this.uniqueCardsCollected.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uniqueCardsCollected.Location = new System.Drawing.Point(8, 181);
            this.uniqueCardsCollected.Name = "uniqueCardsCollected";
            this.uniqueCardsCollected.Size = new System.Drawing.Size(351, 24);
            this.uniqueCardsCollected.TabIndex = 4;
            this.uniqueCardsCollected.Text = "Unique Cards Collected: 0 of 0 (100.00%)";
            // 
            // uniqueProgressBar
            // 
            this.uniqueProgressBar.Location = new System.Drawing.Point(13, 209);
            this.uniqueProgressBar.Name = "uniqueProgressBar";
            this.uniqueProgressBar.Size = new System.Drawing.Size(674, 28);
            this.uniqueProgressBar.TabIndex = 3;
            // 
            // CollectorReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(699, 260);
            this.Controls.Add(this.uniqueCardsCollected);
            this.Controls.Add(this.uniqueProgressBar);
            this.Controls.Add(this.totalCardsCollected);
            this.Controls.Add(this.totalProgressBar);
            this.Controls.Add(this.collectorReportLabel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "CollectorReport";
            this.Text = "Ouroboros Collector Report";
            this.Load += new System.EventHandler(this.CollectorReport_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label collectorReportLabel;
        private System.Windows.Forms.ProgressBar totalProgressBar;
        private System.Windows.Forms.Label totalCardsCollected;
        private System.Windows.Forms.Label uniqueCardsCollected;
        private System.Windows.Forms.ProgressBar uniqueProgressBar;
    }
}