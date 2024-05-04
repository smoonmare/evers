import { Injectable } from '@angular/core';
import { Machine } from '../models/machine.model';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class MachineService {
  private baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  getInventory(): Observable<Machine[]> {
    return this.http.get<Machine[]>(`${this.baseUrl}/machine`);
  }

  getMachineById(id: string): Observable<Machine> {
    return this.http.get<Machine>(`${this.baseUrl}/machine/${id}`);
  }

  createMachine(machine: Machine): Observable<Machine> {
    return this.http.post<Machine>(`${this.baseUrl}/machine`, machine);
  }

  updateMachine(id: string, machine: Machine): Observable<any> {
    return this.http.patch(`${this.baseUrl}/machine/${id}`, machine);
  }

  deleteMachine(id: string): Observable<any> {
    return this.http.delete(`${this.baseUrl}/machine/${id}`);
  }
}
