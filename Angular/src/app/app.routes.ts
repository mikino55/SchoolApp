import { Routes } from '@angular/router';
import { StudentListComponent } from './student/student-list.component';
import { StudentDetailsComponent } from './student/student-details.component';
import { TestComponent } from './sample/test/test.component';

export const routes: Routes = [
    { path: '', redirectTo: 'student', pathMatch: 'full'},
    { path: 'student', component: StudentListComponent },
    { path: 'student/:id', component: StudentDetailsComponent },
    { path: 'createStudent', component: StudentDetailsComponent },
    { path: 'sample', component: TestComponent },
];
