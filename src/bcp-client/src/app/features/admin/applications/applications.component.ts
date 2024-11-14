import { Component, ViewChild, AfterViewInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatTableModule, MatTableDataSource } from '@angular/material/table';
import { MatPaginatorModule, MatPaginator } from '@angular/material/paginator';
import { MatSortModule, MatSort } from '@angular/material/sort';
import { MatCardModule } from '@angular/material/card';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { MatChipsModule } from '@angular/material/chips';
import { MatTooltipModule } from '@angular/material/tooltip';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { FormsModule } from '@angular/forms';
import { PageHeaderComponent } from '../../../shared/components/page-header/page-header.component';
import { BreadcrumbComponent } from '../../../shared/components/breadcrumb/breadcrumb.component';

interface Application {
  id: number;
  nom: string;
  prenom: string;
  email: string;
  telephone: string;
  formule: string;
  dateInscription: Date;
  status: 'En attente' | 'Approuvé' | 'Refusé';
  sexe: 'M' | 'F';
}

@Component({
  selector: 'app-applications',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    MatTableModule,
    MatPaginatorModule,
    MatSortModule,
    MatCardModule,
    MatIconModule,
    MatButtonModule,
    MatChipsModule,
    MatTooltipModule,
    MatFormFieldModule,
    MatInputModule,
    MatSelectModule,
    PageHeaderComponent,
    BreadcrumbComponent
  ],
  templateUrl: './applications.component.html',
  styleUrls: ['./applications.component.css']
})
export class ApplicationsComponent implements AfterViewInit {
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
      status: 'En attente',
      sexe: 'M'
    },
    {
      id: 2,
      nom: 'Martin',
      prenom: 'Marie',
      email: 'marie.martin@email.com',
      telephone: '0687654321',
      formule: 'Gold',
      dateInscription: new Date('2024-01-16'),
      status: 'Approuvé',
      sexe: 'F'
    },
    {
      id: 3,
      nom: 'Bernard',
      prenom: 'Sophie',
      email: 'sophie.bernard@email.com',
      telephone: '0623456789',
      formule: 'Gold',
      dateInscription: new Date('2024-01-17'),
      status: 'Refusé',
      sexe: 'F'
    },
    {
      id: 4,
      nom: 'Petit',
      prenom: 'Thomas',
      email: 'thomas.petit@email.com',
      telephone: '0634567890',
      formule: 'Silver',
      dateInscription: new Date('2024-01-18'),
      status: 'Approuvé',
      sexe: 'M'
    },
    {
      id: 5,
      nom: 'Dubois',
      prenom: 'Claire',
      email: 'claire.dubois@email.com',
      telephone: '0645678901',
      formule: 'Gold',
      dateInscription: new Date('2024-01-19'),
      status: 'En attente',
      sexe: 'F'
    },
    {
      id: 6,
      nom: 'Moreau',
      prenom: 'Lucas',
      email: 'lucas.moreau@email.com',
      telephone: '0656789012',
      formule: 'Silver',
      dateInscription: new Date('2024-01-20'),
      status: 'Refusé',
      sexe: 'M'
    },
    {
      id: 7,
      nom: 'Roux',
      prenom: 'Emma',
      email: 'emma.roux@email.com',
      telephone: '0667890123',
      formule: 'Gold',
      dateInscription: new Date('2024-01-21'),
      status: 'Approuvé',
      sexe: 'F'
    }
  ];

  statusFilter: string = '';
  genderFilter: string = '';
  searchText: string = '';

  constructor() {
    this.dataSource = new MatTableDataSource(this.applications);
    this.dataSource.filterPredicate = this.createFilter();
  }

  ngAfterViewInit() {
    this.dataSource.sort = this.sort;
    this.dataSource.paginator = this.paginator;
    
    this.dataSource.sortingDataAccessor = (item: Application, property: string): string | number => {
      switch(property) {
        case 'dateInscription': 
          return new Date(item.dateInscription).getTime();
        case 'status':
          const statusOrder = {
            'En attente': 0,
            'Approuvé': 1,
            'Refusé': 2
          };
          return statusOrder[item.status as keyof typeof statusOrder];
        default: 
          return item[property as keyof Application]?.toString() || '';
      }
    };

    setTimeout(() => {
      this.sort.sort({
        id: 'status',
        start: 'asc',
        disableClear: true
      });
    });
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

  applyFilter() {
    const filterValue = {
      search: this.searchText.trim().toLowerCase(),
      status: this.statusFilter,
      gender: this.genderFilter
    };
    this.dataSource.filter = JSON.stringify(filterValue);
  }

  private createFilter(): (data: any, filter: string) => boolean {
    return (data: any, filter: string): boolean => {
      const filterObj = JSON.parse(filter);
      
      const searchMatch = !filterObj.search || 
        Object.keys(data).some(key => {
          if (typeof data[key] === 'string') {
            return data[key].toLowerCase().includes(filterObj.search);
          }
          return false;
        });

      const statusMatch = !filterObj.status || data.status === filterObj.status;
      const genderMatch = !filterObj.gender || data.sexe === filterObj.gender;

      return searchMatch && statusMatch && genderMatch;
    };
  }

  clearFilters() {
    this.searchText = '';
    this.statusFilter = '';
    this.genderFilter = '';
    this.applyFilter();
  }
} 