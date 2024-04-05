import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})

export class LoginService{

  private readonly SERVER_URL = "http://localhost:5294/api"; //Faltando pasta environments

  constructor(private httpClient: HttpClient) 
  { 

  }

  LoginUsuario(objeto:any){
    return this.httpClient.post<any>(`${this.SERVER_URL}/CriarTokenIdentity`, objeto);
  }



}
