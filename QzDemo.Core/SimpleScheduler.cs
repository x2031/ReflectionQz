using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QzDemo.Core
{
    /// <summary>
    /// 基于 Quartz.Net 简单的封装
    /// </summary>
    public class SimpleScheduler
    {
        private Quartz.ISchedulerFactory _schedulerFactory;
        private Quartz.IScheduler _scheduler;

        public SimpleScheduler()
        {
            _schedulerFactory = new Quartz.Impl.StdSchedulerFactory();
            _scheduler = _schedulerFactory.GetScheduler().ConfigureAwait(false).GetAwaiter().GetResult();
        }

        public static SimpleScheduler Create()
        {
            return new SimpleScheduler();
        }

        public SimpleScheduler AddJob(IWorkItem workItem)
        {
            _scheduler.ScheduleJob(workItem.JobDetail, workItem.Trigger);
            return this;
        }

        public void Start()
        {
            _scheduler.Start();
        }

        public void Shutdown()
        {
            if (_scheduler != null)
            {
                _scheduler.Shutdown();
            }
        }
    }
}
