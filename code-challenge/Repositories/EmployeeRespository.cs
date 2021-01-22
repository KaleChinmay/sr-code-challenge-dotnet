using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using challenge.Models;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using challenge.Data;

namespace challenge.Repositories
{
    public class EmployeeRespository : IEmployeeRepository
    {
        private readonly EmployeeContext _employeeContext;
        private readonly ILogger<IEmployeeRepository> _logger;

        public EmployeeRespository(ILogger<IEmployeeRepository> logger, EmployeeContext employeeContext)
        {
            _employeeContext = employeeContext;
            _logger = logger;
        }

        public Employee Add(Employee employee)
        {
            employee.EmployeeId = Guid.NewGuid().ToString();
            _employeeContext.Employees.Add(employee);
            _employeeContext.SaveChanges();
            return employee;
        }

        public Employee GetById(string id)
        {
            return _employeeContext.Employees.SingleOrDefault(e => e.EmployeeId == id);
        }

        public Task SaveAsync()
        {
            return _employeeContext.SaveChangesAsync();
        }

        public Employee Remove(Employee employee)
        {
            return _employeeContext.Remove(employee).Entity;
        }

        public ReportingStructure GetReporting(string id)
        {
            return _employeeContext.ReportingStructures.SingleOrDefault(e=>e.Id==id);
        }

        public Compensation GetCompensation(string id){
            return _employeeContext.Compensations.SingleOrDefault(e=>e.Id==id);
        }

        public Compensation Add(Compensation compensation)
        {
            compensation.Id = Guid.NewGuid().ToString();
            _employeeContext.Compensations.Add(compensation);
            _employeeContext.SaveChanges();
            return compensation;
        }
    }
}
