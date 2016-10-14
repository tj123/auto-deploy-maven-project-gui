using System;
using System.Collections.Generic;
using System.Text;

namespace deploy
{
    /// <summary>
    /// 页面数据对象
    /// </summary>
    class FormConfig
    {
        private String projectName;

        public String ProjectName
        {
            get { return projectName; }
            set { projectName = value; }
        }
        private String ip;

        public String Ip
        {
            get { return ip; }
            set { ip = value; }
        }
        private Int16 port;

        public Int16 Port
        {
            get { return port; }
            set { port = value; }
        }
        private Boolean localBackUp;

        public Boolean LocalBackUp
        {
            get { return localBackUp; }
            set { localBackUp = value; }
        }
        private Boolean serverBackUp;

        public Boolean ServerBackUp
        {
            get { return serverBackUp; }
            set { serverBackUp = value; }
        }

        private String userName;

        public String UserName
        {
            get { return userName; }
            set { userName = value; }
        }
        private String password;

        public String Password
        {
            get { return password; }
            set { password = value; }
        }
        private String deployAs;

        public String DeployAs
        {
            get { return deployAs; }
            set { deployAs = value; }
        }
        private String backUpLocation;

        public String BackUpLocation
        {
            get { return backUpLocation; }
            set { backUpLocation = value; }
        }
        private String shellLocation;

        public String ShellLocation
        {
            get { return shellLocation; }
            set { shellLocation = value; }
        }
        private String webappsLocation;

        public String WebappsLocation
        {
            get { return webappsLocation; }
            set { webappsLocation = value; }
        }
        private String catalinaOutLocation;

        public String CatalinaOutLocation
        {
            get { return catalinaOutLocation; }
            set { catalinaOutLocation = value; }
        }

        private String stopTag;

        public String StopTag
        {
            get { return stopTag; }
            set { stopTag = value; }
        }

        private Boolean includeLib;

        public Boolean IncludeLib
        {
            get { return includeLib; }
            set { includeLib = value; }
        }
        private Boolean includeConfig;

        public Boolean IncludeConfig
        {
            get { return includeConfig; }
            set { includeConfig = value; }
        }
        private Boolean includeWebFile;

        public Boolean IncludeWebFile
        {
            get { return includeWebFile; }
            set { includeWebFile = value; }
        }
        private Boolean includeByteCode;

        public Boolean IncludeByteCode
        {
            get { return includeByteCode; }
            set { includeByteCode = value; }
        }

        /// <summary>
        /// 转换为 Dictionary
        /// </summary>
        /// <returns></returns>
        public Dictionary<String, String> toDictionary()
        {
            Dictionary<String, String> dictionary = new Dictionary<string, string>();
            dictionary.Add("projectName", projectName);
            dictionary.Add("ip", ip);
            dictionary.Add("port", port.ToString());
            dictionary.Add("localBackUp", localBackUp ? "y" : "n");
            dictionary.Add("serverBackUp", serverBackUp ? "y" : "n");
            dictionary.Add("includeLib", includeLib ? "y" : "n");
            dictionary.Add("includeConfig", includeConfig ? "y" : "n");
            dictionary.Add("includeWebFile", includeWebFile ? "y" : "n");
            dictionary.Add("includeByteCode", includeByteCode ? "y" : "n");
            dictionary.Add("userName", userName);
            dictionary.Add("password", password);
            dictionary.Add("deployAs", deployAs);
            dictionary.Add("backUpLocation", backUpLocation);
            dictionary.Add("shellLocation", shellLocation);
            dictionary.Add("webappsLocation", webappsLocation);
            dictionary.Add("catalinaOutLocation", catalinaOutLocation);
            dictionary.Add("stopTag", stopTag);
            return dictionary;
        }

    }
}
