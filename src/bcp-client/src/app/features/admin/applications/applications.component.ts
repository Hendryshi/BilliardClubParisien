import { Component, ViewChild } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatTableModule, MatTableDataSource } from '@angular/material/table';
import { MatPaginatorModule, MatPaginator } from '@angular/material/paginator';
import { MatSortModule, MatSort } from '@angular/material/sort';
import { MatCardModule } from '@angular/material/card';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { MatChipsModule } from '@angular/material/chips';
import { MatTooltipModule } from '@angular/material/tooltip';
import { RouterModule } from '@angular/router';
import { BreadcrumbComponent } from '../../../shared/components/breadcrumb/breadcrumb.component';
import { PageHeaderComponent } from '../../../shared/components/page-header/page-header.component';

interface Application {
  id: number;
  nom: string;
  prenom: string;
  email: string;
  telephone: string;
  formule: string;
  dateInscription: Date;
  status: 'En attente' | 'Approuvé' | 'Refusé';
}

@Component({
  selector: 'app-applications',
  standalone: true,
  imports: [
    CommonModule,
    RouterModule,
    MatTableModule,
    MatPaginatorModule,
    MatSortModule,
    MatCardModule,
    MatIconModule,
    MatButtonModule,
    MatChipsModule,
    MatTooltipModule,
    BreadcrumbComponent,
    PageHeaderComponent
  ],
  templateUrl: './applications.component.html',
  styleUrls: ['./applications.component.css']
})
export class ApplicationsComponent {
  displayedColumns: string[] = ['nom', 'prenom', 'email', 'telephone', 'formule', 'dateInscription', 'status', 'actions'];
  dataSource: MatTableDataSource<Application>;
  
  @ViewChild(MatSort) sort!: MatSort;
  @ViewChild(MatPaginator) paginator!: MatPaginator;

  applications: Application[] = [
    {
      id: 1,
      nom: 'Dupont',
      prenom: 'Jean',
      email: 'jean.dupont@email.com',
      telephone: '0612345678',
      formule: 'Silver',
      dateInscription: new Date('2024-01-15'),
      status: 'En attente'
    },
    {
      id: 2,
      nom: 'Martin',
      prenom: 'Marie',
      email: 'marie.martin@email.com',
      telephone: '0687654321',
      formule: 'Gold',
      dateInscription: new Date('2024-01-16'),
      status: 'Approuvé'
    },
    {
      id: 3,
      nom: 'Bernard',
      prenom: 'Sophie',
      email: 'sophie.bernard@email.com',
      telephone: '0623456789',
      formule: 'Gold',
      dateInscription: new Date('2024-01-17'),
      status: 'Refusé'
    },
    {
      id: 4,
      nom: 'Petit',
      prenom: 'Thomas',
      email: 'thomas.petit@email.com',
      telephone: '0634567890',
      formule: 'Silver',
      dateInscription: new Date('2024-01-18'),
      status: 'Approuvé'
    },
    {
      id: 5,
      nom: 'Dubois',
      prenom: 'Claire',
      email: 'claire.dubois@email.com',
      telephone: '0645678901',
      formule: 'Gold',
      dateInscription: new Date('2024-01-19'),
      status: 'En attente'
    },
    {
      id: 6,
      nom: 'Moreau',
      prenom: 'Lucas',
      email: 'lucas.moreau@email.com',
      telephone: '0656789012',
      formule: 'Silver',
      dateInscription: new Date('2024-01-20'),
      status: 'Refusé'
    },
    {
      id: 7,
      nom: 'Roux',
      prenom: 'Emma',
      email: 'emma.roux@email.com',
      telephone: '0667890123',
      formule: 'Gold',
      dateInscription: new Date('2024-01-21'),
      status: 'Approuvé'
    }
  ];

  constructor() {
    this.dataSource = new MatTableDataSource(this.applications);
  }

  ngAfterViewInit() {
    this.dataSource.sort = this.sort;
    this.dataSource.paginator = this.paginator;
    
    setTimeout(() => {
      this.sort.sort({
        id: 'dateInscription',
        start: 'desc',
        disableClear: true
      });
    });

    this.dataSource.sortingDataAccessor = (item: Application, property: string): string | number => {
      switch(property) {
        case 'dateInscription': 
          return new Date(item.dateInscription).getTime();
        default: 
          return item[property as keyof Application]?.toString() || '';
      }
    };
  }

  getStatusClass(status: string): string {
    switch (status) {
      case 'En attente':
        return 'status-pending';
      case 'Approuvé':
        return 'status-approved';
      case 'Refusé':
        return 'status-rejected';
      default:
        return '';
    }
  }

  getStatusIcon(status: string): string {
    switch (status) {
      case 'En attente':
        return 'hourglass_empty';
      case 'Approuvé':
        return 'check_circle';
      case 'Refusé':
        return 'cancel';
      default:
        return '';
    }
  }
} 