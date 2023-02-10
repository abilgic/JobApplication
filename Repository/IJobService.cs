
using JobApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobApplication.Repository
{
    public interface IJobService: IRepository<Job>
    {
        Task<IEnumerable<Job>> GetList();
        Task<Job> GetJob(int Id);
        Task<int> AddJob(JobModel jobmodel);
        Task<int> UpdateJob(JobModel jobmodel);
        Task<int> DeleteJob(int Id);
    }
}
