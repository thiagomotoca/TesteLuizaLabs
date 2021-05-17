import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { map } from 'rxjs/operators';

import { Usuario, UsuarioNovo, UsuarioAlterarSenha } from '../_models';
import { environment } from 'src/environments/environment';

@Injectable({ providedIn: 'root' })
export class UsuarioService {
    private usuarioSubject: BehaviorSubject<Usuario>;
    public usuario: Observable<Usuario>;

    constructor(
        private router: Router,
        private http: HttpClient
    ) {
        this.usuarioSubject = new BehaviorSubject<Usuario>(JSON.parse(localStorage.getItem('usuario')));
        this.usuario = this.usuarioSubject.asObservable();
    }

    public get usuarioValue(): Usuario {
        return this.usuarioSubject.value;
    }

    login(email: string, senha: string) {
        return this.http.post<Usuario>(`${environment.apiUrl}/usuario/autenticar`, { email, senha })
            .pipe(map(usuario => {
                if (usuario && usuario.Token) {
                    localStorage.setItem('usuario', JSON.stringify(usuario));
                    this.usuarioSubject.next(usuario);
                }
                return usuario;
            }));
    }

    logout() {
        localStorage.removeItem('usuario');
        this.usuarioSubject.next(null);
        this.router.navigate(['/usuario/login']);
    }

    cadastrar(usuarioNovo: UsuarioNovo) {
        return this.http.post(`${environment.apiUrl}/usuario`, usuarioNovo);
    }

    recuperarSenha(email: string) {
        return this.http.get(`${environment.apiUrl}/usuario/recuperarsenha?email=${email}`);
    }

    alterarSenha(usuarioAlterarSenha: UsuarioAlterarSenha) {
        return this.http.put(`${environment.apiUrl}/usuario/alterarsenha`, usuarioAlterarSenha);
    }
}