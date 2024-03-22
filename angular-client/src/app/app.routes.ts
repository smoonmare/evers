import { Router, Routes } from '@angular/router';
import { InventoryPageComponent } from './features/inventory-page/inventory-page.component';
import { DetailedViewComponent } from './features/detailed-view/detailed-view.component';

export const routes: Routes = [
  { path: 'inventory', component: InventoryPageComponent },
  { path: 'inventory/:id', component: DetailedViewComponent },
];
