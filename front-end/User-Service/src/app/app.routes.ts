import { Routes } from '@angular/router';
import { RegisterComponent } from './pages/register-component/register-component';
import { LoginComponent } from './pages/login-component/login-component';
import { HomeComponent } from './pages/home-component/home-component';
import { authGuard } from './core/guards/auth-guard';

export const routes: Routes = [
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'home', component: HomeComponent, canActivate: [authGuard] },
  { path: '', redirectTo: 'login', pathMatch: 'full' },
];
