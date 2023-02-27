using Microsoft.Extensions.DependencyInjection;
using System;

namespace IOC1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /*
             * 概念
             * 服务（service）：接口/对象
             * 服务容器：ServiceCollection services
             * 注册服务：services.AddScoped<ServiceObject>();
             * 使用服务:
             * - 服务提供者：ServiceProvider service
             * - 查询服务：ServiceObject t1 = serviceservice.GetService<ServiceObject>();
             * - 使用服务：t1.SayHi();
             * 
             * 服务生命周期
             * - Transient（瞬态）：每次使用服务都会 new 实例化一遍
             * - Scoped（范围）：每次请求生命周期 new 实例化一遍
             * - Singleton（单例）：App 启动时 new 实例化一遍
             */


            // 1.安装依赖包：Microsoft.Extensions.DependencyInjection
            // 2.实例化服务容器
            // 3.注册服务
            // 4.使用/查询/注入服务

            ServiceCollection services = new ServiceCollection();
            services.AddScoped<TestImpl1>();
            services.AddScoped<TestImpl2>();
            using (ServiceProvider service = services.BuildServiceProvider())
            {
                var t1 = service.GetService<TestImpl1>();
                t1.Name = "Hyram";
                t1.SayHi();

                var t11=service.GetService<TestImpl1>();
                // 服务生命周期
                // services.AddTransient：输出 False
                // services.AddScoped/AddSingleton：输出 True
                Console.WriteLine(object.ReferenceEquals(t1,t11));


            }
            Console.ReadKey();
        }
    }

    public interface ITestService
    {
        string Name { get; set; }
        void SayHi();
    }

    public class TestImpl1 : ITestService
    {
        public string Name { get; set; }

        public void SayHi()
        {
            Console.WriteLine($"Hi, I'm {Name}");
        }
    }

    public class TestImpl2 : ITestService
    {
        public string Name { get; set; }

        public void SayHi()
        {
            Console.WriteLine($"你好，我是{Name}");
        }
    }
}