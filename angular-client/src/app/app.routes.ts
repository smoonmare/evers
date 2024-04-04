import { Router, Routes } from '@angular/router';
import { InventoryPageComponent } from './core/features/inventory-page/inventory-page.component';
import { DetailedViewComponent } from './core/features/detailed-view/detailed-view.component';
import { HomePageComponent } from './core/home-page/home-page.component';

export const routes: Routes = [
  { path: '', component: HomePageComponent },
  { path: 'inventory', component: InventoryPageComponent },
  { path: 'inventory/:id', component: DetailedViewComponent },
];
