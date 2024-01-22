import { Component, OnInit } from '@angular/core';
import { EmployeeServiceService } from '../Shared/employee-service.service';
import { RoleService } from '../Shared/role.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-view-employees',
  templateUrl: './view-employees.component.html',
  styleUrl: './view-employees.component.css'
})
export class ViewEmployeesComponent implements OnInit {

  public employeeDetails: any;

  public roleDetails : any;

  public roleRepository : {[key : number] : any} = {};


  constructor(
    private employeeService: EmployeeServiceService,
    private roleService: RoleService,
    private router : Router
  ) { }

  ngOnInit(): void {

    this.employeeService.getEmployee()
      .subscribe({
        next: (response) => {
          this.employeeDetails = response;
          console.log(this.employeeDetails);
          this.setRoles();
        },
        error: (error) => {
          console.log(error)
        }
      })
  }

  setRoles()
  {
    if(this.employeeDetails && this.employeeDetails.length > 0)
    {
      for(const employee of this.employeeDetails)
      {
        if(employee.employeeRoles.length > 0)
        {
          for(const role of employee.employeeRoles)
          {
            this.getRole(role.roleId)
          }
        }
      }
    }
  }

  getRole(roleId: number) {
    this.roleService.getRole(roleId)
      .subscribe({
        next: response => {
          this.roleDetails = response;
          
          if(this.roleRepository[roleId] === undefined)
          {
            this.roleRepository[roleId] = this.roleDetails.role;
          }
        },
        error: error => {
          console.log(error)
        }
      })
  }

  viewDetails(employeeId : number)
  {
    this.router.navigate(['/employee/view' , employeeId]);
  }

}
