using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;

namespace deploy
{
    class StringUtil
    {
        /// <summary>
        /// 替换 文件中的 ${ * } 元素
        /// </summary>
        /// <param name="templateFile">模版文件路径</param>
        /// <param name="targetFile">替换后文件位置</param>
        /// <param name="dictionary">包含所有要替换的值</param>
        public static void replaceDictionary(String templateFile, String targetFile, Dictionary<String, String> dictionary)
        {
            try
            {
                FileStream templateStream = new FileStream(templateFile, FileMode.Open);
                StreamReader templateReader = new StreamReader(templateStream, Encoding.Default);
                FileStream targetStream = new FileStream(targetFile, FileMode.Create);
                StreamWriter targetWriter = new StreamWriter(targetStream, Encoding.Default);
                String line;
                while ((line = templateReader.ReadLine()) != null)
                {
                    MatchCollection matchs = Regex.Matches(line, @"\$\{[^\{\}]*\}");
                    for (int i = 0; i < matchs.Count; i++)
                    {
                        Match match = matchs[i];
                        String key = match.Value.Replace("${", "").Replace("}", "").Trim();
                        String value = "";
                        try
                        {
                            value = dictionary[key];
                        }
                        catch (Exception e)
                        { }
                        line = line.Replace(match.Value, value);
                    }
                    targetWriter.WriteLine(line);
                }
                targetWriter.Close();
                targetStream.Close();
                templateReader.Close();
                templateStream.Close();
            }
            catch (Exception e)
            {

            }
        }
    }
}
