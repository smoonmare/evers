import { Component, Input } from '@angular/core';
import { Machine } from '../../shared/models/machine.model';
import { MatCardModule } from '@angular/material/card';

@Component({
  selector: 'app-inventory-card',
  standalone: true,
  imports: [MatCardModule],
  templateUrl: './inventory-card.component.html',
  styleUrl: './inventory-card.component.scss'
})
export class InventoryCardComponent {
  @Input() item: Machine | undefined;

}
