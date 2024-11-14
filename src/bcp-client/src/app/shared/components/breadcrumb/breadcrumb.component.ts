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
    <div class="breadcrumb hide-on-mobile">
      <ng-container *ngFor="let item of items; let last = last">
        <a *ngIf="!last && item.link" 
           [routerLink]="item.link" 
           class="breadcrumb-item">
          {{item.label}}
        </a>
        <span *ngIf="!last && !item.link" 
              class="breadcrumb-item">
          {{item.label}}
        </span>
        <mat-icon *ngIf="!last" class="breadcrumb-separator">chevron_right</mat-icon>
        <span *ngIf="last" class="breadcrumb-item active">{{item.label}}</span>
      </ng-container>
    </div>
  `,
  styles: [`
    .breadcrumb {
      display: flex;
      align-items: center;
      margin-bottom: 24px;
      color: #666;
      font-size: 14px;
    }

    .breadcrumb-item {
      color: #666;
      text-decoration: none;
      font-size: 14px;
      font-weight: 500;
      padding: 8px 0;
    }

    .breadcrumb-item:hover {
      color: #1a237e;
      text-decoration: underline;
    }

    .breadcrumb-item.active {
      color: #1a237e;
      font-weight: 600;
      font-size: 14px;
    }

    .breadcrumb-separator {
      font-size: 18px;
      height: 18px;
      width: 18px;
      margin: 0 8px;
      color: #666;
    }

    @media screen and (max-width: 599px) {
      .hide-on-mobile {
        display: none;
      }
    }
  `]
})
export class BreadcrumbComponent {
  @Input() items: BreadcrumbItem[] = [];
} 