import { Component } from '@angular/core';
import { MatCardModule } from '@angular/material/card';
import { Router } from '@angular/router';
import { MachineService } from '../../../shared/services/machine.service';
import { Machine } from '../../../shared/models/machine.model';
import { FormBuilder, FormControl, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatSelectModule } from '@angular/material/select';
import { MatInputModule } from '@angular/material/input';
import { InventoryCardComponent } from '../inventory-card/inventory-card.component';
import { MatButtonModule } from '@angular/material/button';

@Component({
  selector: 'app-admin-dashboard',
  standalone: true,
  imports: [
    InventoryCardComponent,
    MatCardModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    MatSelectModule,
    MatInputModule,
    MatButtonModule
  ],
  templateUrl: './admin-dashboard.component.html',
  styleUrl: './admin-dashboard.component.scss'
})
export class AdminDashboardComponent {
  inventory: Machine[] = [];
  machineForm: FormGroup;
  selectedMachine: Machine | null = null;

  constructor(
    private router: Router,
    private machineService: MachineService,
    private formBuilder: FormBuilder
  ) {
    this.machineForm = this.formBuilder.group({
      name: [''],
      year: [''],
      description: [''],
      category: [''],
      price: [''],
      location: [''],
      status: [''],
      // Include other fields as necessary
    });
  }

  ngOnInit(): void {
    this.machineService.getInventory()
      .subscribe(inventory => {
        this.inventory = inventory;
      });
  }

  onSubmit() {
    if (this.selectedMachine) {
      // Update existing machine
    } else {
      // Create new machine
    }
  }

  newMachine() {
    this.selectedMachine = null;
    this.machineForm.reset();
  }

  selectMachine(machine: Machine): void {
    this.selectedMachine = machine;
    this.machineForm.patchValue(machine);
  }

  cancelEdit(): void {
    this.selectedMachine = null;
    this.machineForm.reset();
  }

  applyFilter(value: Event): void {

  }
}
