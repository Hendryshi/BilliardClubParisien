import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [
    CommonModule,
    MatCardModule,
    MatButtonModule,
    MatIconModule,
    RouterModule
  ],
  template: `
    <div class="home-container">
      <div class="header-section">
        <h1 class="page-title">Billiard Club Parisien</h1>
      </div>

      <mat-card class="home-card">
        <mat-card-content>
          <div class="welcome-section">
            <mat-icon class="welcome-icon">sports_bar</mat-icon>
            <h2>Bienvenue au Club de Billard</h2>
            <p>Rejoignez notre club et profitez de nos installations de qualité.</p>
          </div>

          <div class="actions-section">
            <button mat-raised-button color="primary" routerLink="/inscription">
              <mat-icon>how_to_reg</mat-icon>
              <span>Demande d'Inscription</span>
            </button>
          </div>
        </mat-card-content>
      </mat-card>
    </div>
  `,
  styles: [`
    .home-container {
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

    .home-card {
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

    .welcome-section {
      text-align: center;
      margin-bottom: 32px;
    }

    .welcome-icon {
      font-size: 48px;
      width: 48px;
      height: 48px;
      color: #3f51b5;
      margin-bottom: 24px;
    }

    .welcome-section h2 {
      color: #1a237e;
      font-size: 20px;
      font-weight: 500;
      margin: 0 0 16px 0;
    }

    .welcome-section p {
      color: #666;
      font-size: 16px;
      line-height: 1.5;
      margin: 0;
    }

    .actions-section {
      display: flex;
      justify-content: center;
    }

    button {
      height: 48px;
      font-size: 16px;
      font-weight: 500;
      border-radius: 24px;
      padding: 0 32px;
      display: flex;
      align-items: center;
      gap: 8px;
      background: linear-gradient(135deg, #3f51b5, #1a237e);
      transition: all 0.3s ease;
    }

    button:hover {
      transform: translateY(-2px);
      box-shadow: 0 4px 12px rgba(26, 35, 126, 0.2);
    }

    button mat-icon {
      font-size: 20px;
      width: 20px;
      height: 20px;
    }

    @media screen and (max-width: 599px) {
      .home-container {
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

      .welcome-icon {
        font-size: 40px;
        width: 40px;
        height: 40px;
        margin-bottom: 20px;
      }

      .welcome-section h2 {
        font-size: 18px;
      }

      .welcome-section p {
        font-size: 14px;
      }

      button {
        width: 100%;
      }
    }
  `]
})
export class HomeComponent {} 