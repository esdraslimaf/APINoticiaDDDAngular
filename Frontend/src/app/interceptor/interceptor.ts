import { HttpEvent, HttpHandler, HttpHeaders, HttpInterceptor, HttpRequest } from "@angular/common/http";
import { Observable, map } from "rxjs";
import { AutenticaService } from "../service/autentica.service";
import { Injectable } from "@angular/core";

@Injectable({
    providedIn: 'root'
})
export class interceptor implements HttpInterceptor{

    constructor(private autenticaService:AutenticaService) {} // Injetando o serviço AutenticaService no construtor

    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        let headers; // Declaração da variável headers para armazenar os cabeçalhos da requisição

        // Verifica se o corpo da requisição é um FormData
        if(req.body instanceof FormData){
            // Se for, configura os cabeçalhos com contentType e processData definidos como "false"
            // e adiciona o token de autorização ao cabeçalho
            headers: new HttpHeaders({
                contentType:"false",
                processData:"false",
                Authorization:"Bearer "+this.autenticaService.ObterToken()
            });
        }
        else{
            // Se não for um FormData, configura os cabeçalhos adicionando
            // os cabeçalhos padrão "accept", "Content-Type" e o token de autorização ao cabeçalho
            headers: new HttpHeaders()
            .append("accept", "application/json")
            .append("Content-Type", "application/json")
            .append("Authorization","Bearer"+this.autenticaService.ObterToken());
        }

        // Clona a requisição original com os cabeçalhos atualizados
        let request = req.clone({headers});

        // Manipula a requisição clonada com os cabeçalhos atualizados
        // e retorna o Observable resultante
        return next.handle(request).pipe(
            map((event)=>{
               return event; 
            })
        );
    }
}
