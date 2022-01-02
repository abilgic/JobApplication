using JobApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobApplication.Repository
{
    public class JobService:Repository<Job>, IJobService 
    {
        
        public JobService(ApplicationDbContext context) : base(context)
        {

        }

        public async Task<int> AddJob(JobModel jobmodel)
        {           
            try
            {
                var job = new Job
                {
                    Name = jobmodel.Name,
                    Description = jobmodel.Description,
                    CreateDate = DateTime.Now
                };

                var result = await Add(job);
                return result;

            }
            catch(Exception)
            {
                throw ;
            }
        }

        public async Task<IEnumerable<Job>> GetList()
        {
            return await GetAll();
        }

        public async Task<int> DeleteJob(int Id)
        {
            try
            {
                var jobitem = await GetById (Id);
               
                var result = await Remove(jobitem);
                return result;

            }
            catch (Exception)
            {
                throw;
            }
            
        }

        public async Task<Job> GetJob(int Id)
        {
            var jobitem = await GetById(Id);
            return jobitem;
        }

        public async Task<int> UpdateJob(JobModel jobmodel)
        {           
            try
            {
                var jobitem = await GetById(jobmodel.Id);
                jobitem.Name = jobmodel.Name;
                jobitem.Description = jobmodel.Description;
                jobitem.CreateDate = DateTime.Now;

                var result = await UpdateAsync(jobitem);
                return result;

            }
            catch (Exception)
            {
                throw;
            }
                     
        }
    }
}
