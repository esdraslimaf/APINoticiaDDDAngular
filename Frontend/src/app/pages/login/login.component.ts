import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { LoginModel } from '../../models/LoginModel';
import { LoginService } from '../../service/login.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss'
})
export class LoginComponent implements OnInit{
  
  loginForm!:FormGroup;

  constructor(private formBuilder: FormBuilder, private router: Router ,public loginService:LoginService){}
  
  ngOnInit(): void {
   this.createForm();
  }

  createForm(){
    this.loginForm = this.formBuilder.group({
      email:['',[Validators.required, Validators.email]],
      senha:['',[Validators.required]]
    });
  }

  onSubmit(){
    var dadosDoFormulario = this.loginForm.getRawValue() as LoginModel
    
    /*  this.loginService.LoginUsuario(dadosDoFormulario)
    .subscribe(
      token=>{
        debugger
        var meutoken = token;
      },
      error=>{

      }) */

  this.router.navigate(["/noticias"])

              }

}
