using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Newtonsoft.Json;

namespace deploy
{

    /// <summary>
    /// config.json 的对象实体模型
    /// </summary>
    class Config
    {
        /// <summary>
        /// 读取 config.json 文件为 Config 对象
        /// </summary>
        /// <param name="configDotJsonLocation"></param>
        /// <returns></returns>
        public static Config getConfig(String configDotJsonLocation)
        {
            FileStream fileStream = new FileStream(configDotJsonLocation , FileMode.Open);
            StreamReader streamReader = new StreamReader(fileStream);
            Config config = new JsonSerializer().Deserialize<Config>(new JsonTextReader(streamReader));
            streamReader.Close();
            fileStream.Close();
            return config;
        }

        private String title;

        public String Title
        {
            get { return title; }
            set { title = value; }
        }
        private String id;

        public String Id
        {
            get { return id; }
            set { id = value; }
        }
        private String projectName;

        public String ProjectName
        {
            get { return projectName; }
            set { projectName = value; }
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

        private List<String> serverIpCheck;

        public List<String> ServerIpCheck
        {
            get { return serverIpCheck; }
            set { serverIpCheck = value; }
        }

        private List<Server> servers;

        public List<Server> Servers
        {
            get { return servers; }
            set { servers = value; }
        }


    }
}
