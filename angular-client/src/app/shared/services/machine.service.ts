import { Injectable } from '@angular/core';
import { Machine } from '../models/machine.model';
import { HttpClient } from '@angular/common/http';
import { Observable, map } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class MachineService {
  private inventory: Machine[] = [];

  constructor(private http: HttpClient) { }

  getInventory(): Observable<Machine[]> {
    return this.http.get<Machine[]>('/assets/machines.json');
  }

  getMachineById(id: string): Observable<Machine | undefined> {
    return this.getInventory().pipe(
      map(machines => machines.find(machine => machine.id === id))
    );
  }

}
