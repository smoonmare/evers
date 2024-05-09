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
      id: [''],
      name: [''],
      year: [''],
      description: [''],
      category: [''],
      price: [''],
      location: [''],
      images: [''],
      thumbnail: [''],
      status: [''],
    });
  }

  ngOnInit(): void {
    this.machineService.getInventory()
      .subscribe(inventory => {
        this.inventory = inventory;
      });
  }

  private generatePatchOperations(): any[] {
    const patchOperations: any[] = [];
    const formValues = this.machineForm.value;
  
    Object.keys(formValues).forEach((key) => {
      if (key === 'id') {
        // Skip generating a patch operation for the `id` field
        return;
      }

      const newValue = formValues[key];
      if (newValue !== undefined && newValue !== null) {
        patchOperations.push({ op: 'replace', path: `/${key}`, value: newValue });
      }
    });
  
    return patchOperations;
  }
  
  onSubmit() {
    if (this.selectedMachine) {
      // Update existing machine
      const patchOperations = this.generatePatchOperations();
      this.machineService.updateMachine(this.selectedMachine.id!, this.machineForm.value).subscribe({
        next: () => {
          alert('Machine updated successfully');
          this.reloadInventory();
          console.log(this.machineForm.value);
        },
        error: (err) => {
          alert('Failed to update machine');
          console.error(err);
        }
      });
    } else {
      // Create new machine
      this.machineService.createMachine(this.machineForm.value).subscribe({
        next: (machine) => {
          alert('Machine created successfully');
          this.inventory.push(machine);
          this.machineForm.reset();
        },
        error: (err) => {
          alert('Failed to create machine');
          console.error(err);
        }
      });
    }
  }

  private reloadInventory() {
    this.machineService.getInventory().subscribe(inventory => {
      this.inventory = inventory;
    })
  }

  newMachine() {
    this.selectedMachine = null;
    this.machineForm.reset();
  }

  selectMachine(machine: Machine): void {
    this.selectedMachine = machine;
    this.machineForm.patchValue(machine);
    console.log(machine);
  }

  deleteMachine(machineId: string) {
    this.machineService.deleteMachine(machineId).subscribe({
      next: () => {
        alert('Machine deleted successfully!');
        this.inventory = this.inventory.filter(m => m.id !== machineId);
      },
      error: (err) => {
        alert('Failed to delete machine');
        console.error(err);
      }
    });
  }

  applyFilter(value: Event): void {

  }
}
