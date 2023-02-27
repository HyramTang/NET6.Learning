using Microsoft.Extensions.DependencyInjection;
using System;

namespace IOC2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ServiceCollection services = new ServiceCollection();
            //services.AddScoped<TestImpl1>();
            //services.AddScoped(typeof(ITestService), typeof(TestImpl1));
            //services.AddSingleton<ITestService, TestImpl1>();
            //services.AddSingleton(typeof(ITestService), new TestImpl1());
            services.AddScoped<ITestService, TestImpl1>();
            services.AddScoped<ITestService, TestImpl2>();
            using (ServiceProvider service = services.BuildServiceProvider())
            {
                // 必须的服务：如果服务不存在则抛异常，不用判断 null
                ITestService t1=service.GetRequiredService<ITestService>();
                t1.Name = "Test";
                t1.SayHi();

                // 多个服务
                IEnumerable<ITestService> ts=service.GetServices<ITestService>();
                foreach (ITestService t in ts)
                {
                    Console.WriteLine(t.GetType());
                    t.Name = "Hyram";
                    t.SayHi();
                }
            }
            Console.ReadKey();
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
}