import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { StudentService } from '../../core/services/student.service';
import { Student } from './student';
import { RouterLink, Router } from '@angular/router';
import {MatTableModule} from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';


@Component({
  selector: 'app-student-list',
  standalone: true,
  imports: [CommonModule, RouterLink, MatTableModule, MatButtonModule],
  templateUrl: './student-list.component.html',
})
export class StudentListComponent implements OnInit {
  students: Student[] = [];
  displayedColumns: string[] = ['id', 'firstName', 'lastName', 'email', 'actions'];
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
  deleteStudent(id: string): void {
    this.studentService
      .deleteStudent(id)
      .subscribe(_ => this.getStudents());
  }
}
