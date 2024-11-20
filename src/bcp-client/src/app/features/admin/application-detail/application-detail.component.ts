import { Component, OnInit, Inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, Router } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatChipsModule } from '@angular/material/chips';
import { MatDividerModule } from '@angular/material/divider';
import { MatSnackBar, MatSnackBarConfig, MatSnackBarModule } from '@angular/material/snack-bar';
import { MatDialog, MatDialogModule, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { InscriptionService } from '../../../api/api/inscription.service';
import { InscriptionResponse } from '../../../api/model/inscriptionResponse';
import { ConfirmDialogComponent } from '../../../shared/components/confirm-dialog/confirm-dialog.component';
import { firstValueFrom } from 'rxjs';
import { UpdateInscriptionRequest } from '../../../api/model/updateInscriptionRequest';
import { ImagePreviewDialogComponent } from '../../../shared/components/image-preview-dialog/image-preview-dialog.component';

@Component({
  selector: 'app-application-detail',
  standalone: true,
  imports: [
    CommonModule,
    MatCardModule,
    MatButtonModule,
    MatIconModule,
    MatChipsModule,
    MatDividerModule,
    MatDialogModule,
    MatSnackBarModule,
    ConfirmDialogComponent,
    ImagePreviewDialogComponent
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
    private snackBar: MatSnackBar,
    private dialog: MatDialog
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

  async approveApplication() {
    if (!this.application?.id) {
      const errorConfig: MatSnackBarConfig = {
        duration: 3000,
        panelClass: ['error-snackbar'],
        horizontalPosition: 'center',
        verticalPosition: 'top'
      };

      this.snackBar.open(
        '❌ Erreur: ID de la demande manquant',
        '',
        errorConfig
      );
      return;
    }

    const dialogRef = this.dialog.open(ConfirmDialogComponent, {
      width: '300px',
      data: {
        title: 'Confirmer l\'approbation',
        message: 'Êtes-vous sûr de vouloir approuver cette demande ?',
        confirmText: 'Approuver',
        cancelText: 'Annuler'
      }
    });

    const result = await firstValueFrom(dialogRef.afterClosed());
    if (result) {
      try {
        const updateRequest: UpdateInscriptionRequest = {
          data: {
            status: 'APPROVED',
            firstName: this.application?.firstName || '',
            lastName: this.application?.lastName || '',
            sex: this.application?.sex || '',
            email: this.application?.email || ''
          }
        };

        await firstValueFrom(
          this.inscriptionService.inscriptionPatch(
            this.application.id,
            updateRequest
          )
        );
        
        this.router.navigate(['/admin/applications']);
      } catch (error: any) {
        console.error('Error approving application:', error);
        
        const errorConfig: MatSnackBarConfig = {
          duration: 3000,
          panelClass: ['error-snackbar'],
          horizontalPosition: 'center',
          verticalPosition: 'top'
        };

        let errorMessage = '❌ Une erreur est survenue lors de l\'approbation';
      
        this.snackBar.open(
          errorMessage,
          '',
          errorConfig
        );
      }
    }
  }

  async rejectApplication() {
    if (!this.application?.id) {
      const errorConfig: MatSnackBarConfig = {
        duration: 3000,
        panelClass: ['error-snackbar'],
        horizontalPosition: 'center',
        verticalPosition: 'top'
      };

      this.snackBar.open(
        '❌ Erreur: ID de la demande manquant',
        '',
        errorConfig
      );
      return;
    }

    const dialogRef = this.dialog.open(ConfirmDialogComponent, {
      width: '300px',
      data: {
        title: 'Confirmer le refus',
        message: 'Êtes-vous sûr de vouloir refuser cette demande ?',
        confirmText: 'Refuser',
        cancelText: 'Annuler'
      }
    });

    const result = await firstValueFrom(dialogRef.afterClosed());
    if (result) {
      try {
        const updateRequest: UpdateInscriptionRequest = {
          data: {
            status: 'REJECTED',
            firstName: this.application?.firstName || '',
            lastName: this.application?.lastName || '',
            sex: this.application?.sex || '',
            email: this.application?.email || ''
          }
        };

        await firstValueFrom(
          this.inscriptionService.inscriptionPatch(
            this.application.id,
            updateRequest
          )
        );

        this.router.navigate(['/admin/applications']);
      } catch (error: any) {
        console.error('Error rejecting application:', error);
        
        const errorConfig: MatSnackBarConfig = {
          duration: 3000,
          panelClass: ['error-snackbar'],
          horizontalPosition: 'center',
          verticalPosition: 'top'
        };

        let errorMessage = '❌ Une erreur est survenue lors du refus';
        
        this.snackBar.open(
          errorMessage,
          '',
          errorConfig
        );
      }
    }
  }

  openImagePreview(image: any) {
    this.dialog.open(ImagePreviewDialogComponent, {
      data: {
        imageUrl: 'data:image/jpeg;base64,' + image.imageData
      },
      maxWidth: '95vw',
      maxHeight: '95vh',
      panelClass: 'image-preview-dialog-container',
      backdropClass: 'image-preview-backdrop'
    });
  }
} 