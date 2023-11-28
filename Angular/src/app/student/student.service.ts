import { Injectable } from '@angular/core';
import { Student } from './student';
import { HttpClient, HttpHeaders } from '@angular/common/http';

import { Observable, of } from 'rxjs';
import { catchError, map, tap } from 'rxjs/operators';
import { observableToBeFn } from 'rxjs/internal/testing/TestScheduler';

@Injectable({
  providedIn: 'root'
})
export class StudentService {

  url = 'http://localhost:3000/students';

  constructor(private http: HttpClient) {

  }

  getStudents(): Observable<Student[]> {
    return this.http.get<Student[]>(this.url)
      .pipe(
        tap(_ => console.log('fetched students')),
        catchError(this.handleError<Student[]>('getStudents', []))
      );
  }

  getStudent(id: Number): Observable<Student> {
    return this.http.get<Student>(this.url + "/" + id)
      .pipe(
        tap(_ => console.log('fetched student id' + id)),
        catchError(this.handleError<Student>('getStudentById'))
      );
  }

  updateStudent(student: Student): Observable<Student> {
    return this.http.put<Student>(
      `${this.url}/${student.id}`, student)
      .pipe(
        tap(_ => console.log('updated student id ' + student.id)),
        catchError(this.handleError<Student>('updateStudent'))
      );
  }

  createStudent(student: Student): Observable<Student> {
    return this.http.post<Student>(
      this.url, student)
      .pipe(
        tap(_ => console.log('created student id ' + student.id)),
        catchError(this.handleError<Student>('createStudent'))
      );
  }

  deleteStudent(id: Number): Observable<Student> {
    return this.http.delete<Student>(
      `${this.url}/${id}`)
      .pipe(
        tap(_ => console.log('deleted student id ' + id)),
        catchError(this.handleError<Student>('deleteStudent'))
      );
  }
  private handleError<T>(operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {

      // TODO: send the error to remote logging infrastructure
      console.error(error); // log to console instead

      // Let the app keep running by returning an empty result.
      return of(result as T);
    };
  }
}
