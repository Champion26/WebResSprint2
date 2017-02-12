namespace WebResSprint2
{
    partial class Form1
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
            this.lblContent = new System.Windows.Forms.Label();
            this.lblPopularity = new System.Windows.Forms.Label();
            this.lblHybrid = new System.Windows.Forms.Label();
            this.lblContentValue = new System.Windows.Forms.Label();
            this.lblPoplarityValue = new System.Windows.Forms.Label();
            this.lblHybridValue = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblContent
            // 
            this.lblContent.AutoSize = true;
            this.lblContent.Location = new System.Drawing.Point(143, 113);
            this.lblContent.Name = "lblContent";
            this.lblContent.Size = new System.Drawing.Size(44, 13);
            this.lblContent.TabIndex = 0;
            this.lblContent.Text = "Content";
            this.lblContent.Click += new System.EventHandler(this.label1_Click);
            // 
            // lblPopularity
            // 
            this.lblPopularity.AutoSize = true;
            this.lblPopularity.Location = new System.Drawing.Point(266, 113);
            this.lblPopularity.Name = "lblPopularity";
            this.lblPopularity.Size = new System.Drawing.Size(53, 13);
            this.lblPopularity.TabIndex = 1;
            this.lblPopularity.Text = "Popularity";
            // 
            // lblHybrid
            // 
            this.lblHybrid.AutoSize = true;
            this.lblHybrid.Location = new System.Drawing.Point(392, 113);
            this.lblHybrid.Name = "lblHybrid";
            this.lblHybrid.Size = new System.Drawing.Size(37, 13);
            this.lblHybrid.TabIndex = 2;
            this.lblHybrid.Text = "Hybrid";
            this.lblHybrid.Click += new System.EventHandler(this.label3_Click);
            // 
            // lblContentValue
            // 
            this.lblContentValue.AutoSize = true;
            this.lblContentValue.Location = new System.Drawing.Point(146, 155);
            this.lblContentValue.Name = "lblContentValue";
            this.lblContentValue.Size = new System.Drawing.Size(0, 13);
            this.lblContentValue.TabIndex = 3;
            // 
            // lblPoplarityValue
            // 
            this.lblPoplarityValue.AutoSize = true;
            this.lblPoplarityValue.Location = new System.Drawing.Point(269, 155);
            this.lblPoplarityValue.Name = "lblPoplarityValue";
            this.lblPoplarityValue.Size = new System.Drawing.Size(0, 13);
            this.lblPoplarityValue.TabIndex = 4;
            // 
            // lblHybridValue
            // 
            this.lblHybridValue.AutoSize = true;
            this.lblHybridValue.Location = new System.Drawing.Point(392, 155);
            this.lblHybridValue.Name = "lblHybridValue";
            this.lblHybridValue.Size = new System.Drawing.Size(0, 13);
            this.lblHybridValue.TabIndex = 5;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(632, 392);
            this.Controls.Add(this.lblHybridValue);
            this.Controls.Add(this.lblPoplarityValue);
            this.Controls.Add(this.lblContentValue);
            this.Controls.Add(this.lblHybrid);
            this.Controls.Add(this.lblPopularity);
            this.Controls.Add(this.lblContent);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblContent;
        private System.Windows.Forms.Label lblPopularity;
        private System.Windows.Forms.Label lblHybrid;
        private System.Windows.Forms.Label lblContentValue;
        private System.Windows.Forms.Label lblPoplarityValue;
        private System.Windows.Forms.Label lblHybridValue;

    }
}

