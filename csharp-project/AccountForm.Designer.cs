using System.ComponentModel;

namespace csharp_project
{
    partial class AccountForm
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
            this.dataGridViewEvents = new System.Windows.Forms.DataGridView();
            this.dataGridViewParticipants = new System.Windows.Forms.DataGridView();
            this.searchButton = new System.Windows.Forms.Button();
            this.registerButton = new System.Windows.Forms.Button();
            this.title = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewEvents)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewParticipants)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewEvents
            // 
            this.dataGridViewEvents.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewEvents.Location = new System.Drawing.Point(12, 126);
            this.dataGridViewEvents.Name = "dataGridViewEvents";
            this.dataGridViewEvents.RowTemplate.Height = 28;
            this.dataGridViewEvents.Size = new System.Drawing.Size(384, 224);
            this.dataGridViewEvents.TabIndex = 0;
            // 
            // dataGridViewParticipants
            // 
            this.dataGridViewParticipants.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewParticipants.Location = new System.Drawing.Point(404, 126);
            this.dataGridViewParticipants.Name = "dataGridViewParticipants";
            this.dataGridViewParticipants.RowTemplate.Height = 28;
            this.dataGridViewParticipants.Size = new System.Drawing.Size(384, 224);
            this.dataGridViewParticipants.TabIndex = 1;
            // 
            // searchButton
            // 
            this.searchButton.Location = new System.Drawing.Point(161, 386);
            this.searchButton.Name = "searchButton";
            this.searchButton.Size = new System.Drawing.Size(75, 44);
            this.searchButton.TabIndex = 2;
            this.searchButton.Text = "Search";
            this.searchButton.UseVisualStyleBackColor = true;
            this.searchButton.Click += new System.EventHandler(this.HandleSearch);
            // 
            // registerButton
            // 
            this.registerButton.Location = new System.Drawing.Point(547, 386);
            this.registerButton.Name = "registerButton";
            this.registerButton.Size = new System.Drawing.Size(96, 44);
            this.registerButton.TabIndex = 3;
            this.registerButton.Text = "Register";
            this.registerButton.UseVisualStyleBackColor = true;
            this.registerButton.Click += new System.EventHandler(this.HandleRegister);
            // 
            // title
            // 
            this.title.Location = new System.Drawing.Point(358, 65);
            this.title.Name = "title";
            this.title.Size = new System.Drawing.Size(84, 23);
            this.title.TabIndex = 4;
            this.title.Text = "Welcome!";
            // 
            // AccountForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.title);
            this.Controls.Add(this.registerButton);
            this.Controls.Add(this.searchButton);
            this.Controls.Add(this.dataGridViewParticipants);
            this.Controls.Add(this.dataGridViewEvents);
            this.Name = "AccountForm";
            this.Text = "Account Form";
            this.Load += new System.EventHandler(this.LoadDataGridViews);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewEvents)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewParticipants)).EndInit();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Label title;

        private System.Windows.Forms.Button searchButton;
        private System.Windows.Forms.Button registerButton;

        private System.Windows.Forms.DataGridView dataGridViewParticipants;

        private System.Windows.Forms.DataGridView dataGridViewEvents;

        #endregion
    }
}