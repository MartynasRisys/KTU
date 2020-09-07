namespace GameClient
{
    partial class CreateBattleUnitWindow
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
            this.battleUnitTypeComboBox = new System.Windows.Forms.ComboBox();
            this.finishButton = new System.Windows.Forms.Button();
            this.addButton = new System.Windows.Forms.Button();
            this.battleUnitsListView = new System.Windows.Forms.ListView();
            this.mapLayout = new System.Windows.Forms.PictureBox();
            this.specialAbilityTitle = new System.Windows.Forms.Label();
            this.specialAbilityText = new System.Windows.Forms.Label();
            this.specialAbilityButton = new System.Windows.Forms.Button();
            this.turnLabel = new System.Windows.Forms.Label();
            this.playerTurn = new System.Windows.Forms.Label();
            this.moveButton = new System.Windows.Forms.Button();
            this.attackButton = new System.Windows.Forms.Button();
            this.confirmActionButton = new System.Windows.Forms.Button();
            this.cancelActionButton = new System.Windows.Forms.Button();
            this.informationalLabel = new System.Windows.Forms.Label();
            this.stateLabel = new System.Windows.Forms.Label();
            this.endTurnButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.mapLayout)).BeginInit();
            this.SuspendLayout();
            // 
            // battleUnitTypeComboBox
            // 
            this.battleUnitTypeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.battleUnitTypeComboBox.FormattingEnabled = true;
            this.battleUnitTypeComboBox.Items.AddRange(new object[] {
            "Square",
            "Triangle",
            "Circle"});
            this.battleUnitTypeComboBox.Location = new System.Drawing.Point(524, 620);
            this.battleUnitTypeComboBox.Name = "battleUnitTypeComboBox";
            this.battleUnitTypeComboBox.Size = new System.Drawing.Size(121, 21);
            this.battleUnitTypeComboBox.TabIndex = 1;
            this.battleUnitTypeComboBox.SelectedIndexChanged += new System.EventHandler(this.battleUnitTypeComboBox_SelectedIndexChanged);
            // 
            // finishButton
            // 
            this.finishButton.Location = new System.Drawing.Point(753, 618);
            this.finishButton.Name = "finishButton";
            this.finishButton.Size = new System.Drawing.Size(75, 23);
            this.finishButton.TabIndex = 2;
            this.finishButton.Text = "Finish";
            this.finishButton.UseVisualStyleBackColor = true;
            this.finishButton.Click += new System.EventHandler(this.finishButton_Click);
            // 
            // addButton
            // 
            this.addButton.Location = new System.Drawing.Point(651, 620);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(75, 21);
            this.addButton.TabIndex = 0;
            this.addButton.Text = "Add";
            this.addButton.UseVisualStyleBackColor = true;
            this.addButton.Click += new System.EventHandler(this.addButton_Click);
            // 
            // battleUnitsListView
            // 
            this.battleUnitsListView.HideSelection = false;
            this.battleUnitsListView.Location = new System.Drawing.Point(524, 517);
            this.battleUnitsListView.Name = "battleUnitsListView";
            this.battleUnitsListView.Size = new System.Drawing.Size(202, 100);
            this.battleUnitsListView.TabIndex = 3;
            this.battleUnitsListView.UseCompatibleStateImageBehavior = false;
            this.battleUnitsListView.View = System.Windows.Forms.View.List;
            this.battleUnitsListView.SelectedIndexChanged += new System.EventHandler(this.battleUnitsListView_SelectedIndexChanged);
            // 
            // mapLayout
            // 
            this.mapLayout.BackColor = System.Drawing.Color.PaleGreen;
            this.mapLayout.Location = new System.Drawing.Point(27, 10);
            this.mapLayout.Name = "mapLayout";
            this.mapLayout.Size = new System.Drawing.Size(801, 501);
            this.mapLayout.TabIndex = 4;
            this.mapLayout.TabStop = false;
            this.mapLayout.Click += new System.EventHandler(this.mapLayout_Click);
            this.mapLayout.Paint += new System.Windows.Forms.PaintEventHandler(this.mapLayout_Paint);
            this.mapLayout.MouseClick += new System.Windows.Forms.MouseEventHandler(this.mapLayout_MouseClick);
            this.mapLayout.MouseEnter += new System.EventHandler(this.mapLayout_MouseEnter);
            this.mapLayout.MouseHover += new System.EventHandler(this.mapLayout_MouseHover);
            // 
            // specialAbilityTitle
            // 
            this.specialAbilityTitle.AutoSize = true;
            this.specialAbilityTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.specialAbilityTitle.Location = new System.Drawing.Point(24, 527);
            this.specialAbilityTitle.Name = "specialAbilityTitle";
            this.specialAbilityTitle.Size = new System.Drawing.Size(92, 16);
            this.specialAbilityTitle.TabIndex = 5;
            this.specialAbilityTitle.Text = "Special ability";
            this.specialAbilityTitle.Visible = false;
            // 
            // specialAbilityText
            // 
            this.specialAbilityText.AutoSize = true;
            this.specialAbilityText.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.specialAbilityText.Location = new System.Drawing.Point(24, 543);
            this.specialAbilityText.Name = "specialAbilityText";
            this.specialAbilityText.Size = new System.Drawing.Size(0, 16);
            this.specialAbilityText.TabIndex = 6;
            this.specialAbilityText.Visible = false;
            // 
            // specialAbilityButton
            // 
            this.specialAbilityButton.Location = new System.Drawing.Point(275, 527);
            this.specialAbilityButton.Name = "specialAbilityButton";
            this.specialAbilityButton.Size = new System.Drawing.Size(103, 32);
            this.specialAbilityButton.TabIndex = 7;
            this.specialAbilityButton.Text = "Use special ability";
            this.specialAbilityButton.UseVisualStyleBackColor = true;
            this.specialAbilityButton.Visible = false;
            this.specialAbilityButton.Click += new System.EventHandler(this.specialAbilityButton_Click);
            // 
            // turnLabel
            // 
            this.turnLabel.AutoSize = true;
            this.turnLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.turnLabel.Location = new System.Drawing.Point(753, 518);
            this.turnLabel.Name = "turnLabel";
            this.turnLabel.Size = new System.Drawing.Size(0, 20);
            this.turnLabel.TabIndex = 8;
            // 
            // playerTurn
            // 
            this.playerTurn.AutoSize = true;
            this.playerTurn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.playerTurn.Location = new System.Drawing.Point(753, 543);
            this.playerTurn.Name = "playerTurn";
            this.playerTurn.Size = new System.Drawing.Size(0, 16);
            this.playerTurn.TabIndex = 9;
            // 
            // moveButton
            // 
            this.moveButton.Location = new System.Drawing.Point(164, 527);
            this.moveButton.Name = "moveButton";
            this.moveButton.Size = new System.Drawing.Size(49, 32);
            this.moveButton.TabIndex = 10;
            this.moveButton.Text = "Move";
            this.moveButton.UseVisualStyleBackColor = true;
            this.moveButton.Visible = false;
            this.moveButton.Click += new System.EventHandler(this.moveButton_Click);
            // 
            // attackButton
            // 
            this.attackButton.Location = new System.Drawing.Point(219, 527);
            this.attackButton.Name = "attackButton";
            this.attackButton.Size = new System.Drawing.Size(50, 32);
            this.attackButton.TabIndex = 11;
            this.attackButton.Text = "Attack";
            this.attackButton.UseVisualStyleBackColor = true;
            this.attackButton.Visible = false;
            this.attackButton.Click += new System.EventHandler(this.attackButton_Click);
            // 
            // confirmActionButton
            // 
            this.confirmActionButton.Location = new System.Drawing.Point(236, 618);
            this.confirmActionButton.Name = "confirmActionButton";
            this.confirmActionButton.Size = new System.Drawing.Size(75, 23);
            this.confirmActionButton.TabIndex = 12;
            this.confirmActionButton.Text = "confirm";
            this.confirmActionButton.UseVisualStyleBackColor = true;
            this.confirmActionButton.Visible = false;
            this.confirmActionButton.Click += new System.EventHandler(this.confirmActionButton_Click);
            // 
            // cancelActionButton
            // 
            this.cancelActionButton.Location = new System.Drawing.Point(317, 618);
            this.cancelActionButton.Name = "cancelActionButton";
            this.cancelActionButton.Size = new System.Drawing.Size(75, 23);
            this.cancelActionButton.TabIndex = 13;
            this.cancelActionButton.Text = "cancel";
            this.cancelActionButton.UseVisualStyleBackColor = true;
            this.cancelActionButton.Visible = false;
            this.cancelActionButton.Click += new System.EventHandler(this.cancelActionButton_Click);
            // 
            // informationalLabel
            // 
            this.informationalLabel.AutoSize = true;
            this.informationalLabel.Location = new System.Drawing.Point(27, 570);
            this.informationalLabel.Name = "informationalLabel";
            this.informationalLabel.Size = new System.Drawing.Size(62, 13);
            this.informationalLabel.TabIndex = 14;
            this.informationalLabel.Text = "Information:";
            // 
            // stateLabel
            // 
            this.stateLabel.AutoSize = true;
            this.stateLabel.Location = new System.Drawing.Point(27, 603);
            this.stateLabel.Name = "stateLabel";
            this.stateLabel.Size = new System.Drawing.Size(73, 13);
            this.stateLabel.TabIndex = 15;
            this.stateLabel.Text = "Current state: ";
            // 
            // endTurnButton
            // 
            this.endTurnButton.Location = new System.Drawing.Point(384, 527);
            this.endTurnButton.Name = "endTurnButton";
            this.endTurnButton.Size = new System.Drawing.Size(82, 32);
            this.endTurnButton.TabIndex = 16;
            this.endTurnButton.Text = "End unit\'s turn";
            this.endTurnButton.UseVisualStyleBackColor = true;
            this.endTurnButton.Click += new System.EventHandler(this.endTurnButton_Click);
            // 
            // CreateBattleUnitWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(846, 653);
            this.Controls.Add(this.endTurnButton);
            this.Controls.Add(this.stateLabel);
            this.Controls.Add(this.informationalLabel);
            this.Controls.Add(this.cancelActionButton);
            this.Controls.Add(this.confirmActionButton);
            this.Controls.Add(this.attackButton);
            this.Controls.Add(this.moveButton);
            this.Controls.Add(this.playerTurn);
            this.Controls.Add(this.turnLabel);
            this.Controls.Add(this.specialAbilityButton);
            this.Controls.Add(this.specialAbilityText);
            this.Controls.Add(this.specialAbilityTitle);
            this.Controls.Add(this.mapLayout);
            this.Controls.Add(this.battleUnitsListView);
            this.Controls.Add(this.finishButton);
            this.Controls.Add(this.battleUnitTypeComboBox);
            this.Controls.Add(this.addButton);
            this.Name = "CreateBattleUnitWindow";
            this.Text = "CreateBattleUnitWindow";
            this.Load += new System.EventHandler(this.CreateBattleUnitWindow_Load);
            ((System.ComponentModel.ISupportInitialize)(this.mapLayout)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ComboBox battleUnitTypeComboBox;
        private System.Windows.Forms.Button finishButton;
        private System.Windows.Forms.Button addButton;
        private System.Windows.Forms.ListView battleUnitsListView;
        private System.Windows.Forms.PictureBox mapLayout;
        private System.Windows.Forms.Label specialAbilityTitle;
        private System.Windows.Forms.Label specialAbilityText;
        private System.Windows.Forms.Button specialAbilityButton;
        private System.Windows.Forms.Label turnLabel;
        private System.Windows.Forms.Label playerTurn;
        private System.Windows.Forms.Button moveButton;
        private System.Windows.Forms.Button attackButton;
        private System.Windows.Forms.Button confirmActionButton;
        private System.Windows.Forms.Button cancelActionButton;
        private System.Windows.Forms.Label informationalLabel;
        private System.Windows.Forms.Label stateLabel;
        private System.Windows.Forms.Button endTurnButton;
    }
}