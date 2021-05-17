import { Injectable } from '@angular/core';
import { Router, CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';

import { UsuarioService } from '../_services';

@Injectable({ providedIn: 'root' })
export class AuthGuard implements CanActivate {
    constructor(
        private router: Router,
        private usuarioService: UsuarioService
    ) {}

    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
        const usuario = this.usuarioService.usuarioValue;
        if (usuario) {
            return true;
        }

        this.router.navigate(['/usuario/login'], { queryParams: { returnUrl: state.url }});
        return false;
    }
}