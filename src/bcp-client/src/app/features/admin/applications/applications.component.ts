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
import { InscriptionService } from '../../../api/api/inscription.service';
import { InscriptionResponse } from '../../../api/model/inscriptionResponse';

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
  dataSource: MatTableDataSource<any>;
  
  @ViewChild(MatSort) sort!: MatSort;
  @ViewChild(MatPaginator) paginator!: MatPaginator;

  statusFilter: string = '';
  genderFilter: string = '';
  searchText: string = '';

  constructor(private inscriptionService: InscriptionService) {
    this.dataSource = new MatTableDataSource();
    this.dataSource.filterPredicate = this.createFilter();
  }

  ngAfterViewInit() {
    this.dataSource.sort = this.sort;
    this.dataSource.paginator = this.paginator;
    
    this.dataSource.sortingDataAccessor = (item: any, property: string) => {
      switch(property) {
        case 'fullName':
          return `${item.firstName} ${item.lastName}`.toLowerCase();
        case 'status':
          return item.status;
        default: 
          return item[property];
      }
    };

    this.loadApplications();
  }

  private loadApplications() {
    this.inscriptionService.inscriptionGetAllInscriptions()
      .subscribe({
        next: (response) => {
          const inscriptions = Array.isArray(response.data) ? response.data : [response.data];
          
          const transformedData = inscriptions
            .filter((item): item is InscriptionResponse => item !== null)
            .map(item => ({
              ...item,
              prenom: item.firstName,
              nom: item.lastName,
              formule: item.formula,
              previousMember: item.isMemberBefore ? 'Oui' : 'Non'
            }))
            .sort((a, b) => {
              if (a.status === 'PENDING' && b.status !== 'PENDING') return -1;
              if (a.status !== 'PENDING' && b.status === 'PENDING') return 1;
              
              const dateA = new Date(a.dtCreate || '').getTime();
              const dateB = new Date(b.dtCreate || '').getTime();
              return dateB - dateA;
            });
          
          this.dataSource.data = transformedData;
        },
        error: (error) => {
          console.error('Error loading applications:', error);
        }
      });
  }

  getStatusClass(status: string): string {
    switch (status) {
      case 'PENDING':
        return 'status-pending';
      case 'APPROVED':
        return 'status-approved';
      case 'REJECTED':
        return 'status-rejected';
      default:
        return '';
    }
  }

  getStatusDesc(status: string): string {
    switch (status) {
      case 'PENDING':
        return 'En attente';
      case 'APPROVED':
        return 'Approuvé';
      case 'REJECTED':
        return 'Refusé';
      default:
        return '';
    }
  }

  getStatusIcon(status: string): string {
    switch (status) {
      case 'PENDING':
        return 'hourglass_empty';
      case 'APPROVED':
        return 'check_circle';
      case 'REJECTED':
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

      const statusMatch = !filterObj.status || 
        this.getStatusDesc(data.status) === filterObj.status;

      return searchMatch && statusMatch;
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