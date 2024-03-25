import { Component, OnInit } from '@angular/core';
import { MatCardModule } from '@angular/material/card';
import { Machine } from '../../shared/models/machine.model';
import { InventoryCardComponent } from '../inventory-card/inventory-card.component';
import { Router } from '@angular/router';
import { MachineService } from '../../shared/services/machine.service';

@Component({
  selector: 'app-inventory-page',
  standalone: true,
  imports: [
    InventoryCardComponent,
    MatCardModule
  ],
  templateUrl: './inventory-page.component.html',
  styleUrl: './inventory-page.component.scss'
})
export class InventoryPageComponent implements OnInit {
  inventory: Machine[] = [];

  constructor(
    private router: Router,
    private machineService: MachineService
  ) { }

  ngOnInit(): void {
    this.machineService.getInventory()
      .subscribe(inventory => {
        this.inventory = inventory;
      });
  }

  viewDetails(itemId: string): void {
    this.router.navigate(['/inventory', itemId]);
  }
}
