import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { EmployeeServiceService } from '../Shared/employee-service.service';
import { RoleService } from '../Shared/role.service';

@Component({
  selector: 'app-employee-details',
  templateUrl: './employee-details.component.html',
  styleUrl: './employee-details.component.css'
})
export class EmployeeDetailsComponent implements OnInit {

  public employeeDetail: any;

  private roleDetails: any;

  public infoOrder: string[] = ["employeeId", "name", "age", "dateofBirth", "emailId", "mobileNumber"];

  public contactOrder: string[] = ["doorNumber", "street", "city", "district", "state", "postalCode"];

  public roleRepository: { [key: number]: any } = {};

  constructor(private route: ActivatedRoute, private employeeService: EmployeeServiceService , private roleService : RoleService) { }


  ngOnInit(): void {

    this.route.params.subscribe(param => {
      const id = param['id']

      this.getDetails(id);
      this.setRoles();
    })
  }

  getDetails(employeeId: number) {
    this.employeeService.getEmployeeDetails(employeeId)
      .subscribe({
        next: response => {
          this.employeeDetail = response

          console.log(response)
          console.log(this.employeeDetail)
        },
        error: error => {
          console.log(error)
        }
      })
  }

  setRoles() {
    if (this.employeeDetail.employeeRoles.length > 0) {
      for (const role of this.employeeDetail.employeeRoles) {
        this.getRole(role.roleId)
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


}
