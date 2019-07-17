namespace Adlite.IPInserter
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
      this.btnInsert = new System.Windows.Forms.Button();
      this.btnFindFile = new System.Windows.Forms.Button();
      this.labelFilePath = new System.Windows.Forms.Label();
      this.labelMessage = new System.Windows.Forms.Label();
      this.label1 = new System.Windows.Forms.Label();
      this.btnMnoFile = new System.Windows.Forms.Button();
      this.SuspendLayout();
      // 
      // btnInsert
      // 
      this.btnInsert.Location = new System.Drawing.Point(15, 120);
      this.btnInsert.Name = "btnInsert";
      this.btnInsert.Size = new System.Drawing.Size(101, 23);
      this.btnInsert.TabIndex = 4;
      this.btnInsert.Text = "Insert";
      this.btnInsert.UseVisualStyleBackColor = true;
      this.btnInsert.Click += new System.EventHandler(this.btnInsert_Click);
      // 
      // btnFindFile
      // 
      this.btnFindFile.Location = new System.Drawing.Point(12, 12);
      this.btnFindFile.Name = "btnFindFile";
      this.btnFindFile.Size = new System.Drawing.Size(195, 23);
      this.btnFindFile.TabIndex = 5;
      this.btnFindFile.Text = "Find Country file";
      this.btnFindFile.UseVisualStyleBackColor = true;
      this.btnFindFile.Click += new System.EventHandler(this.btnFindFile_Click);
      // 
      // labelFilePath
      // 
      this.labelFilePath.AutoSize = true;
      this.labelFilePath.Location = new System.Drawing.Point(13, 39);
      this.labelFilePath.Name = "labelFilePath";
      this.labelFilePath.Size = new System.Drawing.Size(12, 13);
      this.labelFilePath.TabIndex = 6;
      this.labelFilePath.Text = "/";
      // 
      // labelMessage
      // 
      this.labelMessage.AutoSize = true;
      this.labelMessage.Location = new System.Drawing.Point(17, 147);
      this.labelMessage.Name = "labelMessage";
      this.labelMessage.Size = new System.Drawing.Size(35, 13);
      this.labelMessage.TabIndex = 7;
      this.labelMessage.Text = "label1";
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(13, 85);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(12, 13);
      this.label1.TabIndex = 9;
      this.label1.Text = "/";
      // 
      // btnMnoFile
      // 
      this.btnMnoFile.Location = new System.Drawing.Point(12, 58);
      this.btnMnoFile.Name = "btnMnoFile";
      this.btnMnoFile.Size = new System.Drawing.Size(195, 23);
      this.btnMnoFile.TabIndex = 8;
      this.btnMnoFile.Text = "Find MobileOperator file";
      this.btnMnoFile.UseVisualStyleBackColor = true;
      this.btnMnoFile.Click += new System.EventHandler(this.btnMnoFile_Click);
      // 
      // Form1
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(425, 210);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.btnMnoFile);
      this.Controls.Add(this.labelMessage);
      this.Controls.Add(this.labelFilePath);
      this.Controls.Add(this.btnFindFile);
      this.Controls.Add(this.btnInsert);
      this.Name = "Form1";
      this.Text = "IP Inserter";
      this.Load += new System.EventHandler(this.Form1_Load);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion
    private System.Windows.Forms.Button btnInsert;
    private System.Windows.Forms.Button btnFindFile;
    private System.Windows.Forms.Label labelFilePath;
    private System.Windows.Forms.Label labelMessage;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Button btnMnoFile;
  }
}

