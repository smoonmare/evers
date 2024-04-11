import { Router, Routes } from '@angular/router';
import { InventoryPageComponent } from './core/features/inventory-page/inventory-page.component';
import { DetailedViewComponent } from './core/features/detailed-view/detailed-view.component';
import { HomePageComponent } from './core/home-page/home-page.component';
import { ServicesPageComponent } from './core/services-page/services-page.component';
import { ContactFormComponent } from './shared/contact-form/contact-form.component';
import { AdminDashboardComponent } from './core/features/admin-dashboard/admin-dashboard.component';

export const routes: Routes = [
  { path: '', component: HomePageComponent },
  { path: 'dashboard', component: AdminDashboardComponent},
  { path: 'inventory', component: InventoryPageComponent },
  { path: 'inventory/:id', component: DetailedViewComponent },
  { path: 'services', component: ServicesPageComponent },
  { path: 'contact', component: ContactFormComponent }
];
