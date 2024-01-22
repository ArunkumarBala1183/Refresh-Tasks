import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment.development';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class RoleService {

  private apiUrl : string = environment.apiUrl + "Role";

  constructor(private http : HttpClient) { }

  getRole(roleId : number)
  {
    return this.http.get(this.apiUrl + "/" + roleId);
  }
}
