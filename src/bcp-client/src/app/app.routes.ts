import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: '',
    redirectTo: 'inscription',
    pathMatch: 'full'
  },
  {
    path: 'inscription',
    loadComponent: () => import('./features/inscription/inscription.component')
      .then(m => m.InscriptionComponent)
  },
  {
    path: 'inscription/success',
    loadComponent: () => import('./features/inscription/success/success.component')
      .then(m => m.SuccessComponent)
  },
  {
    path: 'admin/applications',
    loadComponent: () => import('./features/admin/applications/applications.component')
      .then(m => m.ApplicationsComponent)
  },
  {
    path: 'admin/applications/:id',
    loadComponent: () => import('./features/admin/application-detail/application-detail.component')
      .then(m => m.ApplicationDetailComponent)
  },
  {
    path: 'admin/login',
    loadComponent: () => import('./features/admin/login/login.component')
      .then(m => m.LoginComponent)
  }
];
