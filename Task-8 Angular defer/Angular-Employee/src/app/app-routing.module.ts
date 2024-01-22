import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ViewEmployeesComponent } from './view-employees/view-employees.component';
import { AddEmployeeComponent } from './add-employee/add-employee.component';
import { LoginComponent } from './login/login.component';
import { RoleComponent } from './role/role.component';
import { ProfileComponent } from './profile/profile.component';
import { EmployeeDetailsComponent } from './employee-details/employee-details.component';
import { EmployeeComponent } from './employee/employee.component';

const routes: Routes = [
  {path : 'profile' , title : 'profile' , component : ProfileComponent},
  {path : 'login' , title : 'HRHub - Login' , component: LoginComponent},
  {path : 'role' , title : 'Role Assign' , component : RoleComponent},
  {
    'path': 'employee', component: EmployeeComponent, children: [
      {'path': '', 'title': 'Employee', component: ViewEmployeesComponent},
      { 'path': 'add', 'title': 'Add Employee', component: AddEmployeeComponent },
      {path : 'view/:id' , title : 'Employee Details' , component : EmployeeDetailsComponent}
    ]
  },
  { 'path': '', redirectTo: '/employee', pathMatch: 'full' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
