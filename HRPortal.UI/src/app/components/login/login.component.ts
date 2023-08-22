import { Validators, FormBuilder, FormGroup } from '@angular/forms';
import { Component } from '@angular/core';
import { UserAuthService } from 'src/services/user-auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
})
export class LoginComponent {
  userLoginForm: FormGroup = new FormGroup({});
  submitted = false;
  apiErrorMessages: string[] = [];
  loginSuccess = false;
  invalidLogin = false;

  constructor(
    private formBuilder: FormBuilder,
    private userService: UserAuthService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.initializeForm();
  }

  initializeForm() {
    this.userLoginForm = this.formBuilder.group({
      email: ['', [Validators.required]],
      password: ['', [Validators.required]],
    });
  }

  submitForm() {
    this.submitted = true;
    this.apiErrorMessages = [];

    if (this.userLoginForm.valid) {
      this.userService.loginUser(this.userLoginForm.value).subscribe({
        next: (res: any) => {
          if (res && res.token) {
            console.log(res.token);
            localStorage.setItem('token', res.token);
            this.invalidLogin = false;
            this.loginSuccess = true;
            this.userLoginForm.reset();
            this.submitted = false;
            console.log('User login successfully');
          }
          this.router.navigate(['/']);
        },
        error: (error) => {
          if (error.status === 400) {
            this.invalidLogin = true;
            this.loginSuccess = false;
            this.apiErrorMessages.push('Invalid email or password');
          } else {
            console.error('An error occurred:', error);
          }
          this.submitted = false;
        },
      });
    } else {
      this.apiErrorMessages.push('Please provide all required fields');
    }
  }
}
