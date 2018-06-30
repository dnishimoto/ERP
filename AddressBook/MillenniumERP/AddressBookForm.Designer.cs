﻿namespace MillenniumERP
{
    partial class AddressBookForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtSearchName = new System.Windows.Forms.TextBox();
            this.cmdSearch = new System.Windows.Forms.Button();
            this.dataGridViewAddressBook = new System.Windows.Forms.DataGridView();
            this.AddressId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtBillState = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cmdSelect = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewAddressBook)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(30, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Name";
            // 
            // txtSearchName
            // 
            this.txtSearchName.Location = new System.Drawing.Point(110, 47);
            this.txtSearchName.Name = "txtSearchName";
            this.txtSearchName.Size = new System.Drawing.Size(217, 22);
            this.txtSearchName.TabIndex = 1;
            // 
            // cmdSearch
            // 
            this.cmdSearch.Location = new System.Drawing.Point(91, 3);
            this.cmdSearch.Name = "cmdSearch";
            this.cmdSearch.Size = new System.Drawing.Size(75, 25);
            this.cmdSearch.TabIndex = 2;
            this.cmdSearch.Text = "Search";
            this.cmdSearch.UseVisualStyleBackColor = true;
            this.cmdSearch.Click += new System.EventHandler(this.cmdSearch_Click);
            // 
            // dataGridViewAddressBook
            // 
            this.dataGridViewAddressBook.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewAddressBook.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.AddressId,
            this.txtName,
            this.txtBillState});
            this.dataGridViewAddressBook.Location = new System.Drawing.Point(22, 93);
            this.dataGridViewAddressBook.Name = "dataGridViewAddressBook";
            this.dataGridViewAddressBook.RowTemplate.Height = 24;
            this.dataGridViewAddressBook.Size = new System.Drawing.Size(679, 135);
            this.dataGridViewAddressBook.TabIndex = 3;
            // 
            // AddressId
            // 
            this.AddressId.DataPropertyName = "AddressId";
            this.AddressId.HeaderText = "Key";
            this.AddressId.Name = "AddressId";
            // 
            // txtName
            // 
            this.txtName.DataPropertyName = "Name";
            this.txtName.HeaderText = "Name";
            this.txtName.Name = "txtName";
            this.txtName.ReadOnly = true;
            // 
            // txtBillState
            // 
            this.txtBillState.DataPropertyName = "BillState";
            this.txtBillState.HeaderText = "State";
            this.txtBillState.Name = "txtBillState";
            this.txtBillState.ReadOnly = true;
            // 
            // cmdSelect
            // 
            this.cmdSelect.Location = new System.Drawing.Point(182, 3);
            this.cmdSelect.Name = "cmdSelect";
            this.cmdSelect.Size = new System.Drawing.Size(75, 25);
            this.cmdSelect.TabIndex = 4;
            this.cmdSelect.Text = "Select";
            this.cmdSelect.UseVisualStyleBackColor = true;
            this.cmdSelect.Click += new System.EventHandler(this.cmdSelect_Click);
            // 
            // AddressBookForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(723, 321);
            this.Controls.Add(this.cmdSelect);
            this.Controls.Add(this.dataGridViewAddressBook);
            this.Controls.Add(this.cmdSearch);
            this.Controls.Add(this.txtSearchName);
            this.Controls.Add(this.label1);
            this.Name = "AddressBookForm";
            this.Text = "Address Book";
            this.Load += new System.EventHandler(this.AddressBookForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewAddressBook)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtSearchName;
        private System.Windows.Forms.Button cmdSearch;
        private System.Windows.Forms.DataGridView dataGridViewAddressBook;
        private System.Windows.Forms.Button cmdSelect;
        private System.Windows.Forms.DataGridViewTextBoxColumn AddressId;
        private System.Windows.Forms.DataGridViewTextBoxColumn txtName;
        private System.Windows.Forms.DataGridViewTextBoxColumn txtBillState;
    }
}

