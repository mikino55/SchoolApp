import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';

@Component({
  selector: 'app-test',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './test.component.html',
  styleUrl: './test.component.css'
})
export class TestComponent {
  protected person: Person;

  constructor (private router: Router) {
    this.person =  { id : 1, name : 'aaaa'};
  }

  goToStudentList() : void {
    this.router.navigateByUrl('student');
  }
}

export class Person{
  id: number;
  name: string;
}
