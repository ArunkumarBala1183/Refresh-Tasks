using System;
using System.Collections.Generic;

namespace EmployeeManagement.Models.DbModels;

public class EmployeeRole
{
    public int Id { get; set; }

    public Employee? Employee { get; set; }
    public int EmployeeId { get; set; }

    public int RoleId { get; set; }
    public Roles? Role { get; set; }
}