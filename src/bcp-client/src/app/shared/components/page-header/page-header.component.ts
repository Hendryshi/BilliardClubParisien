import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatIconModule } from '@angular/material/icon';

@Component({
  selector: 'app-page-header',
  standalone: true,
  imports: [CommonModule, MatIconModule],
  template: `
    <div class="page-header">
      <div class="page-header-icon">
        <mat-icon>{{icon}}</mat-icon>
      </div>
      <div>
        <h1 class="page-title">{{title}}</h1>
        <div class="page-subtitle" *ngIf="subtitle">{{subtitle}}</div>
      </div>
    </div>
  `,
  styles: [`
    .page-header {
      margin-bottom: 32px;
      background: linear-gradient(135deg, #ffffff 0%, #f8f9fa 100%);
      padding: 24px;
      border-radius: 12px;
      box-shadow: 0 2px 8px rgba(0, 0, 0, 0.05);
      border-left: 4px solid #1a237e;
      display: flex;
      align-items: center;
      gap: 16px;
    }

    .page-header-icon {
      background: linear-gradient(135deg, #1a237e, #3949ab);
      color: white;
      width: 40px;
      height: 40px;
      border-radius: 10px;
      display: flex;
      align-items: center;
      justify-content: center;
    }

    .page-header-icon mat-icon {
      font-size: 24px;
      width: 24px;
      height: 24px;
    }

    .page-title {
      font-size: 24px;
      font-weight: 600;
      color: #1a237e;
      margin: 0;
      letter-spacing: 0.5px;
    }

    .page-subtitle {
      font-size: 14px;
      color: #666;
      margin-top: 4px;
    }

    @media screen and (max-width: 599px) {
      .page-header {
        padding: 16px;
        margin-bottom: 24px;
      }

      .page-header-icon {
        width: 32px;
        height: 32px;
      }

      .page-header-icon mat-icon {
        font-size: 20px;
        width: 20px;
        height: 20px;
      }

      .page-title {
        font-size: 20px;
      }

      .page-subtitle {
        font-size: 12px;
      }
    }
  `]
})
export class PageHeaderComponent {
  @Input() title: string = '';
  @Input() subtitle?: string;
  @Input() icon: string = 'article';
} 