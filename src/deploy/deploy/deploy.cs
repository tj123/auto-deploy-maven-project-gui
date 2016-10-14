using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.IO;
using System.Diagnostics;

namespace deploy
{
    public partial class deploy : Form
    {

        private Config config;
        private Dictionary<String , FormConfig> configs;
        private String defaultName;
        private String currentDirectory = new FileInfo(Process.GetCurrentProcess().MainModule.FileName).Directory.FullName;

        public deploy()
        {
            InitializeComponent();
            String config_json = currentDirectory + @"\config\config.json";
            config = Config.getConfig(config_json);
            Server defaultServer = null;
            configs = new Dictionary<String , FormConfig>();
            config.Servers.ForEach(delegate(Server server)
            {
                if( server.Default )
                {
                    defaultServer = server;
                }
                configs.Add(server.Name , generateFormConfig(config , server));
            });
            if( defaultServer == null && config.Servers != null && config.Servers.Count != 0 )
            {
                defaultServer = config.Servers[0];
            }
            defaultName = defaultServer.Name;
        }

        /// <summary>
        /// 根据配置文件获取配置
        /// </summary>
        /// <param name="config"></param>
        /// <param name="server"></param>
        /// <returns></returns>
        private FormConfig generateFormConfig(Config config , Server server)
        {
            FormConfig formConfig = new FormConfig();
            formConfig.BackUpLocation = server.BackUpLocation;
            formConfig.CatalinaOutLocation = server.CatalinaOutLocation;
            formConfig.DeployAs = server.DeployAs;
            formConfig.Ip = server.Ip;
            formConfig.LocalBackUp = config.LocalBackUp;
            formConfig.Password = server.Password;
            formConfig.Port = server.Port;
            formConfig.ProjectName = config.ProjectName;
            formConfig.ServerBackUp = config.ServerBackUp;
            formConfig.ShellLocation = server.ShellLocation;
            formConfig.UserName = server.UserName;
            formConfig.WebappsLocation = server.WebappsLocation;
            formConfig.StopTag = server.StopTag;
            formConfig.IncludeWebFile = server.IncludeWebFile;
            formConfig.IncludeByteCode = server.IncludeByteCode;
            formConfig.IncludeConfig = server.IncludeConfig;
            formConfig.IncludeLib = server.IncludeLib;
            return formConfig;
        }

        /// <summary>
        /// 对象的值赋给页面
        /// </summary>
        /// <param name="formConfig"></param>
        private void setFormConfig(FormConfig formConfig)
        {
            backupLocation.Text = formConfig.BackUpLocation;
            catalinaOutLocation.Text = formConfig.CatalinaOutLocation;
            deployAs.Text = formConfig.DeployAs;
            ip.Text = formConfig.Ip;
            localBackup.Checked = formConfig.LocalBackUp;
            userName.Text = formConfig.UserName;
            password.Text = formConfig.Password;
            port.Text = formConfig.Port.ToString();
            projectName.Text = formConfig.ProjectName;
            serverBackup.Checked = formConfig.ServerBackUp;
            shellLocation.Text = formConfig.ShellLocation;
            webappsLocation.Text = formConfig.WebappsLocation;
            stopTag.Text = formConfig.StopTag;
            includeWebFile.Checked = formConfig.IncludeWebFile;
            includeByteCode.Checked = formConfig.IncludeByteCode;
            includeConfig.Checked = formConfig.IncludeConfig;
            includeLib.Checked = formConfig.IncludeLib;
        }

        /// <summary>
        /// 页面上的值转换为对象 
        /// </summary>
        /// <returns></returns>
        private FormConfig getFormConfig()
        {
            FormConfig formConfig = new FormConfig();
            formConfig.BackUpLocation = backupLocation.Text;
            formConfig.CatalinaOutLocation = catalinaOutLocation.Text;
            formConfig.DeployAs = deployAs.Text;
            formConfig.Ip = ip.Text;
            formConfig.LocalBackUp = localBackup.Checked;
            formConfig.Password = password.Text;
            formConfig.Port = Int16.Parse(port.Text);
            formConfig.ProjectName = projectName.Text;
            formConfig.ServerBackUp = serverBackup.Checked;
            formConfig.ShellLocation = shellLocation.Text;
            formConfig.UserName = userName.Text;
            formConfig.WebappsLocation = webappsLocation.Text;
            formConfig.StopTag = stopTag.Text;
            formConfig.IncludeWebFile = includeWebFile.Checked;
            formConfig.IncludeByteCode = includeByteCode.Checked;
            formConfig.IncludeConfig = includeConfig.Checked;
            formConfig.IncludeLib = includeLib.Checked;
            return formConfig;
        }

        private void deploy_Load(object sender , EventArgs e)
        {
            foreach( var config in configs )
            {
                servers.Items.Add(config.Key);
            }
            servers.Text = defaultName;
        }

        private void servers_SelectedIndexChanged(object sender , EventArgs e)
        {
            Text = config.Title;
            setFormConfig(configs[servers.Text]);
        }

        private void btnStart_Click(object sender , EventArgs e)
        {
            String currentDirectory = new FileInfo(Process.GetCurrentProcess().MainModule.FileName).Directory.FullName;
            String deployLocation = currentDirectory + @"\bin\deploy.cmd";
            Dictionary<String , String> dictionary = getFormConfig().toDictionary();
            dictionary.Add("currentDirectory" , currentDirectory + @"\bin");
            StringUtil.replaceDictionary(currentDirectory + @"\template\deploy.template.cmd" , deployLocation , dictionary);
            String servers = "";
            foreach( String ip in config.ServerIpCheck )
            {
                servers += ip + ",";
            }
            if( config.ServerIpCheck.Count != 0 )
            {
                servers = servers.Substring(0 , servers.Length - 1);
            }
            dictionary.Add("servers" , servers);
            StringUtil.replaceDictionary(currentDirectory + @"\template\package.template.cmd" , currentDirectory + @"\bin\package.cmd" , dictionary);
            Process process = new Process();
            process.StartInfo.FileName = deployLocation;
            process.Start();
            process.WaitForExit();
            if( File.Exists(currentDirectory + @"\bin\package.cmd") )
            {
                File.Delete(currentDirectory + @"\bin\package.cmd");
            }
            if( File.Exists(deployLocation) )
            {
                File.Delete(deployLocation);
            }
        }

        /// <summary>
        /// 显示控制台
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnShowConsole_Click(object sender , EventArgs e)
        {
            String currentDir = new FileInfo(Process.GetCurrentProcess().MainModule.FileName).Directory.FullName;
            String putty = currentDir + @"\bin\putty.exe";
            Process process = new Process();
            FormConfig cfg = getFormConfig();
            String showConsoleFileName = currentDir + @"\show_console.sh";
            FileStream fileStrean = new FileStream(showConsoleFileName , FileMode.Create);
            StreamWriter streamWriter = new StreamWriter(fileStrean);
            streamWriter.Write("/usr/bin/tail -f " + cfg.CatalinaOutLocation);
            streamWriter.Close();
            fileStrean.Close();
            process.StartInfo.FileName = putty;
            if( cfg.Password == null || cfg.Password.Trim().Equals("") )
            {
                MessageBox.Show("密码不能为空！");
                return;
            }
            process.StartInfo.Arguments = cfg.UserName + "@" + cfg.Ip
                + " -P " + cfg.Port
                + " -m \"" + showConsoleFileName + "\""
                + " -pw " + cfg.Password;
            process.Start();
            System.Threading.Thread.Sleep(3000);
            if( File.Exists(showConsoleFileName) )
            {
                File.Delete(showConsoleFileName);
            }
        }

        /// <summary>
        /// 重启服务
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRestart_Click(object sender , EventArgs e)
        {
            if( !(MessageBox.Show("确认要重启服务？" , "重启服务" , MessageBoxButtons.OKCancel) == DialogResult.OK) )
                return;
            String currentDir = new FileInfo(Process.GetCurrentProcess().MainModule.FileName).Directory.FullName;
            String putty = currentDir + @"\bin\putty.exe";
            Process process = new Process();
            FormConfig cfg = getFormConfig();
            String restartServerFileName = currentDir + @"\restart_server.sh";
            FileStream fileStrean = new FileStream(restartServerFileName , FileMode.Create);
            StreamWriter streamWriter = new StreamWriter(fileStrean);
            streamWriter.WriteLine("/usr/bin/ps -ef|grep tomcat|grep " + cfg.StopTag + "|grep -v grep|grep -v PPID|grep -v tail|awk '{print $2}'|xargs kill -9");
            streamWriter.WriteLine(cfg.ShellLocation + "/startup.sh");
            streamWriter.Close();
            fileStrean.Close();
            process.StartInfo.FileName = putty;
            process.StartInfo.Arguments = cfg.UserName + "@" + cfg.Ip
                + " -P " + cfg.Port
                + " -m \"" + restartServerFileName + "\""
                + " -pw " + cfg.Password;
            process.Start();
            System.Threading.Thread.Sleep(3000);
            if( File.Exists(restartServerFileName) )
            {
                File.Delete(restartServerFileName);
            }
        }

        private void btnPackage_Click(object sender , EventArgs e)
        {
            package();
        }

        /// <summary>
        /// 打包
        /// </summary>
        private void package()
        {
            FormConfig formConfig = getFormConfig();
            String packageLocation = currentDirectory + @"\bin\package.cmd";
            var dictionary = formConfig.toDictionary();
            dictionary.Add("currentDirectory" , currentDirectory + @"\bin");
            String servers = "";
            foreach( String ip in config.ServerIpCheck )
            {
                servers += ip + ",";
            }
            if( config.ServerIpCheck.Count != 0 )
            {
                servers = servers.Substring(0 , servers.Length - 1);
            }
            dictionary.Add("servers" , servers);
            StringUtil.replaceDictionary(currentDirectory + @"\template\package.template.cmd" , packageLocation , dictionary);
            Process process = new Process();
            process.StartInfo.FileName = packageLocation;
            process.Start();
            process.WaitForExit();
            if( File.Exists(packageLocation) )
            {
                File.Delete(packageLocation);
            }
        }

    }
}
