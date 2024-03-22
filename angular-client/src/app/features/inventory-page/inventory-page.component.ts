import { Component, OnInit } from '@angular/core';
import { MatCardModule } from '@angular/material/card';
import { Machine } from '../../shared/models/machine.model';
import { InventoryCardComponent } from '../inventory-card/inventory-card.component';
import { Router } from '@angular/router';

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

  constructor(private router: Router) { }

  ngOnInit(): void {
      
  }

  viewDetails(itemId: string): void {
    this.router.navigate(['/inventory', itemId]);
  }
}
