import { NgModule } from '@angular/core';
import { BrowserModule, provideClientHydration } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { ViewEmployeesComponent } from './view-employees/view-employees.component';
import { AddEmployeeComponent } from './add-employee/add-employee.component';
import { NavbarComponent } from './navbar/navbar.component';
import { ProfileComponent } from './profile/profile.component';
import { LoginComponent } from './login/login.component';
import { RoleComponent } from './role/role.component';
import { HttpClientModule } from '@angular/common/http';
import { EmployeeDetailsComponent } from './employee-details/employee-details.component';
import { EmployeeComponent } from './employee/employee.component';

@NgModule({
  declarations: [
    AppComponent,
    ViewEmployeesComponent,
    AddEmployeeComponent,
    NavbarComponent,
    ProfileComponent,
    LoginComponent,
    RoleComponent,
    EmployeeDetailsComponent,
    EmployeeComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule
  ],
  providers: [
    provideClientHydration()
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
