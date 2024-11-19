import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, Router } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatChipsModule } from '@angular/material/chips';
import { MatDividerModule } from '@angular/material/divider';
import { MatSnackBar } from '@angular/material/snack-bar';
import { InscriptionService } from '../../../api/api/inscription.service';
import { InscriptionResponse } from '../../../api/model/inscriptionResponse';

@Component({
  selector: 'app-application-detail',
  standalone: true,
  imports: [
    CommonModule,
    MatCardModule,
    MatButtonModule,
    MatIconModule,
    MatChipsModule,
    MatDividerModule
  ],
  templateUrl: './application-detail.component.html',
  styleUrls: ['./application-detail.component.css']
})
export class ApplicationDetailComponent implements OnInit {
  application: InscriptionResponse | null = null;
  isLoading = true;
  error: string | null = null;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private inscriptionService: InscriptionService,
    private snackBar: MatSnackBar
  ) {}

  ngOnInit() {
    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.loadApplication(parseInt(id, 10));
    }
  }

  private loadApplication(id: number) {
    this.isLoading = true;
    this.error = null;

    this.inscriptionService.inscriptionGet(id)
      .subscribe({
        next: (response) => {
          if (response.data) {
            this.application = {
              ...response.data,
              motivation: response.data.motivation || 'Aucune motivation fournie'
            };
          } else {
            this.application = null;
            this.error = 'Demande non trouvée';
          }
          this.isLoading = false;
        },
        error: (error) => {
          console.error('Error loading application:', error);
          this.error = 'Une erreur est survenue lors du chargement des données';
          this.isLoading = false;
          this.application = null;
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

  getStatusDesc(status: string): string {
    switch (status) {
      case 'PENDING':
        return 'En attente';
      case 'APPROVED':
        return 'Approuvé';
      case 'REJECTED':
        return 'Refusé';
      default:
        return status;
    }
  }

  getFormuleClass(formule: string): string {
    return formule.toLowerCase();
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

  approveApplication() {
    if (this.application?.id) {
      console.log('Approving application:', this.application.id);
    }
  }

  rejectApplication() {
    if (this.application?.id) {
      console.log('Rejecting application:', this.application.id);
    }
  }
} 