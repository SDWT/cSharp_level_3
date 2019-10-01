using MailSender.lib.Entities;
using MailSender.lib.Services.DataProviders.Interfaces;

namespace MailSender.lib.Services.DataProviders.InMemory
{
    public class InMemorySchedulerTasksDataProvider : InMemoryDataProvider<SchedulerTask>, ISchedulerTasksDataProvider
    {
        public override void Copy(ref SchedulerTask destination, SchedulerTask source)
        {
            destination.Recipients = source.Recipients;
            destination.Sender = source.Sender;
            destination.Server = source.Server;
            destination.Time = source.Time;
            destination.Email = source.Email;
        }
    }


}
