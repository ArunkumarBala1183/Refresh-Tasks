import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment.development';

@Injectable({
  providedIn: 'root'
})
export class EmployeeServiceService {

  private apiUrl : string = environment.apiUrl + "Employee";

  constructor(private http : HttpClient) { }

  
  getEmployee()
  {
    return this.http.get(this.apiUrl  + "/ViewEmployees");
  }

  getEmployeeDetails(employeeId : number)
  {
    return this.http.get(this.apiUrl + "/GetEmployee/" + employeeId)
  }
}
