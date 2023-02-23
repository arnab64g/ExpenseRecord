using DatabaseLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLayer.Interface
{
    public interface ICategoryRepository
    {
        public Task<List<UserChoice>> GetUserChoiceAsync(string uname);
    }
}
