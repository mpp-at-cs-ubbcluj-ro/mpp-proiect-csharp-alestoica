using System.ComponentModel;

namespace csharp_project
{
    partial class RegisterForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

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
            this.title = new System.Windows.Forms.Label();
            this.textBoxFirstName = new System.Windows.Forms.TextBox();
            this.textBoxLastName = new System.Windows.Forms.TextBox();
            this.labelFirstName = new System.Windows.Forms.Label();
            this.labelLastName = new System.Windows.Forms.Label();
            this.labelAge = new System.Windows.Forms.Label();
            this.labelEvent = new System.Windows.Forms.Label();
            this.comboBoxAge = new System.Windows.Forms.ComboBox();
            this.comboBoxEvent = new System.Windows.Forms.ComboBox();
            this.buttonRegister = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // title
            // 
            this.title.Location = new System.Drawing.Point(333, 83);
            this.title.Name = "title";
            this.title.Size = new System.Drawing.Size(86, 23);
            this.title.TabIndex = 0;
            this.title.Text = "Register";
            // 
            // textBoxFirstName
            // 
            this.textBoxFirstName.Location = new System.Drawing.Point(381, 156);
            this.textBoxFirstName.Name = "textBoxFirstName";
            this.textBoxFirstName.Size = new System.Drawing.Size(193, 26);
            this.textBoxFirstName.TabIndex = 1;
            // 
            // textBoxLastName
            // 
            this.textBoxLastName.Location = new System.Drawing.Point(381, 198);
            this.textBoxLastName.Name = "textBoxLastName";
            this.textBoxLastName.Size = new System.Drawing.Size(193, 26);
            this.textBoxLastName.TabIndex = 2;
            // 
            // labelFirstName
            // 
            this.labelFirstName.Location = new System.Drawing.Point(226, 159);
            this.labelFirstName.Name = "labelFirstName";
            this.labelFirstName.Size = new System.Drawing.Size(100, 23);
            this.labelFirstName.TabIndex = 3;
            this.labelFirstName.Text = "first name: ";
            // 
            // labelLastName
            // 
            this.labelLastName.Location = new System.Drawing.Point(226, 201);
            this.labelLastName.Name = "labelLastName";
            this.labelLastName.Size = new System.Drawing.Size(100, 23);
            this.labelLastName.TabIndex = 4;
            this.labelLastName.Text = "last name: ";
            // 
            // labelAge
            // 
            this.labelAge.Location = new System.Drawing.Point(226, 242);
            this.labelAge.Name = "labelAge";
            this.labelAge.Size = new System.Drawing.Size(100, 23);
            this.labelAge.TabIndex = 5;
            this.labelAge.Text = "age: ";
            // 
            // labelEvent
            // 
            this.labelEvent.Location = new System.Drawing.Point(226, 281);
            this.labelEvent.Name = "labelEvent";
            this.labelEvent.Size = new System.Drawing.Size(100, 23);
            this.labelEvent.TabIndex = 6;
            this.labelEvent.Text = "event: ";
            // 
            // comboBoxAge
            // 
            this.comboBoxAge.FormattingEnabled = true;
            this.comboBoxAge.Location = new System.Drawing.Point(381, 239);
            this.comboBoxAge.Name = "comboBoxAge";
            this.comboBoxAge.Size = new System.Drawing.Size(193, 28);
            this.comboBoxAge.TabIndex = 7;
            // 
            // comboBoxEvent
            // 
            this.comboBoxEvent.FormattingEnabled = true;
            this.comboBoxEvent.Location = new System.Drawing.Point(381, 278);
            this.comboBoxEvent.Name = "comboBoxEvent";
            this.comboBoxEvent.Size = new System.Drawing.Size(193, 28);
            this.comboBoxEvent.TabIndex = 8;
            // 
            // buttonRegister
            // 
            this.buttonRegister.Location = new System.Drawing.Point(333, 370);
            this.buttonRegister.Name = "buttonRegister";
            this.buttonRegister.Size = new System.Drawing.Size(86, 40);
            this.buttonRegister.TabIndex = 9;
            this.buttonRegister.Text = "Register";
            this.buttonRegister.UseVisualStyleBackColor = true;
            this.buttonRegister.Click += new System.EventHandler(this.HandleRegister);
            // 
            // RegisterForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.buttonRegister);
            this.Controls.Add(this.comboBoxEvent);
            this.Controls.Add(this.comboBoxAge);
            this.Controls.Add(this.labelEvent);
            this.Controls.Add(this.labelAge);
            this.Controls.Add(this.labelLastName);
            this.Controls.Add(this.labelFirstName);
            this.Controls.Add(this.textBoxLastName);
            this.Controls.Add(this.textBoxFirstName);
            this.Controls.Add(this.title);
            this.Name = "RegisterForm";
            this.Text = "Register Form";
            this.Load += new System.EventHandler(this.LoadData);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Label labelAge;
        private System.Windows.Forms.Label labelEvent;
        private System.Windows.Forms.ComboBox comboBoxAge;
        private System.Windows.Forms.ComboBox comboBoxEvent;
        private System.Windows.Forms.Button buttonRegister;

        private System.Windows.Forms.Label title;
        private System.Windows.Forms.TextBox textBoxFirstName;
        private System.Windows.Forms.TextBox textBoxLastName;
        private System.Windows.Forms.Label labelFirstName;
        private System.Windows.Forms.Label labelLastName;

        #endregion
    }
}