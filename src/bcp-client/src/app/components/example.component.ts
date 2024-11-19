import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';

interface Pet {
  id: number;
  name: string;
  // 添加其他属性...
}

@Component({
  selector: 'app-example',
  standalone: true,
  imports: [CommonModule],
  template: `
    <div *ngIf="pets$ | async as pets">
      <div *ngFor="let pet of pets">
        {{ pet.name }}
      </div>
    </div>
  `
})
export class ExampleComponent {
  private http = inject(HttpClient);
  pets$ = this.http.get<Pet[]>(`${environment.apiUrl}/pets`);
} 