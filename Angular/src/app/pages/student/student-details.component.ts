import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, Router } from '@angular/router';
import { Location } from '@angular/common';
import { StudentService } from './student.service';
import { Student } from './student';
import { UntypedFormBuilder, UntypedFormGroup, FormsModule, Validators, ReactiveFormsModule, FormGroup } from '@angular/forms';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatCardModule } from '@angular/material/card';

@Component({
  selector: 'app-student-details',
  standalone: true,
  imports:
    [
      CommonModule,
      FormsModule,
      MatFormFieldModule,
      MatInputModule,
      ReactiveFormsModule,
      MatCardModule,
      MatButtonModule],
  templateUrl: './student-details.component.html',
  styleUrl: './student-details.component.css'
})
export class StudentDetailsComponent implements OnInit {
  student: Student;
  isNew = false;
  title = '';
  studentForm: UntypedFormGroup;

  constructor(
    private route: ActivatedRoute,
    private studentService: StudentService,
    private location: Location,
    private router: Router,
    private fb: UntypedFormBuilder
  ) {
  }


  ngOnInit(): void {
    const id = parseInt(this.route.snapshot.paramMap.get('id')!);
    this.isNew = Number.isNaN(id);
    console.log('StudentId ' + id);
    if (this.isNew) {
      this.title = 'Create new student';
      this.student = {} as Student;
      this.initializeForm();
    }
    else {
      this.getStudent(id);
    }
  }

  initializeForm() {
    this.studentForm = this.fb.group({
      firstName: [
        {
          value: this.student.firstName,
          disabled: false,
        },
        [Validators.required, Validators.maxLength(64)],
      ],
      lastName: [
        {
          value: this.student.lastName,
          disabled: false,
        },
        [Validators.required, Validators.maxLength(64)],
      ]
    });
    console.log(this.studentForm);
  }

  save(): void {
    
    if (this.studentForm.valid) {
      this.student.firstName = this.studentForm.controls['firstName'].value;
      this.student.lastName = this.studentForm.controls['lastName'].value;
      console.log('form is valid');
      if (this.isNew) {
        console.log('creating student');
        this.studentService.createStudent(this.student)
          .subscribe(_ => {console.log('navigating'); this.router.navigateByUrl('student')});
      }
      else {
        this.studentService.updateStudent(this.student)
          .subscribe(x => {
            this.student = x;
          });
      }
    }
  }
  getStudent(id: number): void {
    this.studentService.getStudent(id)
      .subscribe(student => {
        this.student = student;
        this.initializeForm();
      })
  }

  delete() {
    if (!this.isNew) {
      this.studentService
        .deleteStudent(this.student.id)
        .subscribe(_ => this.router.navigateByUrl('student'));
    }
  }

  goBack(): void {
    this.location.back();
  }
}
