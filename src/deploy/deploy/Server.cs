using System;
using System.Collections.Generic;
using System.Text;

namespace deploy
{
   /// <summary>
   /// config.json servers 对象实体模型
   /// </summary>
    class Server
    {
        private String name;

        public String Name
        {
            get { return name; }
            set { name = value; }
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

        private Boolean _default = false;

        public Boolean Default
        {
            get { return _default; }
            set { _default = value; }
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
    }
}
