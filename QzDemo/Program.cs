using Quartz;
using Quartz.Impl;
using QzDemo.Core;
using System.Net.WebSockets;
using System.Reflection;

namespace QzDemo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            SimpleScheduler scheduler = SimpleScheduler.Create();
            List<IWorkItem> workItems = GetWorkItems();

            //根据具体配置创建job
            foreach (var item in workItems)
            {
                var job = scheduler.AddJob(item);
                Console.WriteLine(item.JobKey + "启动成功");
            }
            scheduler.Start();
            Console.ReadKey();
        }
        static List<IWorkItem> GetWorkItems()
        {
            Assembly assembly = Assembly.LoadFile(@"D:\Codes\Qz\QzDemo.Job\bin\Debug\net6.0\QzDemo.Job.dll");
            List<IWorkItem> workItems = new List<IWorkItem>();
            foreach (var item in assembly.ExportedTypes)
            {
                Type? type = item.GetInterface(nameof(IWorkItem));

                if (type != null && type.Name.Equals(nameof(IWorkItem)))
                {
                    ConstructorInfo? constructorInfo = item.GetConstructor(BindingFlags.Public | BindingFlags.Instance, Type.EmptyTypes);
                    var workItem = constructorInfo?.Invoke(Array.Empty<object>()) as IWorkItem;

                    workItems.Add(item: workItem!);
                }
            }
            return workItems;
        }
    }
}