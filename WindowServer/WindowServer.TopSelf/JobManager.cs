using Microsoft.Extensions.Configuration;
using Quartz;
using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using WindowServer.Services;
using WindowServer.Services.Model;

namespace WindowServer.TopShelf
{
    /// <summary>
    /// 统计服务
    /// </summary>
    public class JobManager
    {
        private readonly ISchedulerFactory _schedulerFactory;
        private readonly IConfiguration _configuration;
        private readonly IServiceProvider _serviceProvider;
        private readonly IScheduler _scheduler;

        public JobManager(ISchedulerFactory schedulerFactory,
            IConfiguration configuration,
            IServiceProvider serviceProvider)
        {
            this._schedulerFactory = schedulerFactory;
            this._configuration = configuration;
            this._serviceProvider = serviceProvider;

            this._scheduler = _schedulerFactory.GetScheduler().Result;
            _scheduler.JobFactory = new JobFactory(_serviceProvider);
        }

        public async Task ConfigWithStartJobs()
        {
            await Task.Factory.StartNew(async () =>
            {
                var assembly = Assembly.GetExecutingAssembly();
                var jobTypes = assembly.GetTypes().Where(c => typeof(IJob).IsAssignableFrom(c) && !c.IsAbstract).ToList();

                foreach (var jobType in jobTypes)
                {
                    var jobName = jobType.Name;
                    var jobAttribute = GetJobConfig(jobName);
                    if (!jobAttribute.IsEnabled)
                        continue;
                    await AddJob(jobType, jobAttribute.Crontab, jobAttribute.JobName, jobAttribute.Description);
                }
                await _scheduler.Start();
            });
        }


        private async Task AddJob(Type jobType, string cron, string jobName, string description)
        {
            var groupName = jobName + "_group";
            var triggerName = jobName + "_trigger";
            var triggerGroupName = groupName;

            var job = JobBuilder.Create(jobType)
                            .WithIdentity(jobName, groupName)
                            .WithDescription(description)
                            .Build();

            var trigger = TriggerBuilder.Create()
                                .WithIdentity(triggerName, triggerGroupName)
                                .WithSchedule(CronScheduleBuilder.CronSchedule(new CronExpression(cron)))
                                .StartNow()
                                .Build();

            await _scheduler.ScheduleJob(job, trigger);
        }

        private JobConfig GetJobConfig(string jobName)
        {
            var defaultCron = "0 0 0/2 * * ?";
            var jobConfig = new JobConfig
            {
                Crontab = _configuration.GetSection($"JobConfig:{jobName}:Crontab").Value ?? defaultCron,
                Description = _configuration.GetSection($"JobConfig:{jobName}:Description").Value ?? jobName,
                JobName = jobName
            };
            if (bool.TryParse(_configuration.GetSection($"JobConfig:{jobName}:IsEnabled").Value ?? "true", out bool isEnabled))
            {
                jobConfig.IsEnabled = isEnabled;
            }
            return jobConfig;
        }
    }
}
