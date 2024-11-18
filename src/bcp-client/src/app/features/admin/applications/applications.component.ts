import { Component, ViewChild, AfterViewInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
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

interface Application {
  id: number;
  nom: string;
  prenom: string;
  formule: string;
  status: 'En attente' | 'Approuvé' | 'Refusé';
  previousMember: 'Oui' | 'Non';
}

@Component({
  selector: 'app-applications',
  standalone: true,
  imports: [
    CommonModule,
    RouterModule,
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
    MatSelectModule
  ],
  templateUrl: './applications.component.html',
  styleUrls: ['./applications.component.css']
})
export class ApplicationsComponent implements AfterViewInit {
  displayedColumns: string[] = ['fullName', 'status', 'formule', 'previousMember', 'actions'];
  dataSource: MatTableDataSource<Application>;
  
  @ViewChild(MatSort) sort!: MatSort;
  @ViewChild(MatPaginator) paginator!: MatPaginator;

  applications: Application[] = [
    {
      id: 1,
      nom: 'Dupont',
      prenom: 'Jean',
      formule: 'Silver',
      status: 'En attente',
      previousMember: 'Oui'
    },
    {
      id: 2,
      nom: 'Martin',
      prenom: 'Marie',
      formule: 'Gold',
      status: 'Approuvé',
      previousMember: 'Non'
    },
    {
      id: 3,
      nom: 'Bernard',
      prenom: 'Sophie',
      formule: 'Gold',
      status: 'Refusé',
      previousMember: 'Non'
    },
    {
      id: 4,
      nom: 'Petit',
      prenom: 'Thomas',
      formule: 'Silver',
      status: 'Approuvé',
      previousMember: 'Oui'
    },
    {
      id: 5,
      nom: 'Dubois',
      prenom: 'Claire',
      formule: 'Gold',
      status: 'En attente',
      previousMember: 'Non'
    },
    {
      id: 6,
      nom: 'Moreau',
      prenom: 'Lucas',
      formule: 'Silver',
      status: 'Refusé',
      previousMember: 'Oui'
    },
    {
      id: 7,
      nom: 'Roux',
      prenom: 'Emma',
      formule: 'Gold',
      status: 'Approuvé',
      previousMember: 'Non'
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
        case 'fullName':
          return `${item.prenom} ${item.nom}`.toLowerCase();
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

  getFormuleIcon(formule: string): string {
    switch (formule.toLowerCase()) {
      case 'gold':
        return 'stars';
      case 'silver':
        return 'star_half';
      default:
        return 'star_outline';
    }
  }

  getFormuleTooltip(formule: string): string {
    switch (formule.toLowerCase()) {
      case 'gold':
        return 'Formule Gold - Accès illimité 7j/7, réservation 48h à l\'avance';
      case 'silver':
        return 'Formule Silver - Accès en semaine, réservation 24h à l\'avance';
      default:
        return 'Formule inconnue';
    }
  }
} 