import { Component, OnInit, OnDestroy } from '@angular/core';
import { Subscription } from 'rxjs';

import { Usuario } from '../_models';
import { UsuarioService,} from '../_services';

@Component({ templateUrl: 'home.component.html' })
export class HomeComponent implements OnInit, OnDestroy {
    usuario: Usuario;
    usuarioSubscription: Subscription;

    constructor(
        private usuarioService: UsuarioService
    ) {
        this.usuarioSubscription = this.usuarioService.usuario.subscribe(user => {
            this.usuario = user;
        });
    }

    ngOnInit() {
    }

    ngOnDestroy() {
        this.usuarioSubscription.unsubscribe();
    }
}