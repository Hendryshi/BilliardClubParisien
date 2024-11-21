import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatRadioModule } from '@angular/material/radio';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatExpansionModule } from '@angular/material/expansion';
import { MatIconModule } from '@angular/material/icon';
import { Router } from '@angular/router';
import { trigger, state, style, transition, animate } from '@angular/animations';
import { MatSnackBar, MatSnackBarConfig } from '@angular/material/snack-bar';
import { InscriptionService } from '../../api/api/inscription.service';
import { InscriptionCommand } from '../../api/model/inscriptionCommand';
import { InscriptionImageCommand } from '../../api/model/inscriptionImageCommand';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';

interface Genre {
  value: string;
  viewValue: string;
}

interface Formule {
  value: string;
  viewValue: string;
}

interface Categorie {
  value: string;
  viewValue: string;
}

interface PhotoUpload {
  file?: File;
  preview?: string;
}

@Component({
  selector: 'app-inscription',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatSelectModule,
    MatButtonModule,
    MatCardModule,
    MatRadioModule,
    MatCheckboxModule,
    MatExpansionModule,
    MatIconModule,
    MatProgressSpinnerModule
  ],
  templateUrl: './inscription.component.html',
  styleUrls: ['./inscription.component.css'],
  animations: [
    trigger('expandCollapse', [
      state('collapsed', style({
        height: '0',
        overflow: 'hidden',
        opacity: '0',
        padding: '0'
      })),
      state('expanded', style({
        height: '*',
        overflow: 'hidden',
        opacity: '1'
      })),
      transition('collapsed <=> expanded', [
        animate('300ms ease-in-out')
      ])
    ])
  ]
})
export class InscriptionComponent {
  inscriptionForm: FormGroup;
  maxLength = 1000;
  isFormulesExpanded = false;
  isExpanded = false;
  isSubmitting = false;

  genres: Genre[] = [
    { value: 'M', viewValue: 'Masculin' },
    { value: 'F', viewValue: 'Féminin' }
  ];

  formules: Formule[] = [
    { value: 'Silver', viewValue: 'Formule « Silver »' },
    { value: 'Gold', viewValue: 'Formule « Gold »' }
  ];

  categories: Categorie[] = [
    { value: 'junior', viewValue: 'Catégorie Junior (-23 ans et moins)' },
    { value: 'mixte', viewValue: 'Catégorie Mixte' },
    { value: 'feminine', viewValue: 'Catégorie Féminine' },
    { value: 'regional', viewValue: 'Niveau Régional' },
    { value: 'national', viewValue: 'Niveau National' }
  ];

  photos: PhotoUpload[] = [
    { file: undefined, preview: '' },
    { file: undefined, preview: '' }
  ];

  constructor(
    private fb: FormBuilder,
    private inscriptionService: InscriptionService,
    private snackBar: MatSnackBar,
    private router: Router
  ) {
    this.inscriptionForm = this.fb.group({
      nom: ['', Validators.required],
      prenom: ['', Validators.required],
      sexe: ['', Validators.required],
      telephone: ['', [Validators.required, Validators.pattern('^[0-9]{10}$')]],
      email: ['', [Validators.required, Validators.email]],
      previousMember: ['', Validators.required],
      formule: ['', Validators.required],
      competition: ['', Validators.required],
      categories: this.fb.group({
        junior: [false],
        mixte: [false],
        feminine: [false],
        regional: [false],
        national: [false]
      }),
      motivation: ['', [Validators.maxLength(this.maxLength)]]
    });
  }

  toggleFormules() {
    this.isFormulesExpanded = !this.isFormulesExpanded;
  }

  getRemainingChars(): number {
    const motivation = this.inscriptionForm.get('motivation')?.value || '';
    return this.maxLength - motivation.length;
  }

  showCategories(): boolean {
    return this.inscriptionForm.get('competition')?.value === 'oui';
  }

  onSubmit() {
    if (this.inscriptionForm.valid && !this.isSubmitting) {
      this.isSubmitting = true;
      const formValue = this.inscriptionForm.value;

      const selectedCategories = Object.entries(formValue.categories || {})
        .filter(([_, selected]) => selected)
        .map(([category]) => category.toUpperCase());

      // 准备图片数据
      const images: InscriptionImageCommand[] = this.photos
        .filter(photo => photo.preview) // 只处理有预览的照片
        .map(photo => ({
          imageData: photo.preview?.split(',')[1] || null // 移除 data:image/xxx;base64, 前缀
        }));

      const inscriptionCommand: InscriptionCommand = {
        firstName: formValue.prenom,
        lastName: formValue.nom,
        email: formValue.email,
        phone: formValue.telephone,
        sex: formValue.sexe,
        status: 'PENDING',
        isMemberBefore: formValue.previousMember === 'oui',
        formula: formValue.formule,
        joinCompetition: formValue.competition === 'oui',
        competitionCats: selectedCategories,
        motivation: formValue.motivation || '',
        inscriptionImages: images
      };

      const successConfig: MatSnackBarConfig = {
        duration: 3000,
        panelClass: ['success-snackbar'],
        horizontalPosition: 'center',
        verticalPosition: 'top'
      };

      const errorConfig: MatSnackBarConfig = {
        duration: 3000,
        panelClass: ['error-snackbar'],
        horizontalPosition: 'center',
        verticalPosition: 'top'
      };

      this.inscriptionService.inscriptionCreate({data: inscriptionCommand})
        .subscribe({
          next: () => {
            this.inscriptionForm.reset();
            this.photos = [
              { file: undefined, preview: '' },
              { file: undefined, preview: '' }
            ];
            this.router.navigate(['/home']).then(() => {
              setTimeout(() => {
                this.snackBar.open(
                  '🎉 Félicitations! Votre inscription a été enregistrée avec succès!',
                  '',
                  successConfig
                );
              }, 100);
            });
            this.isSubmitting = false;
          },
          error: (error) => {
            console.error('Inscription error:', error);
            this.snackBar.open(
              '❌ Une erreur est survenue lors de l\'inscription. Veuillez réessayer ou contacter le support si le problème persiste.',
              '',
              errorConfig
            );
            this.isSubmitting = false;
          }
        });
    }
  }

  toggleDetails() {
    this.isExpanded = !this.isExpanded;
  }

  onFileSelected(event: Event, index: number) {
    const input = event.target as HTMLInputElement;
    if (input.files && input.files[0]) {
      const file = input.files[0];

      // 检查文件大小（5MB限制）
      if (file.size > 5 * 1024 * 1024) {
        this.snackBar.open('La taille du fichier ne doit pas dépasser 5MB', '', {
          duration: 3000,
          panelClass: ['error-snackbar']
        });
        return;
      }

      // 检查文件类型
      if (!file.type.startsWith('image/')) {
        this.snackBar.open('Seuls les fichiers image sont acceptés', '', {
          duration: 3000,
          panelClass: ['error-snackbar']
        });
        return;
      }

      // 创建预览
      const reader = new FileReader();
      reader.onload = (e) => {
        this.photos[index] = {
          file: file,
          preview: e.target?.result as string
        };
      };
      reader.readAsDataURL(file);
    }
  }

  removePhoto(index: number) {
    this.photos[index] = { file: undefined, preview: '' };
  }
} 