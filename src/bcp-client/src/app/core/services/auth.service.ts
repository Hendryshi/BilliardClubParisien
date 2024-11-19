import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, map } from 'rxjs';
import { UserService } from '../../api/api/user.service';
import { LoginUserRequest } from '../../api/model/loginUserRequest';
import { LoginUserResponse } from '../../api/model/loginUserResponse';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private readonly TOKEN_KEY = 'auth_token';
  private isAuthenticatedSubject = new BehaviorSubject<boolean>(this.hasToken());
  isAuthenticated$ = this.isAuthenticatedSubject.asObservable();

  constructor(private userService: UserService) {}

  login(username: string, password: string): Observable<LoginUserResponse> {
    const loginRequest: LoginUserRequest = {
      userName: username,
      password: password
    };

    return this.userService.userLogin(loginRequest).pipe(
      map(response => {
        if (response.accessToken) {
          this.setToken(response.accessToken);
          this.isAuthenticatedSubject.next(true);
        }
        return response;
      })
    );
  }

  logout(): void {
    localStorage.removeItem(this.TOKEN_KEY);
    this.isAuthenticatedSubject.next(false);
  }

  getToken(): string | null {
    return localStorage.getItem(this.TOKEN_KEY);
  }

  private setToken(token: string): void {
    localStorage.setItem(this.TOKEN_KEY, token);
  }

  private hasToken(): boolean {
    return !!this.getToken();
  }
} 