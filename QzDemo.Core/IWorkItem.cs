using Quartz;

namespace QzDemo.Core
{
    public interface IWorkItem : IJob
    {
        ITrigger Trigger { get; set; }

        IJobDetail JobDetail { get; set; }

        JobKey JobKey { get; set; }
    }
}