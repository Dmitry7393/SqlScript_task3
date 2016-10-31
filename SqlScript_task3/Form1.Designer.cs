namespace SqlScript_task3
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
            this.label_state = new System.Windows.Forms.Label();
            this.btnAddTables = new System.Windows.Forms.Button();
            this.label_state_table = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label_state
            // 
            this.label_state.AutoSize = true;
            this.label_state.Location = new System.Drawing.Point(12, 9);
            this.label_state.Name = "label_state";
            this.label_state.Size = new System.Drawing.Size(35, 13);
            this.label_state.TabIndex = 2;
            this.label_state.Text = "label1";
            // 
            // btnAddTables
            // 
            this.btnAddTables.Location = new System.Drawing.Point(15, 79);
            this.btnAddTables.Name = "btnAddTables";
            this.btnAddTables.Size = new System.Drawing.Size(118, 37);
            this.btnAddTables.TabIndex = 3;
            this.btnAddTables.Text = "Add tables";
            this.btnAddTables.UseVisualStyleBackColor = true;
            this.btnAddTables.Click += new System.EventHandler(this.btnAddTables_Click);
            // 
            // label_state_table
            // 
            this.label_state_table.AutoSize = true;
            this.label_state_table.Location = new System.Drawing.Point(12, 35);
            this.label_state_table.Name = "label_state_table";
            this.label_state_table.Size = new System.Drawing.Size(0, 13);
            this.label_state_table.TabIndex = 4;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.label_state_table);
            this.Controls.Add(this.btnAddTables);
            this.Controls.Add(this.label_state);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label_state;
        private System.Windows.Forms.Button btnAddTables;
        private System.Windows.Forms.Label label_state_table;
    }
}

