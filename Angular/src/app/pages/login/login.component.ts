import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, Router } from '@angular/router';
import { UntypedFormBuilder, UntypedFormGroup, FormsModule, Validators, ReactiveFormsModule, FormGroup } from '@angular/forms';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatCardModule } from '@angular/material/card';
import { AuthService } from '../../core/services/auth.service';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    MatFormFieldModule,
    MatInputModule,
    ReactiveFormsModule,
    MatCardModule,
    MatButtonModule],
  templateUrl: './login.component.html',
})
export class LoginComponent implements OnInit {

  form: UntypedFormGroup;
  constructor(private fb: UntypedFormBuilder, private authService: AuthService) {
  }

  ngOnInit(): void {
    this.createForm();
  }

  createForm() : void {    
    this.form = this.fb.group({
        username : ['bob@bob.com', Validators.required],
        password : ['abc123', Validators.required],
      });
  }

  signIn() : void {
    console.log(this.form.value.username);
    this.authService
      .signIn(this.form.value.username, this.form.value.password)
      .subscribe(tokenData => console.log(tokenData));
  }

  signInBob(): void {
    this.authService
    .signIn('bob@bob.com', 'abc123')
    .subscribe(tokenData => console.log(tokenData));
  }

  signInWithUserEndpoint() : void {
    console.log(this.form.value.username);
    this.authService
      .signInWithUserEndpoint(this.form.value.username, this.form.value.password)
      .subscribe(tokenData => console.log('Token', tokenData));

  }

}
