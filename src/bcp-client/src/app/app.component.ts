import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Router, NavigationEnd } from '@angular/router';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatButtonModule } from '@angular/material/button';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatIconModule } from '@angular/material/icon';
import { MatListModule } from '@angular/material/list';
import { BreakpointObserver, Breakpoints } from '@angular/cdk/layout';
import { Observable } from 'rxjs';
import { map, shareReplay, filter } from 'rxjs/operators';
import { AuthService } from './core/services/auth.service';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [
    CommonModule,
    RouterModule,
    MatToolbarModule,
    MatButtonModule,
    MatSidenavModule,
    MatIconModule,
    MatListModule,
  ],
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  private breakpointObserver = inject(BreakpointObserver);
  private router = inject(Router);
  private authService = inject(AuthService);
  
  sidenavOpened = false;
  isAuthenticated$ = this.authService.isAuthenticated$;

  isHandset$: Observable<boolean> = this.breakpointObserver.observe(Breakpoints.Handset)
    .pipe(
      map(result => result.matches),
      shareReplay()
    );

  constructor() {
    // 监听路由变化
    this.router.events.pipe(
      filter(event => event instanceof NavigationEnd)
    ).subscribe((event: any) => {
      // 检查是否是移动端
      if (this.breakpointObserver.isMatched(Breakpoints.Handset)) {
        // 滚动到顶部
        window.scrollTo(0, 0);
        // 确保内容区域也滚动到顶部
        const contentElement = document.querySelector('.content');
        if (contentElement) {
          contentElement.scrollTop = 0;
        }
        // 如果是首页，确保主内容区域也滚动到顶部
        if (event.url === '/' || event.url === '/home') {
          setTimeout(() => {
            window.scrollTo(0, 0);
            document.body.scrollTop = 0;
            document.documentElement.scrollTop = 0;
            const mainContent = document.querySelector('mat-sidenav-content');
            if (mainContent) {
              mainContent.scrollTop = 0;
            }
          }, 100);
        }
      }
    });
  }

  handleMenuClick(sidenav: any): void {
    if (this.breakpointObserver.isMatched(Breakpoints.Handset)) {
      sidenav.close();
    }
  }

  toggleSidenav(): void {
    this.sidenavOpened = !this.sidenavOpened;
  }

  logout(): void {
    this.authService.logout();
    this.router.navigate(['/']);
  }
}
