import { Component, inject, ViewChild } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Router, NavigationEnd } from '@angular/router';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatButtonModule } from '@angular/material/button';
import { MatSidenavModule, MatSidenav } from '@angular/material/sidenav';
import { MatIconModule } from '@angular/material/icon';
import { MatListModule } from '@angular/material/list';
import { BreakpointObserver, Breakpoints } from '@angular/cdk/layout';
import { Observable } from 'rxjs';
import { map, shareReplay, filter } from 'rxjs/operators';
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
    MatButtonModule,
    MatSidenavModule,
    MatIconModule,
    MatListModule,
  ],
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  @ViewChild('sidenav') sidenav!: MatSidenav;
  
  private breakpointObserver = inject(BreakpointObserver);
  private router = inject(Router);
  private authService = inject(AuthService);
  private viewportScroller = inject(ViewportScroller);
  private titleService = inject(Title);
  
  sidenavOpened = false;
  isAuthenticated$ = this.authService.isAuthenticated$;

  isHandset$: Observable<boolean> = this.breakpointObserver.observe(Breakpoints.Handset)
    .pipe(
      map(result => result.matches),
      shareReplay()
    );

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
        
        // 确保 sidenav-content 也滚动到顶部
        const sidenavContent = document.querySelector('mat-sidenav-content');
        if (sidenavContent) {
          sidenavContent.scrollTop = 0;
        }
      }, 0);
    });
  }

  handleMenuClick(): void {
    if (this.breakpointObserver.isMatched(Breakpoints.Handset)) {
      this.sidenav.close(); 
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
