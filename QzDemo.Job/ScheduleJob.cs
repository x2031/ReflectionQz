using Quartz;
using QzDemo.Core;
using Serilog;
using Serilog.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QzDemo.Job
{
    public class ScheduleJob : IWorkItem
    {
        public ITrigger Trigger { get; set; }
        public IJobDetail JobDetail { get; set; }
        public JobKey JobKey { get; set; }

        private static Logger? _log = new LoggerConfiguration()
            .WriteTo.Console()
            .CreateLogger();

        public ScheduleJob()
        {
            //触发器
            Trigger = TriggerBuilder.Create()
                 .WithIdentity("trigger2", "test")
                //.WithSimpleSchedule
                //(
                //    x => x.WithIntervalInSeconds(5).RepeatForever()
                //)
                .WithCronSchedule("0 33 23 ? * *")
                .StartNow()

                .Build();
            //作业的实例
            JobDetail = JobBuilder.Create(this.GetType())
                .WithIdentity("jobDetail2", "test")
                .Build();
            JobKey = JobDetail.Key;
        }

        public Task Execute(IJobExecutionContext context)
        {
            _log.Information("23.33 定时任务 {A}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            return Task.CompletedTask;
        }
    }
}
