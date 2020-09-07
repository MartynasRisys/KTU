namespace GameClient
{
    partial class GameRoomWindow
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
            this.startButton = new System.Windows.Forms.Button();
            this.exitButton = new System.Windows.Forms.Button();
            this.gameRoomText = new System.Windows.Forms.Label();
            this.gameRoomHeading = new System.Windows.Forms.Label();
            this.hostLabel = new System.Windows.Forms.Label();
            this.hostNameField = new System.Windows.Forms.Label();
            this.joinerLabel = new System.Windows.Forms.Label();
            this.joinerTextField = new System.Windows.Forms.Label();
            this.startingGoldLabel = new System.Windows.Forms.Label();
            this.startingGoldTextField = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.mapSizeTextField = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // startButton
            // 
            this.startButton.Location = new System.Drawing.Point(387, 345);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(75, 23);
            this.startButton.TabIndex = 0;
            this.startButton.Text = "Start";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.startButton_Click);
            // 
            // exitButton
            // 
            this.exitButton.Location = new System.Drawing.Point(21, 25);
            this.exitButton.Name = "exitButton";
            this.exitButton.Size = new System.Drawing.Size(75, 23);
            this.exitButton.TabIndex = 1;
            this.exitButton.Text = "Exit";
            this.exitButton.UseVisualStyleBackColor = true;
            this.exitButton.Click += new System.EventHandler(this.exitButton_Click);
            // 
            // gameRoomText
            // 
            this.gameRoomText.AutoSize = true;
            this.gameRoomText.Font = new System.Drawing.Font("Microsoft Sans Serif", 25.25F);
            this.gameRoomText.Location = new System.Drawing.Point(328, 25);
            this.gameRoomText.Name = "gameRoomText";
            this.gameRoomText.Size = new System.Drawing.Size(195, 39);
            this.gameRoomText.TabIndex = 2;
            this.gameRoomText.Text = "Game room";
            // 
            // gameRoomHeading
            // 
            this.gameRoomHeading.AutoSize = true;
            this.gameRoomHeading.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.25F);
            this.gameRoomHeading.Location = new System.Drawing.Point(414, 64);
            this.gameRoomHeading.Name = "gameRoomHeading";
            this.gameRoomHeading.Size = new System.Drawing.Size(0, 25);
            this.gameRoomHeading.TabIndex = 3;
            this.gameRoomHeading.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // hostLabel
            // 
            this.hostLabel.AutoSize = true;
            this.hostLabel.Location = new System.Drawing.Point(18, 110);
            this.hostLabel.Name = "hostLabel";
            this.hostLabel.Size = new System.Drawing.Size(29, 13);
            this.hostLabel.TabIndex = 4;
            this.hostLabel.Text = "Host";
            // 
            // hostNameField
            // 
            this.hostNameField.AutoSize = true;
            this.hostNameField.Location = new System.Drawing.Point(18, 123);
            this.hostNameField.Name = "hostNameField";
            this.hostNameField.Size = new System.Drawing.Size(0, 13);
            this.hostNameField.TabIndex = 5;
            // 
            // joinerLabel
            // 
            this.joinerLabel.AutoSize = true;
            this.joinerLabel.Location = new System.Drawing.Point(18, 152);
            this.joinerLabel.Name = "joinerLabel";
            this.joinerLabel.Size = new System.Drawing.Size(35, 13);
            this.joinerLabel.TabIndex = 6;
            this.joinerLabel.Text = "Joiner";
            // 
            // joinerTextField
            // 
            this.joinerTextField.AutoSize = true;
            this.joinerTextField.Location = new System.Drawing.Point(18, 165);
            this.joinerTextField.Name = "joinerTextField";
            this.joinerTextField.Size = new System.Drawing.Size(0, 13);
            this.joinerTextField.TabIndex = 7;
            // 
            // startingGoldLabel
            // 
            this.startingGoldLabel.AutoSize = true;
            this.startingGoldLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.startingGoldLabel.Location = new System.Drawing.Point(359, 109);
            this.startingGoldLabel.Name = "startingGoldLabel";
            this.startingGoldLabel.Size = new System.Drawing.Size(103, 20);
            this.startingGoldLabel.TabIndex = 8;
            this.startingGoldLabel.Text = "Starting gold";
            // 
            // startingGoldTextField
            // 
            this.startingGoldTextField.AutoSize = true;
            this.startingGoldTextField.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
            this.startingGoldTextField.Location = new System.Drawing.Point(360, 129);
            this.startingGoldTextField.Name = "startingGoldTextField";
            this.startingGoldTextField.Size = new System.Drawing.Size(0, 17);
            this.startingGoldTextField.TabIndex = 9;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.label2.Location = new System.Drawing.Point(359, 165);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 20);
            this.label2.TabIndex = 10;
            this.label2.Text = "Map size";
            // 
            // mapSizeTextField
            // 
            this.mapSizeTextField.AutoSize = true;
            this.mapSizeTextField.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
            this.mapSizeTextField.Location = new System.Drawing.Point(360, 185);
            this.mapSizeTextField.Name = "mapSizeTextField";
            this.mapSizeTextField.Size = new System.Drawing.Size(0, 17);
            this.mapSizeTextField.TabIndex = 11;
            // 
            // GameRoomWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.mapSizeTextField);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.startingGoldTextField);
            this.Controls.Add(this.startingGoldLabel);
            this.Controls.Add(this.joinerTextField);
            this.Controls.Add(this.joinerLabel);
            this.Controls.Add(this.hostNameField);
            this.Controls.Add(this.hostLabel);
            this.Controls.Add(this.gameRoomHeading);
            this.Controls.Add(this.gameRoomText);
            this.Controls.Add(this.exitButton);
            this.Controls.Add(this.startButton);
            this.Name = "GameRoomWindow";
            this.Text = "GameRoomWindow";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.GameRoomWindow_FormClosed);
            this.Load += new System.EventHandler(this.GameRoomWindow_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.Button exitButton;
        private System.Windows.Forms.Label gameRoomText;
        private System.Windows.Forms.Label gameRoomHeading;
        private System.Windows.Forms.Label hostLabel;
        private System.Windows.Forms.Label hostNameField;
        private System.Windows.Forms.Label joinerLabel;
        private System.Windows.Forms.Label joinerTextField;
        private System.Windows.Forms.Label startingGoldLabel;
        private System.Windows.Forms.Label startingGoldTextField;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label mapSizeTextField;
    }
}