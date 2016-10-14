namespace deploy
{
    partial class deploy
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.btnRestart = new System.Windows.Forms.Button();
            this.btnShowConsole = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.includeLib = new System.Windows.Forms.CheckBox();
            this.serverBackup = new System.Windows.Forms.CheckBox();
            this.localBackup = new System.Windows.Forms.CheckBox();
            this.webappsLocation = new System.Windows.Forms.TextBox();
            this.deployAs = new System.Windows.Forms.TextBox();
            this.backupLocation = new System.Windows.Forms.TextBox();
            this.password = new System.Windows.Forms.TextBox();
            this.userName = new System.Windows.Forms.TextBox();
            this.port = new System.Windows.Forms.TextBox();
            this.ip = new System.Windows.Forms.TextBox();
            this.projectName = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.servers = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.shellLocation = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.catalinaOutLocation = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.stopTag = new System.Windows.Forms.TextBox();
            this.includeConfig = new System.Windows.Forms.CheckBox();
            this.includeWebFile = new System.Windows.Forms.CheckBox();
            this.includeByteCode = new System.Windows.Forms.CheckBox();
            this.btnPackage = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnRestart
            // 
            this.btnRestart.Location = new System.Drawing.Point(434 , 555);
            this.btnRestart.Name = "btnRestart";
            this.btnRestart.Size = new System.Drawing.Size(118 , 47);
            this.btnRestart.TabIndex = 21;
            this.btnRestart.Text = "重启服务";
            this.btnRestart.UseVisualStyleBackColor = true;
            this.btnRestart.Click += new System.EventHandler(this.btnRestart_Click);
            // 
            // btnShowConsole
            // 
            this.btnShowConsole.Location = new System.Drawing.Point(301 , 555);
            this.btnShowConsole.Name = "btnShowConsole";
            this.btnShowConsole.Size = new System.Drawing.Size(118 , 47);
            this.btnShowConsole.TabIndex = 20;
            this.btnShowConsole.Text = "查看控制台";
            this.btnShowConsole.UseVisualStyleBackColor = true;
            this.btnShowConsole.Click += new System.EventHandler(this.btnShowConsole_Click);
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(168 , 555);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(118 , 47);
            this.btnStart.TabIndex = 19;
            this.btnStart.Text = "开始部署";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // includeLib
            // 
            this.includeLib.AutoSize = true;
            this.includeLib.Location = new System.Drawing.Point(422 , 492);
            this.includeLib.Name = "includeLib";
            this.includeLib.Size = new System.Drawing.Size(78 , 16);
            this.includeLib.TabIndex = 14;
            this.includeLib.Text = "包含lib包";
            this.includeLib.UseVisualStyleBackColor = true;
            // 
            // serverBackup
            // 
            this.serverBackup.AutoSize = true;
            this.serverBackup.Location = new System.Drawing.Point(231 , 492);
            this.serverBackup.Name = "serverBackup";
            this.serverBackup.Size = new System.Drawing.Size(84 , 16);
            this.serverBackup.TabIndex = 13;
            this.serverBackup.Text = "服务器备份";
            this.serverBackup.UseVisualStyleBackColor = true;
            // 
            // localBackup
            // 
            this.localBackup.AutoSize = true;
            this.localBackup.Location = new System.Drawing.Point(63 , 492);
            this.localBackup.Name = "localBackup";
            this.localBackup.Size = new System.Drawing.Size(72 , 16);
            this.localBackup.TabIndex = 12;
            this.localBackup.Text = "本地备份";
            this.localBackup.UseVisualStyleBackColor = true;
            // 
            // webappsLocation
            // 
            this.webappsLocation.Location = new System.Drawing.Point(167 , 301);
            this.webappsLocation.Name = "webappsLocation";
            this.webappsLocation.Size = new System.Drawing.Size(384 , 21);
            this.webappsLocation.TabIndex = 7;
            // 
            // deployAs
            // 
            this.deployAs.Location = new System.Drawing.Point(167 , 338);
            this.deployAs.Name = "deployAs";
            this.deployAs.Size = new System.Drawing.Size(384 , 21);
            this.deployAs.TabIndex = 8;
            // 
            // backupLocation
            // 
            this.backupLocation.Location = new System.Drawing.Point(167 , 264);
            this.backupLocation.Name = "backupLocation";
            this.backupLocation.Size = new System.Drawing.Size(384 , 21);
            this.backupLocation.TabIndex = 6;
            // 
            // password
            // 
            this.password.Location = new System.Drawing.Point(167 , 227);
            this.password.Name = "password";
            this.password.PasswordChar = '*';
            this.password.Size = new System.Drawing.Size(384 , 21);
            this.password.TabIndex = 5;
            this.password.Tag = "";
            // 
            // userName
            // 
            this.userName.Location = new System.Drawing.Point(167 , 190);
            this.userName.Name = "userName";
            this.userName.Size = new System.Drawing.Size(384 , 21);
            this.userName.TabIndex = 4;
            // 
            // port
            // 
            this.port.Location = new System.Drawing.Point(167 , 153);
            this.port.Name = "port";
            this.port.Size = new System.Drawing.Size(384 , 21);
            this.port.TabIndex = 3;
            // 
            // ip
            // 
            this.ip.Location = new System.Drawing.Point(167 , 116);
            this.ip.Name = "ip";
            this.ip.Size = new System.Drawing.Size(384 , 21);
            this.ip.TabIndex = 2;
            // 
            // projectName
            // 
            this.projectName.Location = new System.Drawing.Point(167 , 79);
            this.projectName.Name = "projectName";
            this.projectName.Size = new System.Drawing.Size(384 , 21);
            this.projectName.TabIndex = 1;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(60 , 304);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(101 , 12);
            this.label12.TabIndex = 41;
            this.label12.Text = "服务器部署位置：";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(108 , 341);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(53 , 12);
            this.label10.TabIndex = 39;
            this.label10.Text = "部署为：";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(72 , 193);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(89 , 12);
            this.label9.TabIndex = 38;
            this.label9.Text = "服务器用户名：";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(84 , 230);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(77 , 12);
            this.label8.TabIndex = 37;
            this.label8.Text = "服务器密码：";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(60 , 267);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(101 , 12);
            this.label7.TabIndex = 36;
            this.label7.Text = "服务器备份位置：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(84 , 156);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77 , 12);
            this.label4.TabIndex = 33;
            this.label4.Text = "服务器端口：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(84 , 119);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77 , 12);
            this.label3.TabIndex = 32;
            this.label3.Text = "服务器地址：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(108 , 82);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53 , 12);
            this.label2.TabIndex = 31;
            this.label2.Text = "工程名：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(108 , 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53 , 12);
            this.label1.TabIndex = 30;
            this.label1.Text = "部署到：";
            // 
            // servers
            // 
            this.servers.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.servers.FormattingEnabled = true;
            this.servers.Location = new System.Drawing.Point(165 , 43);
            this.servers.Name = "servers";
            this.servers.Size = new System.Drawing.Size(384 , 20);
            this.servers.TabIndex = 0;
            this.servers.SelectedIndexChanged += new System.EventHandler(this.servers_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(60 , 378);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(101 , 12);
            this.label5.TabIndex = 57;
            this.label5.Text = "服务器脚本位置：";
            // 
            // shellLocation
            // 
            this.shellLocation.Location = new System.Drawing.Point(167 , 375);
            this.shellLocation.Name = "shellLocation";
            this.shellLocation.Size = new System.Drawing.Size(384 , 21);
            this.shellLocation.TabIndex = 9;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(60 , 415);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(101 , 12);
            this.label6.TabIndex = 59;
            this.label6.Text = "服务器日志位置：";
            // 
            // catalinaOutLocation
            // 
            this.catalinaOutLocation.Location = new System.Drawing.Point(167 , 412);
            this.catalinaOutLocation.Name = "catalinaOutLocation";
            this.catalinaOutLocation.Size = new System.Drawing.Size(384 , 21);
            this.catalinaOutLocation.TabIndex = 10;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(36 , 452);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(125 , 12);
            this.label11.TabIndex = 61;
            this.label11.Text = "服务器结束任务标识：";
            // 
            // stopTag
            // 
            this.stopTag.Location = new System.Drawing.Point(167 , 449);
            this.stopTag.Name = "stopTag";
            this.stopTag.Size = new System.Drawing.Size(384 , 21);
            this.stopTag.TabIndex = 11;
            // 
            // includeConfig
            // 
            this.includeConfig.AutoSize = true;
            this.includeConfig.Location = new System.Drawing.Point(63 , 515);
            this.includeConfig.Name = "includeConfig";
            this.includeConfig.Size = new System.Drawing.Size(96 , 16);
            this.includeConfig.TabIndex = 15;
            this.includeConfig.Text = "包含配置文件";
            this.includeConfig.UseVisualStyleBackColor = true;
            // 
            // includeWebFile
            // 
            this.includeWebFile.AutoSize = true;
            this.includeWebFile.Location = new System.Drawing.Point(231 , 514);
            this.includeWebFile.Name = "includeWebFile";
            this.includeWebFile.Size = new System.Drawing.Size(138 , 16);
            this.includeWebFile.TabIndex = 16;
            this.includeWebFile.Text = "包含jsp/js/html文件";
            this.includeWebFile.UseVisualStyleBackColor = true;
            // 
            // includeByteCode
            // 
            this.includeByteCode.AutoSize = true;
            this.includeByteCode.Location = new System.Drawing.Point(422 , 514);
            this.includeByteCode.Name = "includeByteCode";
            this.includeByteCode.Size = new System.Drawing.Size(102 , 16);
            this.includeByteCode.TabIndex = 17;
            this.includeByteCode.Text = "包含class文件";
            this.includeByteCode.UseVisualStyleBackColor = true;
            // 
            // btnPackage
            // 
            this.btnPackage.Location = new System.Drawing.Point(35 , 555);
            this.btnPackage.Name = "btnPackage";
            this.btnPackage.Size = new System.Drawing.Size(118 , 47);
            this.btnPackage.TabIndex = 18;
            this.btnPackage.Text = "打包";
            this.btnPackage.UseVisualStyleBackColor = true;
            this.btnPackage.Click += new System.EventHandler(this.btnPackage_Click);
            // 
            // deploy
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F , 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(587 , 640);
            this.Controls.Add(this.btnPackage);
            this.Controls.Add(this.includeByteCode);
            this.Controls.Add(this.includeWebFile);
            this.Controls.Add(this.includeConfig);
            this.Controls.Add(this.stopTag);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.catalinaOutLocation);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.shellLocation);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.servers);
            this.Controls.Add(this.btnRestart);
            this.Controls.Add(this.btnShowConsole);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.includeLib);
            this.Controls.Add(this.serverBackup);
            this.Controls.Add(this.localBackup);
            this.Controls.Add(this.webappsLocation);
            this.Controls.Add(this.deployAs);
            this.Controls.Add(this.backupLocation);
            this.Controls.Add(this.password);
            this.Controls.Add(this.userName);
            this.Controls.Add(this.port);
            this.Controls.Add(this.ip);
            this.Controls.Add(this.projectName);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "deploy";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "自动部署";
            this.Load += new System.EventHandler(this.deploy_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnRestart;
        private System.Windows.Forms.Button btnShowConsole;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.CheckBox includeLib;
        private System.Windows.Forms.CheckBox serverBackup;
        private System.Windows.Forms.CheckBox localBackup;
        private System.Windows.Forms.TextBox webappsLocation;
        private System.Windows.Forms.TextBox deployAs;
        private System.Windows.Forms.TextBox backupLocation;
        private System.Windows.Forms.TextBox password;
        private System.Windows.Forms.TextBox userName;
        private System.Windows.Forms.TextBox port;
        private System.Windows.Forms.TextBox ip;
        private System.Windows.Forms.TextBox projectName;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox servers;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox shellLocation;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox catalinaOutLocation;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox stopTag;
        private System.Windows.Forms.CheckBox includeConfig;
        private System.Windows.Forms.CheckBox includeWebFile;
        private System.Windows.Forms.CheckBox includeByteCode;
        private System.Windows.Forms.Button btnPackage;

    }
}

