namespace GameClient
{
    partial class MainWindow
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
            this.tabsControl = new System.Windows.Forms.TabControl();
            this.gameRoomsTab = new System.Windows.Forms.TabPage();
            this.createRoomButton = new System.Windows.Forms.Button();
            this.joinRoomButton = new System.Windows.Forms.Button();
            this.gameRoomsListbox = new System.Windows.Forms.ListBox();
            this.profileTab = new System.Windows.Forms.TabPage();
            this.registerButton = new System.Windows.Forms.Button();
            this.loginButton = new System.Windows.Forms.Button();
            this.usernameText = new System.Windows.Forms.Label();
            this.logoutButton = new System.Windows.Forms.Button();
            this.tabsControl.SuspendLayout();
            this.gameRoomsTab.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabsControl
            // 
            this.tabsControl.Controls.Add(this.gameRoomsTab);
            this.tabsControl.Controls.Add(this.profileTab);
            this.tabsControl.Location = new System.Drawing.Point(7, 9);
            this.tabsControl.Name = "tabsControl";
            this.tabsControl.SelectedIndex = 0;
            this.tabsControl.Size = new System.Drawing.Size(558, 435);
            this.tabsControl.TabIndex = 9;
            // 
            // gameRoomsTab
            // 
            this.gameRoomsTab.Controls.Add(this.createRoomButton);
            this.gameRoomsTab.Controls.Add(this.joinRoomButton);
            this.gameRoomsTab.Controls.Add(this.gameRoomsListbox);
            this.gameRoomsTab.Location = new System.Drawing.Point(4, 22);
            this.gameRoomsTab.Name = "gameRoomsTab";
            this.gameRoomsTab.Padding = new System.Windows.Forms.Padding(3);
            this.gameRoomsTab.Size = new System.Drawing.Size(550, 409);
            this.gameRoomsTab.TabIndex = 0;
            this.gameRoomsTab.Text = "Game rooms";
            this.gameRoomsTab.UseVisualStyleBackColor = true;
            // 
            // createRoomButton
            // 
            this.createRoomButton.Location = new System.Drawing.Point(370, 302);
            this.createRoomButton.Name = "createRoomButton";
            this.createRoomButton.Size = new System.Drawing.Size(95, 23);
            this.createRoomButton.TabIndex = 2;
            this.createRoomButton.Text = "Create room";
            this.createRoomButton.UseVisualStyleBackColor = true;
            this.createRoomButton.Click += new System.EventHandler(this.createRoomButton_Click);
            // 
            // joinRoomButton
            // 
            this.joinRoomButton.Location = new System.Drawing.Point(30, 302);
            this.joinRoomButton.Name = "joinRoomButton";
            this.joinRoomButton.Size = new System.Drawing.Size(92, 23);
            this.joinRoomButton.TabIndex = 1;
            this.joinRoomButton.Text = "Join room";
            this.joinRoomButton.UseVisualStyleBackColor = true;
            this.joinRoomButton.Click += new System.EventHandler(this.joinRoomButton_Click);
            // 
            // gameRoomsListbox
            // 
            this.gameRoomsListbox.FormattingEnabled = true;
            this.gameRoomsListbox.Location = new System.Drawing.Point(30, 6);
            this.gameRoomsListbox.Name = "gameRoomsListbox";
            this.gameRoomsListbox.Size = new System.Drawing.Size(435, 290);
            this.gameRoomsListbox.TabIndex = 0;
            // 
            // profileTab
            // 
            this.profileTab.Location = new System.Drawing.Point(4, 22);
            this.profileTab.Name = "profileTab";
            this.profileTab.Padding = new System.Windows.Forms.Padding(3);
            this.profileTab.Size = new System.Drawing.Size(550, 409);
            this.profileTab.TabIndex = 1;
            this.profileTab.Text = "Profile";
            this.profileTab.UseVisualStyleBackColor = true;
            // 
            // registerButton
            // 
            this.registerButton.Location = new System.Drawing.Point(571, 9);
            this.registerButton.Name = "registerButton";
            this.registerButton.Size = new System.Drawing.Size(88, 23);
            this.registerButton.TabIndex = 8;
            this.registerButton.Text = "Register";
            this.registerButton.UseVisualStyleBackColor = true;
            this.registerButton.Click += new System.EventHandler(this.registerButton_Click);
            // 
            // loginButton
            // 
            this.loginButton.Location = new System.Drawing.Point(665, 9);
            this.loginButton.Name = "loginButton";
            this.loginButton.Size = new System.Drawing.Size(85, 23);
            this.loginButton.TabIndex = 7;
            this.loginButton.Text = "Login";
            this.loginButton.UseVisualStyleBackColor = true;
            this.loginButton.Click += new System.EventHandler(this.loginButton_Click);
            // 
            // usernameText
            // 
            this.usernameText.AutoSize = true;
            this.usernameText.Location = new System.Drawing.Point(659, 9);
            this.usernameText.Name = "usernameText";
            this.usernameText.Size = new System.Drawing.Size(0, 13);
            this.usernameText.TabIndex = 10;
            this.usernameText.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // logoutButton
            // 
            this.logoutButton.Location = new System.Drawing.Point(670, 417);
            this.logoutButton.Name = "logoutButton";
            this.logoutButton.Size = new System.Drawing.Size(75, 23);
            this.logoutButton.TabIndex = 11;
            this.logoutButton.Text = "Logout";
            this.logoutButton.UseVisualStyleBackColor = true;
            this.logoutButton.Click += new System.EventHandler(this.logoutButton_Click);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(757, 453);
            this.Controls.Add(this.logoutButton);
            this.Controls.Add(this.usernameText);
            this.Controls.Add(this.tabsControl);
            this.Controls.Add(this.registerButton);
            this.Controls.Add(this.loginButton);
            this.Name = "MainWindow";
            this.Text = "MainWindow";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainWindow_FormClosed);
            this.Load += new System.EventHandler(this.MainWindow_Load);
            this.tabsControl.ResumeLayout(false);
            this.gameRoomsTab.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabsControl;
        private System.Windows.Forms.TabPage gameRoomsTab;
        private System.Windows.Forms.Button createRoomButton;
        private System.Windows.Forms.Button joinRoomButton;
        private System.Windows.Forms.ListBox gameRoomsListbox;
        private System.Windows.Forms.TabPage profileTab;
        private System.Windows.Forms.Button registerButton;
        private System.Windows.Forms.Button loginButton;
        private System.Windows.Forms.Label usernameText;
        private System.Windows.Forms.Button logoutButton;
    }
}