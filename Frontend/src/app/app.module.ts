import { NgModule } from '@angular/core';
import { BrowserModule, provideClientHydration } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './pages/login/login.component';
import { ReactiveFormsModule } from '@angular/forms';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { NoticiasComponent } from './pages/noticias/noticias.component';
import { NavbarComponent } from './components/navbar/navbar.component';
import { interceptor } from './interceptor/interceptor';


const serviceAutentica = [interceptor]

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    NoticiasComponent,
    NavbarComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    ReactiveFormsModule,
    HttpClientModule
  ],
  providers: [  
    provideClientHydration(),
    serviceAutentica, {provide:HTTP_INTERCEPTORS, useClass:interceptor, multi:true}
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
