import { Routes } from '@angular/router';
import { MainLayoutComponent } from './layout/main-layout/main-layout.component';
import { HomeComponent } from './pages/home/home.component';
import { AboutComponent } from './pages/about/about.component';
import { TrusteesComponent } from './pages/trustees/trustees.component';
import { InitiativesComponent } from './pages/initiatives/initiatives.component';
import { TransparencyComponent } from './pages/transparency/transparency.component';
import { ContactComponent } from './pages/contact/contact.component';

export const routes: Routes = [
  {
    path: '',
    component: MainLayoutComponent,
    children: [
      { path: '', component: HomeComponent },
      { path: 'about', component: AboutComponent },
      { path: 'trustees', component: TrusteesComponent },
      { path: 'initiatives', component: InitiativesComponent },
      { path: 'transparency', component: TransparencyComponent },
      { path: 'contact', component: ContactComponent }
    ]
  },
  { path: '**', redirectTo: '' }
];
