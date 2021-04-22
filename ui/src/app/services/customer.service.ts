import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { CustomerModel } from '../models/customer.model';

@Injectable({
  providedIn: 'root'
})
export class CustomerService {

  constructor(private httpClient: HttpClient) { }

  public GetCustomers(): Observable<CustomerModel[]> {
    return this.httpClient.get<CustomerModel[]>('http://localhost:63235/customer');
  }

  public GetCustomerTypes(): Observable<string[]> {
    return this.httpClient.get<string[]>('http://localhost:63235/customer/types');
  }

  public GetCustomer(customerId: number): Observable<CustomerModel> {
    return this.httpClient.get<CustomerModel>(`http://localhost:63235/customer/${customerId}`);
  }

  public CreateCustomer(customer: CustomerModel): Promise<object> {
    return this.httpClient.post('http://localhost:63235/customer', customer).toPromise();
  }
}
