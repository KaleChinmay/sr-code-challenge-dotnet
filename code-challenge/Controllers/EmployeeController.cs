using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using challenge.Services;
using challenge.Models;

namespace challenge.Controllers
{
    [Route("api/employee")]
    public class EmployeeController : Controller
    {
        private readonly ILogger _logger;
        private readonly IEmployeeService _employeeService;

        public EmployeeController(ILogger<EmployeeController> logger, IEmployeeService employeeService)
        {
            _logger = logger;
            _employeeService = employeeService;
        }

        [HttpPost]
        public IActionResult CreateEmployee([FromBody] Employee employee)
        {
            _logger.LogDebug($"Received employee create request for '{employee.FirstName} {employee.LastName}'");

            _employeeService.Create(employee);

            return CreatedAtRoute("getEmployeeById", new { id = employee.EmployeeId }, employee);
        }

        [HttpGet("{id}", Name = "getEmployeeById")]
        public IActionResult GetEmployeeById(String id)
        {
            _logger.LogDebug($"Received employee get request for '{id}'");

            var employee = _employeeService.GetById(id);

            if (employee == null)
                return NotFound();

            return Ok(employee);
        }

        [HttpPut("{id}")]
        public IActionResult ReplaceEmployee(String id, [FromBody]Employee newEmployee)
        {
            _logger.LogDebug($"Recieved employee update request for '{id}'");

            var existingEmployee = _employeeService.GetById(id);
            if (existingEmployee == null)
                return NotFound();

            _employeeService.Replace(existingEmployee, newEmployee);

            return Ok(newEmployee);
        }


        [HttpGet("{id}/reporting/")]
        public IActionResult GetReportingStructure(String id)
        {
            _logger.LogDebug($"Recieved employee reporting read request for '{id}'");
            var reporting = _employeeService.GetReporting(id);
            if (reporting == null)
                return NotFound();
            return Ok(reporting);
        }

        [HttpGet("{id}/compensation/")]
        public IActionResult GetCompensation(String id)
        {
            _logger.LogDebug($"Recieved employee compensation read request for '{id}'");
            var compensation = _employeeService.GetCompensation(id);
            if (compensation == null)
                return NotFound();
            return Ok(compensation);
        }

        [HttpPost("{id}/compensation")]
        public IActionResult AddCompensation([FromBody] Compensation compensation)
        {
            _logger.LogDebug($"Received employee compensation create request for '{compensation.Id}'");

            _employeeService.AddCompensation(compensation);
            return CreatedAtRoute("getEmployeeById", new { id = compensation.Id }, compensation);
        }
    }
}
