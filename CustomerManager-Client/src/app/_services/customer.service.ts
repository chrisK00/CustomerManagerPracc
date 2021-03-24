import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Customer } from '../_interfaces/customer';

const httpOptions = {
  headers: new HttpHeaders({
    Authorization: 'Bearer ' + JSON.parse(localStorage.getItem('user'))?.token
  })
};

@Injectable({
  providedIn: 'root'
})
export class CustomerService {
  baseUrl = environment.apiUrl;
  constructor(private http: HttpClient) { }

  getCustomers() {
    return this.http.get<Customer[]>(this.baseUrl + 'customers', httpOptions);
  }
}
