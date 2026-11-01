import { Component, inject, signal } from '@angular/core';
import { AuthService } from '../../core/services/auth.service';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';

@Component({
  imports: [ReactiveFormsModule],
  selector: 'app-register-component',
  styleUrl: './register-component.css',
  templateUrl: './register-component.html',
})
export class RegisterComponent {
  private auth = inject(AuthService);
  private router = inject(Router);
  private fb = inject(FormBuilder);

  loading = signal(false);
  error = signal<string | null>(null);

  form = this.fb.nonNullable.group({
    personName: ['', Validators.required],
    email: ['', [Validators.required, Validators.email]],
    password: ['', [Validators.required, Validators.minLength(5)]],
    gender: ['Male', Validators.required],
  });

  submit() {
    if (this.form.invalid) {
      return;
    }
    this.loading.set(true);
    this.error.set(null);

    this.auth.register(this.form.getRawValue()).subscribe({
      next: (res) => {
        this.loading.set(false);
        if (res.success) {
          this.router.navigate(['/login']);
        } else {
          this.error.set('Registration failed');
        }
      },
      error: (err) => {
        this.loading.set(false);
        console.log('Register error:', err);

        const e = err.error;
        let msg = 'Something went wrong';

        if (e?.errors) {
          // validation errors: { Password: ["..."], Email: ["..."] }
          msg = Object.values(e.errors).flat().join(' | ');
        } else if (e?.message || e?.title || e?.detail) {
          msg = e.message ?? e.detail ?? e.title;
        } else if (typeof e === 'string') {
          msg = e;
        }

        this.error.set(msg);
      },
    });
  }
}
