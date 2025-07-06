import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Router, NavigationEnd } from '@angular/router';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatIconModule } from '@angular/material/icon';
import { filter } from 'rxjs/operators';
import { AuthService } from './core/services/auth.service';
import { ViewportScroller } from '@angular/common';
import { Title } from '@angular/platform-browser';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [
    CommonModule,
    RouterModule,
    MatToolbarModule,
    MatIconModule,
  ],
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  private router = inject(Router);
  private authService = inject(AuthService);
  private viewportScroller = inject(ViewportScroller);
  private titleService = inject(Title);
  
  isAuthenticated$ = this.authService.isAuthenticated$;

  constructor() {
    this.titleService.setTitle('BCParisien');
    // 监听路由变化
    this.router.events.pipe(
      filter(event => event instanceof NavigationEnd)
    ).subscribe(() => {
      // 滚动到顶部
      this.viewportScroller.scrollToPosition([0, 0]);
      
      // 确保内容区域也滚动到顶部
      setTimeout(() => {
        const contentElement = document.querySelector('.content');
        if (contentElement) {
          contentElement.scrollTop = 0;
        }
      }, 0);
    });
  }
}
