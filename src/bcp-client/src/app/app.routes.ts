import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: '',
    redirectTo: 'home',
    pathMatch: 'full'
  },
  {
    path: 'home',
    loadComponent: () => import('./features/home/home.component')
      .then(m => m.HomeComponent)
  },
  {
    path: 'inscription',
    loadComponent: () => import('./features/inscription/inscription.component')
      .then(m => m.InscriptionComponent)
  }
];
