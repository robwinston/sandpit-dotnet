namespace CalcForms
{
    partial class IntExpression
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
            this.components = new System.ComponentModel.Container();
            this.txtExpression = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.ExpressionTip = new System.Windows.Forms.ToolTip(this.components);
            this.txtMessages = new System.Windows.Forms.TextBox();
            this.btnEvaluate = new System.Windows.Forms.Button();
            this.txtResult = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnReset = new System.Windows.Forms.Button();
            this.grpExpOptions = new System.Windows.Forms.GroupBox();
            this.rbSimple = new System.Windows.Forms.RadioButton();
            this.rbExtended = new System.Windows.Forms.RadioButton();
            this.txtWhatsAllowed = new System.Windows.Forms.TextBox();
            this.grpExpOptions.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtExpression
            // 
            this.txtExpression.Location = new System.Drawing.Point(75, 62);
            this.txtExpression.Name = "txtExpression";
            this.txtExpression.Size = new System.Drawing.Size(239, 20);
            this.txtExpression.TabIndex = 0;
            this.ExpressionTip.SetToolTip(this.txtExpression, "Enter an integer arithmetic expression using add (+), subtract (-), multiply(*), " +
        "negate(-)\r\nGroup expressions with parentheses as needed (nesting is permitted).");
            this.txtExpression.TextChanged += new System.EventHandler(this.OnExpressionChange);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(75, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Expression";
            // 
            // txtMessages
            // 
            this.txtMessages.Location = new System.Drawing.Point(75, 231);
            this.txtMessages.Multiline = true;
            this.txtMessages.Name = "txtMessages";
            this.txtMessages.ReadOnly = true;
            this.txtMessages.Size = new System.Drawing.Size(353, 122);
            this.txtMessages.TabIndex = 5;
            this.ExpressionTip.SetToolTip(this.txtMessages, "Displays error messages if Evaluate fails");
            // 
            // btnEvaluate
            // 
            this.btnEvaluate.AutoSize = true;
            this.btnEvaluate.Enabled = false;
            this.btnEvaluate.Location = new System.Drawing.Point(75, 98);
            this.btnEvaluate.Name = "btnEvaluate";
            this.btnEvaluate.Size = new System.Drawing.Size(75, 23);
            this.btnEvaluate.TabIndex = 2;
            this.btnEvaluate.Text = "Evaluate";
            this.btnEvaluate.UseVisualStyleBackColor = true;
            this.btnEvaluate.Click += new System.EventHandler(this.OnEvaluate);
            // 
            // txtResult
            // 
            this.txtResult.Location = new System.Drawing.Point(75, 166);
            this.txtResult.Name = "txtResult";
            this.txtResult.ReadOnly = true;
            this.txtResult.Size = new System.Drawing.Size(100, 20);
            this.txtResult.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(75, 137);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Result";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(75, 202);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Messages";
            // 
            // btnReset
            // 
            this.btnReset.AutoSize = true;
            this.btnReset.Enabled = false;
            this.btnReset.Location = new System.Drawing.Point(166, 98);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(75, 23);
            this.btnReset.TabIndex = 7;
            this.btnReset.Text = "Reset";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.OnReset);
            // 
            // grpExpOptions
            // 
            this.grpExpOptions.Controls.Add(this.txtWhatsAllowed);
            this.grpExpOptions.Controls.Add(this.rbExtended);
            this.grpExpOptions.Controls.Add(this.rbSimple);
            this.grpExpOptions.Location = new System.Drawing.Point(534, 33);
            this.grpExpOptions.Name = "grpExpOptions";
            this.grpExpOptions.Size = new System.Drawing.Size(200, 100);
            this.grpExpOptions.TabIndex = 8;
            this.grpExpOptions.TabStop = false;
            this.grpExpOptions.Text = "Supported Expressions";
            // 
            // rbSimple
            // 
            this.rbSimple.AutoSize = true;
            this.rbSimple.Checked = true;
            this.rbSimple.Location = new System.Drawing.Point(7, 20);
            this.rbSimple.Name = "rbSimple";
            this.rbSimple.Size = new System.Drawing.Size(56, 17);
            this.rbSimple.TabIndex = 0;
            this.rbSimple.TabStop = true;
            this.rbSimple.Text = "Simple";
            this.rbSimple.UseVisualStyleBackColor = true;
            this.rbSimple.CheckedChanged += new System.EventHandler(this.OnExpressionOpts);
            // 
            // rbExtended
            // 
            this.rbExtended.AutoSize = true;
            this.rbExtended.Location = new System.Drawing.Point(7, 44);
            this.rbExtended.Name = "rbExtended";
            this.rbExtended.Size = new System.Drawing.Size(70, 17);
            this.rbExtended.TabIndex = 1;
            this.rbExtended.Text = "Extended";
            this.rbExtended.UseVisualStyleBackColor = true;
            this.rbExtended.CheckedChanged += new System.EventHandler(this.OnExpressionOpts);
            // 
            // txtWhatsAllowed
            // 
            this.txtWhatsAllowed.Location = new System.Drawing.Point(7, 67);
            this.txtWhatsAllowed.Name = "txtWhatsAllowed";
            this.txtWhatsAllowed.ReadOnly = true;
            this.txtWhatsAllowed.Size = new System.Drawing.Size(175, 20);
            this.txtWhatsAllowed.TabIndex = 2;
            // 
            // IntExpression
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(762, 395);
            this.Controls.Add(this.grpExpOptions);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtMessages);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtResult);
            this.Controls.Add(this.btnEvaluate);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtExpression);
            this.Name = "IntExpression";
            this.Text = "Simple Expression Calculator";
            this.grpExpOptions.ResumeLayout(false);
            this.grpExpOptions.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtExpression;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolTip ExpressionTip;
        private System.Windows.Forms.Button btnEvaluate;
        private System.Windows.Forms.TextBox txtResult;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtMessages;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.GroupBox grpExpOptions;
        private System.Windows.Forms.TextBox txtWhatsAllowed;
        private System.Windows.Forms.RadioButton rbExtended;
        private System.Windows.Forms.RadioButton rbSimple;
    }
}

