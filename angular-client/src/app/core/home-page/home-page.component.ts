import { Component, OnInit } from '@angular/core';
import { Machine } from '../../shared/models/machine.model';
import { Router } from '@angular/router';
import { MachineService } from '../../shared/services/machine.service';
import { InventoryCardComponent } from '../features/inventory-card/inventory-card.component';
import { MatCardModule } from '@angular/material/card';


@Component({
  selector: 'app-home-page',
  standalone: true,
  imports: [
    InventoryCardComponent,
    MatCardModule
  ],
  templateUrl: './home-page.component.html',
  styleUrl: './home-page.component.scss'
})
export class HomePageComponent implements OnInit {
  inventory: Machine[] = [];

  constructor(
    private router: Router,
    private machineService: MachineService
  ) { }

  ngOnInit(): void {
    this.machineService.getInventory()
      .subscribe(inventory => {
        this.inventory = inventory.slice(0, 3);
      })
  }

  viewDetails(itemId: string) {
    this.router.navigate(['/inventory', itemId]);
  }

}
