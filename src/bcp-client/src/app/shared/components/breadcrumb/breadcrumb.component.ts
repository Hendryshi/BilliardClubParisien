import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatIconModule } from '@angular/material/icon';
import { RouterModule } from '@angular/router';

export interface BreadcrumbItem {
  label: string;
  link?: string;
}

@Component({
  selector: 'app-breadcrumb',
  standalone: true,
  imports: [CommonModule, MatIconModule, RouterModule],
  template: `
    <div class="breadcrumb-container">
      <div class="breadcrumb">
        <ng-container *ngFor="let item of items; let last = last; let first = first">
          <a *ngIf="!last && item.link" 
             [routerLink]="item.link" 
             class="breadcrumb-item">
            <mat-icon *ngIf="first" class="home-icon">home</mat-icon>
            {{item.label}}
          </a>
          <span *ngIf="!last && !item.link" 
                class="breadcrumb-item">
            <mat-icon *ngIf="first" class="home-icon">home</mat-icon>
            {{item.label}}
          </span>
          <mat-icon *ngIf="!last" class="breadcrumb-separator">navigate_next</mat-icon>
          <span *ngIf="last" class="breadcrumb-item active">{{item.label}}</span>
        </ng-container>
      </div>
    </div>
  `,
  styles: [`
    .breadcrumb-container {
      background: linear-gradient(to right, #f8f9fa, transparent);
      border-radius: 12px;
      margin-bottom: 24px;
    }

    .breadcrumb {
      display: flex;
      align-items: center;
      padding: 16px 24px;
      overflow: hidden;
    }

    .breadcrumb-item {
      color: #666;
      text-decoration: none;
      font-size: 16px;
      font-weight: 500;
      display: flex;
      align-items: center;
      gap: 8px;
      padding: 8px 4px;
      border-radius: 6px;
      transition: all 0.3s ease;
    }

    .breadcrumb-item:hover {
      color: #1a237e;
      background-color: rgba(26, 35, 126, 0.05);
    }

    .breadcrumb-item.active {
      color: #1a237e;
      font-weight: 600;
      font-size: 18px;
      pointer-events: none;
    }

    .breadcrumb-separator {
      font-size: 24px;
      height: 24px;
      width: 24px;
      margin: 0 8px;
      color: #9e9e9e;
      opacity: 0.8;
    }

    .home-icon {
      color: #1a237e;
      opacity: 0.8;
      font-size: 20px;
      height: 20px;
      width: 20px;
    }

    @media (max-width: 599px) {
      .breadcrumb {
        padding: 12px 16px;
      }

      .breadcrumb-item {
        font-size: 14px;
      }

      .breadcrumb-item.active {
        font-size: 16px;
      }

      .breadcrumb-separator {
        font-size: 20px;
        height: 20px;
        width: 20px;
        margin: 0 4px;
      }
    }
  `]
})
export class BreadcrumbComponent {
  @Input() items: BreadcrumbItem[] = [];
} 