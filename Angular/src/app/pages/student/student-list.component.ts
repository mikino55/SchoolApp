import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { StudentService } from './student.service';
import { Student } from './student';
import { RouterLink, Router } from '@angular/router';
import {MatTableModule} from '@angular/material/table';
import { ReactiveFormsModule, FormBuilder, Validators } from '@angular/forms';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatSelectModule } from '@angular/material/select';
import { MatRadioModule } from '@angular/material/radio';
import { MatCardModule } from '@angular/material/card';


@Component({
  selector: 'app-student-list',
  standalone: true,
  imports: [CommonModule, RouterLink, MatTableModule, MatButtonModule],
  templateUrl: './student-list.component.html',
  styleUrl: './student-list.component.css'
})
export class StudentListComponent implements OnInit {
  students: Student[] = [];
  displayedColumns: string[] = ['id', 'firstName', 'lastName', 'actions'];
  constructor(private studentService: StudentService, private router: Router) {

  }

  ngOnInit(): void {
    this.getStudents();
  }

  getStudents(): void {
    this.studentService
      .getStudents()
      .subscribe(students => {
        this.students = students;
      });
  }

  create(): void {
    this.router.navigateByUrl('createStudent');
  }

  details(id: Number): void {
    this.router.navigateByUrl(`student/${id}`);
  }
  deleteStudent(id: Number): void {
    this.studentService
      .deleteStudent(id)
      .subscribe(_ => this.getStudents());
  }
}
