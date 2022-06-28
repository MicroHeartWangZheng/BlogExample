using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
namespace SvnTest
{
    class Program
    {

        static void Main(string[] args)
        {
            var version = args[3];
            var path = args[5];

            //var version = "147992";
            //var path = "\"D:\\Codes\\IcIntegration\\IcIntegrationNew\\IcIntegration.WebApi\\Response\\Mould\"";

            var projectName = GetProjectName(path);

            var response = ExeCommand($"svn log -r {version} {path}");

            var content = response.Substring(response.IndexOf($"r{version}"));

            var index = content.IndexOf("-----");

            content = content.Substring(0, index);

            var array = content.Split("\r\n");

            var author = array[0].Split("|")[1].Trim();
            var note = array[2];

            var client = HttpClientFactory.Create();

            var url = $@"http://115.236.37.105:8871/job/{projectName}/buildWithParameters?token={projectName}&name={author}&comment={note}";

            var result = client.GetAsync(url).Result;


            File.WriteAllText($"{AppDomain.CurrentDomain.SetupInformation.ApplicationBase}\\log.txt", JsonConvert.SerializeObject(new { version, path, author, note, url, result }));

        }

        private static void WriteLogFile(string input)
        {

            string fname = @"D:\logfile.txt";

            FileInfo finfo = new FileInfo(fname);
            ///判断文件是否存在以及是否大于2K
            if (finfo.Exists)
                finfo.Delete();

            ///创建只写文件流
            using (FileStream fs = finfo.OpenWrite())
            {
                var w = new StreamWriter(fs);
                w.BaseStream.Seek(0, SeekOrigin.End);
                w.Write(input + "\n");

                w.Flush();
                w.Close();
            }
        }


        public static string ExeCommand(string commandText)
        {
            Process p = new Process();
            p.StartInfo.FileName = "cmd.exe";
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardInput = true;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardError = true;
            p.StartInfo.CreateNoWindow = true;
            string strOutput = null;
            try
            {
                p.Start();
                p.StandardInput.WriteLine(commandText);
                p.StandardInput.WriteLine("exit");
                strOutput = p.StandardOutput.ReadToEnd();
                p.WaitForExit();
                p.Close();
            }
            catch (Exception e)
            {
                strOutput = e.Message;
            }
            return strOutput;
        }

        private static string GetProjectName(string path)
        {
            foreach (var dic in projectDics)
            {
                if (path.ToLower().Contains(dic.Key))
                    return dic.Value;
            }
            return String.Empty;
        }


        private static Dictionary<string, string> projectDics = new Dictionary<string, string>
        {
            { "integrationsystemsvn","IcIntegration.Web"},
            { "managerweb","IcIntegration.Web"},
            { "search","IcSearch"},
            { "supplier","Supplier"},
            { "gateway","IcGateway"},
            { "identityserver","IcIdentityServer"},
            { "user","IcUser"},
            { "integration","IcIntegration"},
            { "log","IcLog"},
            { "message","IcMessage"},
        };
    }
}
