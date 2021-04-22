using System;
using System.Linq;
using DeveloperTest.Business.Interfaces;
using DeveloperTest.Database;
using DeveloperTest.Database.Models;
using DeveloperTest.Enums;
using DeveloperTest.Models;
using Microsoft.EntityFrameworkCore;

namespace DeveloperTest.Business
{
    public class JobService : IJobService
    {
        private readonly ApplicationDbContext context;

        public JobService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public JobModel[] GetJobs()
        {
            return context.Jobs.Include(x => x.Customer).Select(x => new JobModel
            {
                JobId = x.JobId,
                Engineer = x.Engineer,
                When = x.When,
                Customer = MapCustomerModel(x.Customer)
            }).ToArray();
        }

        public JobModel GetJob(int jobId)
        {
            return context.Jobs.Include(c => c.Customer)
                .Where(x => x.JobId == jobId).Select(x => new JobModel
                {
                    JobId = x.JobId,
                    Engineer = x.Engineer,
                    When = x.When,
                    Customer = MapCustomerModel(x.Customer)
                }).SingleOrDefault();
        }

        public JobModel CreateJob(BaseJobModel model)
        {
            var addedJob = context.Jobs.Add(new Job
            {
                Engineer = model.Engineer,
                When = model.When,
                CustomerId = model.CustomerId
            });

            context.SaveChanges();

            return new JobModel
            {
                JobId = addedJob.Entity.JobId,
                Engineer = addedJob.Entity.Engineer,
                When = addedJob.Entity.When,
                Customer = MapCustomerModel(addedJob.Entity.Customer)
            };
        }

        private static CustomerModel MapCustomerModel(Customer customer)
        {
            if (customer == null)
            {
                return null;
            }

            return new CustomerModel()
            {
                CustomerId = customer.CustomerId,
                Name = customer.Name,
                Type = Enum.Parse<CustomerType>(customer.Type, true)
            };
        }
    }
}
