namespace GameClient
{
    partial class CreateGameRoomWindow
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
            this.createRoomButton = new System.Windows.Forms.Button();
            this.backButton = new System.Windows.Forms.Button();
            this.createRoomHeading = new System.Windows.Forms.Label();
            this.roomNameField = new System.Windows.Forms.TextBox();
            this.roomNameLabel = new System.Windows.Forms.Label();
            this.smallMapRadioButton = new System.Windows.Forms.RadioButton();
            this.startingMoneyField = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.mediumMapRadioButton = new System.Windows.Forms.RadioButton();
            this.largeMapRadioButton = new System.Windows.Forms.RadioButton();
            this.mapSizeGroupBox = new System.Windows.Forms.GroupBox();
            this.mapSizeGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // createRoomButton
            // 
            this.createRoomButton.Location = new System.Drawing.Point(328, 339);
            this.createRoomButton.Name = "createRoomButton";
            this.createRoomButton.Size = new System.Drawing.Size(75, 23);
            this.createRoomButton.TabIndex = 0;
            this.createRoomButton.Text = "Create";
            this.createRoomButton.UseVisualStyleBackColor = true;
            this.createRoomButton.Click += new System.EventHandler(this.createRoomButton_Click);
            // 
            // backButton
            // 
            this.backButton.Location = new System.Drawing.Point(23, 23);
            this.backButton.Name = "backButton";
            this.backButton.Size = new System.Drawing.Size(75, 23);
            this.backButton.TabIndex = 1;
            this.backButton.Text = "Back";
            this.backButton.UseVisualStyleBackColor = true;
            this.backButton.Click += new System.EventHandler(this.backButton_Click);
            // 
            // createRoomHeading
            // 
            this.createRoomHeading.AutoSize = true;
            this.createRoomHeading.Font = new System.Drawing.Font("Microsoft Sans Serif", 25.25F);
            this.createRoomHeading.Location = new System.Drawing.Point(261, 23);
            this.createRoomHeading.Name = "createRoomHeading";
            this.createRoomHeading.Size = new System.Drawing.Size(205, 39);
            this.createRoomHeading.TabIndex = 2;
            this.createRoomHeading.Text = "Create room";
            // 
            // roomNameField
            // 
            this.roomNameField.Location = new System.Drawing.Point(277, 125);
            this.roomNameField.Name = "roomNameField";
            this.roomNameField.Size = new System.Drawing.Size(178, 20);
            this.roomNameField.TabIndex = 3;
            // 
            // roomNameLabel
            // 
            this.roomNameLabel.AutoSize = true;
            this.roomNameLabel.Location = new System.Drawing.Point(274, 109);
            this.roomNameLabel.Name = "roomNameLabel";
            this.roomNameLabel.Size = new System.Drawing.Size(64, 13);
            this.roomNameLabel.TabIndex = 4;
            this.roomNameLabel.Text = "Room name";
            // 
            // smallMapRadioButton
            // 
            this.smallMapRadioButton.AutoSize = true;
            this.smallMapRadioButton.Checked = true;
            this.smallMapRadioButton.Location = new System.Drawing.Point(6, 19);
            this.smallMapRadioButton.Name = "smallMapRadioButton";
            this.smallMapRadioButton.Size = new System.Drawing.Size(50, 17);
            this.smallMapRadioButton.TabIndex = 5;
            this.smallMapRadioButton.TabStop = true;
            this.smallMapRadioButton.Text = "Small";
            this.smallMapRadioButton.UseVisualStyleBackColor = true;
            // 
            // startingMoneyField
            // 
            this.startingMoneyField.Location = new System.Drawing.Point(277, 188);
            this.startingMoneyField.Name = "startingMoneyField";
            this.startingMoneyField.Size = new System.Drawing.Size(178, 20);
            this.startingMoneyField.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(274, 172);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Starting money";
            // 
            // mediumMapRadioButton
            // 
            this.mediumMapRadioButton.AutoSize = true;
            this.mediumMapRadioButton.Location = new System.Drawing.Point(6, 42);
            this.mediumMapRadioButton.Name = "mediumMapRadioButton";
            this.mediumMapRadioButton.Size = new System.Drawing.Size(62, 17);
            this.mediumMapRadioButton.TabIndex = 9;
            this.mediumMapRadioButton.TabStop = true;
            this.mediumMapRadioButton.Text = "Medium";
            this.mediumMapRadioButton.UseVisualStyleBackColor = true;
            // 
            // largeMapRadioButton
            // 
            this.largeMapRadioButton.AutoSize = true;
            this.largeMapRadioButton.Location = new System.Drawing.Point(6, 65);
            this.largeMapRadioButton.Name = "largeMapRadioButton";
            this.largeMapRadioButton.Size = new System.Drawing.Size(52, 17);
            this.largeMapRadioButton.TabIndex = 10;
            this.largeMapRadioButton.TabStop = true;
            this.largeMapRadioButton.Text = "Large";
            this.largeMapRadioButton.UseVisualStyleBackColor = true;
            // 
            // mapSizeGroupBox
            // 
            this.mapSizeGroupBox.Controls.Add(this.smallMapRadioButton);
            this.mapSizeGroupBox.Controls.Add(this.largeMapRadioButton);
            this.mapSizeGroupBox.Controls.Add(this.mediumMapRadioButton);
            this.mapSizeGroupBox.Location = new System.Drawing.Point(277, 235);
            this.mapSizeGroupBox.Name = "mapSizeGroupBox";
            this.mapSizeGroupBox.Size = new System.Drawing.Size(166, 85);
            this.mapSizeGroupBox.TabIndex = 11;
            this.mapSizeGroupBox.TabStop = false;
            this.mapSizeGroupBox.Text = "Map size";
            // 
            // CreateGameRoomWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(719, 450);
            this.Controls.Add(this.mapSizeGroupBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.startingMoneyField);
            this.Controls.Add(this.roomNameLabel);
            this.Controls.Add(this.roomNameField);
            this.Controls.Add(this.createRoomHeading);
            this.Controls.Add(this.backButton);
            this.Controls.Add(this.createRoomButton);
            this.Name = "CreateGameRoomWindow";
            this.Text = "CreateGameRoomWindow";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.CreateGameRoomWindow_FormClosed);
            this.mapSizeGroupBox.ResumeLayout(false);
            this.mapSizeGroupBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button createRoomButton;
        private System.Windows.Forms.Button backButton;
        private System.Windows.Forms.Label createRoomHeading;
        private System.Windows.Forms.TextBox roomNameField;
        private System.Windows.Forms.Label roomNameLabel;
        private System.Windows.Forms.RadioButton smallMapRadioButton;
        private System.Windows.Forms.TextBox startingMoneyField;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton mediumMapRadioButton;
        private System.Windows.Forms.RadioButton largeMapRadioButton;
        private System.Windows.Forms.GroupBox mapSizeGroupBox;
    }
}