import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { AuthService } from '../../../core/services/auth.service';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatCardModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatIconModule
  ],
  template: `
    <div class="login-container">
      <div class="header-section">
        <h1 class="page-title">Administration Connexion</h1>
      </div>

      <mat-card class="login-card">
        <mat-card-content>
          <form [formGroup]="loginForm" (ngSubmit)="onSubmit()">
            <div class="form-icon">
              <mat-icon>admin_panel_settings</mat-icon>
            </div>
            
            <mat-form-field appearance="outline">
              <mat-label>Identifiant</mat-label>
              <input matInput formControlName="username" type="text">
              <mat-icon matPrefix>person</mat-icon>
              <mat-error *ngIf="loginForm.get('username')?.hasError('required')">
                L'identifiant est obligatoire
              </mat-error>
            </mat-form-field>

            <mat-form-field appearance="outline">
              <mat-label>Mot de passe</mat-label>
              <input matInput [type]="hidePassword ? 'password' : 'text'" formControlName="password">
              <mat-icon matPrefix>lock</mat-icon>
              <button mat-icon-button matSuffix (click)="hidePassword = !hidePassword" type="button">
                <mat-icon>{{hidePassword ? 'visibility_off' : 'visibility'}}</mat-icon>
              </button>
              <mat-error *ngIf="loginForm.get('password')?.hasError('required')">
                Le mot de passe est obligatoire
              </mat-error>
            </mat-form-field>

            <button mat-raised-button color="primary" type="submit" [disabled]="!loginForm.valid">
              <mat-icon>login</mat-icon>
              <span>Se connecter</span>
            </button>
          </form>
        </mat-card-content>
      </mat-card>
    </div>
  `,
  styles: [`
    .login-container {
      padding: 24px 32px;
      width: 90%;
      margin: 0 auto;
    }

    .header-section {
      margin-bottom: 32px;
      text-align: center;
    }

    .page-title {
      font-size: 24px;
      font-weight: 600;
      color: #1a237e;
      margin: 0;
    }

    .login-card {
      width: 100%;
      max-width: 400px;
      margin: 0 auto;
      border-radius: 16px;
      box-shadow: 0 8px 32px rgba(0, 0, 0, 0.1);
      overflow: hidden;
    }

    mat-card-content {
      padding: 40px;
    }

    .form-icon {
      text-align: center;
      margin-bottom: 32px;
    }

    .form-icon mat-icon {
      font-size: 48px;
      width: 48px;
      height: 48px;
      color: #3f51b5;
    }

    form {
      display: flex;
      flex-direction: column;
      gap: 20px;
    }

    mat-form-field {
      width: 100%;
    }

    mat-form-field mat-icon {
      color: #666;
      margin-right: 8px;
    }

    button[type="submit"] {
      margin-top: 8px;
      height: 48px;
      font-size: 16px;
      font-weight: 500;
      border-radius: 24px;
      display: flex;
      align-items: center;
      justify-content: center;
      gap: 8px;
      background: linear-gradient(135deg, #3f51b5, #1a237e);
      transition: all 0.3s ease;
    }

    button[type="submit"]:hover:not([disabled]) {
      transform: translateY(-2px);
      box-shadow: 0 4px 12px rgba(26, 35, 126, 0.2);
    }

    button[type="submit"] mat-icon {
      font-size: 20px;
      width: 20px;
      height: 20px;
    }

    button[type="submit"][disabled] {
      background: linear-gradient(135deg, #9fa8da, #7986cb);
      cursor: not-allowed;
    }

    @media screen and (max-width: 599px) {
      .login-container {
        padding: 16px;
        width: 100%;
      }

      .header-section {
        margin-bottom: 24px;
      }

      .page-title {
        font-size: 20px;
      }

      mat-card-content {
        padding: 24px;
      }

      .form-icon {
        margin-bottom: 24px;
      }

      .form-icon mat-icon {
        font-size: 40px;
        width: 40px;
        height: 40px;
      }
    }
  `]
})
export class LoginComponent {
  loginForm: FormGroup;
  hidePassword = true;

  constructor(
    private fb: FormBuilder,
    private router: Router,
    private authService: AuthService
  ) {
    this.loginForm = this.fb.group({
      username: ['', Validators.required],
      password: ['', Validators.required]
    });
  }

  onSubmit() {
    if (this.loginForm.valid) {
      const { username, password } = this.loginForm.value;
      if (this.authService.login(username, password)) {
        this.router.navigate(['/admin/applications']);
      } else {
        console.error('Login failed');
      }
    }
  }
} 