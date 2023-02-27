using Microsoft.Extensions.DependencyInjection;
using System.ComponentModel.Design;

namespace IOC3
{
    /*
     * 服务传染
     */
    internal class Program
    {
        static void Main(string[] args)
        {
            ServiceCollection services = new ServiceCollection();
            services.AddScoped<Controller>();
            services.AddScoped<ILog, LogImpl>();
            services.AddScoped<IConfig, COnfigImpl>();
            services.AddScoped<IStorage, StorageImple>();

            using (var sp=services.BuildServiceProvider())
            {
                var c= sp.GetRequiredService<Controller>();
                c.Test();
            }
        }
    }

    class Controller
    {
        private readonly ILog _log;
        private readonly IStorage _storage;

        public Controller(ILog log,IStorage storage)
        {
            _log = log;
            _storage = storage;
        }

        public void Test()
        {
            _log.Log("开始上传");
            _storage.Save("asdhasidhkasd", "AABBCC");
            _log.Log("上传完毕");
        }

    }

    interface ILog
    {
        public void Log(string message);
    }

    class LogImpl : ILog
    {
        public void Log(string message)
        {
            Console.WriteLine($"日志：{message}");
        }
    }

    interface IConfig
    {
        public string GetValue(string key);
    }

    class COnfigImpl : IConfig
    {
        public string GetValue(string key)
        {
            return "Config Hello";
        }
    }

    interface IStorage
    {
        public void Save(string content, string name);
    }

    class StorageImple : IStorage
    {
        private readonly IConfig _config;
        public StorageImple(IConfig config)
        {
            _config = config;
        }

        public void Save(string content, string name)
        {
            string server=_config.GetValue(name);
            Console.WriteLine($"向服务器:{name}，上传文件:{content}");
        }
    }
}