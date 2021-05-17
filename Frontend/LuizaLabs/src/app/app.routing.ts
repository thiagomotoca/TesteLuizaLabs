import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { AlterarSenhaComponent } from './usuario/alterar-senha/alterar-senha.component';
import { CadastroComponent } from './usuario/cadastro/cadastro.component';
import { LoginComponent } from './usuario/login/login.component';
import { RecuperarSenhaComponent } from './usuario/recuperar-senha/recuperar-senha.component';
import { UsuarioComponent } from './usuario/usuario.component';
import { AuthGuard } from './_helpers/auth.guard';

const routes: Routes = [
    { path: '', component: HomeComponent, canActivate: [AuthGuard] },
    {
        path: 'usuario', component: UsuarioComponent,
        children: [
            { path: 'login', component: LoginComponent },
            { path: 'cadastro', component: CadastroComponent },
            { path: 'alterar-senha/:idUsuario', component: AlterarSenhaComponent },
            { path: 'recuperar-senha', component: RecuperarSenhaComponent },
        ]
    },

    { path: '**', redirectTo: '' }
];

export const AppRoutingModule = RouterModule.forRoot(routes);