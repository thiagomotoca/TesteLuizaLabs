import { Component } from '@angular/core';

import { UsuarioService } from './_services';
import { Usuario } from './_models';

@Component({ selector: 'app', templateUrl: 'app.component.html' })
export class AppComponent {
    usuario: Usuario;

    constructor(private usuarioService: UsuarioService) {
        this.usuarioService.usuario.subscribe(x => this.usuario = x);
    }

    logout() {
        this.usuarioService.logout();
    }
}