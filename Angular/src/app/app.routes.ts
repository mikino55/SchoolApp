import { Routes } from '@angular/router';
import { StudentListComponent } from './pages/student/student-list.component';
import { StudentDetailsComponent } from './pages/student/student-details.component';
import { TestComponent } from './pages/test/test.component';
import { LoginComponent } from './pages/login/login.component';

export const routes: Routes = [
    { path: '', redirectTo: 'student', pathMatch: 'full'},
    { path: 'student', component: StudentListComponent },
    { path: 'student/:id', component: StudentDetailsComponent },
    { path: 'createStudent', component: StudentDetailsComponent },
    { path: 'signin', component: LoginComponent },
    { path: 'sample', component: TestComponent },
];
