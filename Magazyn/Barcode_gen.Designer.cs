namespace Magazyn
{
    partial class Barcode_gen
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
            this.label2 = new System.Windows.Forms.Label();
            this.barcodeButton = new System.Windows.Forms.Button();
            this.picBarcode = new System.Windows.Forms.PictureBox();
            this.txtBarcode = new System.Windows.Forms.TextBox();
            this.saveButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.picBarcode)).BeginInit();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 132);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(165, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "Wygeneruj kod kreskowy";
            // 
            // barcodeButton
            // 
            this.barcodeButton.Location = new System.Drawing.Point(369, 128);
            this.barcodeButton.Name = "barcodeButton";
            this.barcodeButton.Size = new System.Drawing.Size(90, 30);
            this.barcodeButton.TabIndex = 5;
            this.barcodeButton.Text = "Wygeneruj";
            this.barcodeButton.UseVisualStyleBackColor = true;
            this.barcodeButton.Click += new System.EventHandler(this.barcodeButton_Click);
            // 
            // picBarcode
            // 
            this.picBarcode.BackColor = System.Drawing.Color.White;
            this.picBarcode.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.picBarcode.Location = new System.Drawing.Point(40, 189);
            this.picBarcode.Name = "picBarcode";
            this.picBarcode.Size = new System.Drawing.Size(707, 275);
            this.picBarcode.TabIndex = 6;
            this.picBarcode.TabStop = false;
            // 
            // txtBarcode
            // 
            this.txtBarcode.Location = new System.Drawing.Point(188, 132);
            this.txtBarcode.Name = "txtBarcode";
            this.txtBarcode.Size = new System.Drawing.Size(156, 22);
            this.txtBarcode.TabIndex = 7;
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(657, 470);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(90, 30);
            this.saveButton.TabIndex = 8;
            this.saveButton.Text = "Zapisz";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // Barcode_gen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(921, 533);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.txtBarcode);
            this.Controls.Add(this.picBarcode);
            this.Controls.Add(this.barcodeButton);
            this.Controls.Add(this.label2);
            this.Name = "Barcode_gen";
            this.Text = "Wygeneruj kod kreskowy";
            ((System.ComponentModel.ISupportInitialize)(this.picBarcode)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button barcodeButton;
        private System.Windows.Forms.PictureBox picBarcode;
        private System.Windows.Forms.TextBox txtBarcode;
        private System.Windows.Forms.Button saveButton;
    }
}