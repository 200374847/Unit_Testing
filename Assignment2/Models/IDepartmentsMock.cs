using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment2.Models
{
   public interface IDepartmentsMock
    {
        IQueryable<Department> Departments { get; }
        Department Save(Department Department);
        void delete(Department Department);

    }
}
