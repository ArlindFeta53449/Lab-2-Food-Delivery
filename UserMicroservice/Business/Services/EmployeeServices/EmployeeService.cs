using AutoMapper;
using Business.Services.ParentServices;
using Data.DTOs.EmployeeDtos;
using Data.DTOs.ParentDtos;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.EmployeeServices
{
    public class EmployeeService : IEmployeeService
    {

        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public EmployeeService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<EmployeeDto>> GetAllEmployeesAsync()
        {
            var employees = await _context.Employees.ToListAsync();
            return _mapper.Map<IEnumerable<EmployeeDto>>(employees);
        }

        public async Task<EmployeeDto> GetEmployeeAsync(int id)
        {
            var employee = await _context.Employees.FirstOrDefaultAsync(a => a.Id == id);
            return _mapper.Map<EmployeeDto>(employee);
        }

        public async Task<EmployeeDto> CreateEmployeeAsync(EmployeeCreateDto employeeCreateDto)
        {
            var employee = _mapper.Map<Employee>(employeeCreateDto);
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();
            return _mapper.Map<EmployeeDto>(employee);
        }

        public async Task<EmployeeDto> UpdateEmployeeAsync(int id, EmployeeDto employeeDto)
        {
            var existingEmployee = await _context.Employees.FirstOrDefaultAsync(a => a.Id == id);
            if (existingEmployee == null)
            {
                throw new Exception("Employee not found");
            }

            _mapper.Map(employeeDto, existingEmployee);
            await _context.SaveChangesAsync();
            return _mapper.Map<EmployeeDto>(existingEmployee);
        }

        public async Task<bool> DeleteEmployeeAsync(int id)
        {
            var employee = await _context.Employees.FirstOrDefaultAsync(a => a.Id == id);
            if (employee == null)
            {
                return false;
            }

            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<EmployeeDto>> SearchEmployeesByNameAsync(string name)
        {
            var employees = await _context.Employees.Where(p => p.Name.Contains(name)).ToListAsync();
            return _mapper.Map<IEnumerable<EmployeeDto>>(employees);

        }
    }
}
