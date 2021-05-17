import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { first } from 'rxjs/operators';
import { UsuarioAlterarSenha } from '../../_models';
import { MustMatch } from '../../_helpers';
import { AlertService, UsuarioService } from '../../_services';

@Component({
  selector: 'app-alterar-senha',
  templateUrl: './alterar-senha.component.html',
  styleUrls: ['./alterar-senha.component.css']
})
export class AlterarSenhaComponent implements OnInit {
  alterarSenhaForm: FormGroup;
  loading = false;
  submitted = false;
  idUsuario: number;

  constructor(
    private formBuilder: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private usuarioService: UsuarioService,
    private alertService: AlertService
  ) {
    this.route.params.subscribe(params => {
      this.idUsuario = params['idUsuario'];;
    });
  }

  ngOnInit() {
    this.alterarSenhaForm = this.formBuilder.group({
      senha: ['', Validators.required],
      confirmacaoSenha: ['', Validators.required]
    }, {
      validator: MustMatch('senha', 'confirmacaoSenha')
    });
  }

  get f() { return this.alterarSenhaForm.controls; }

  onSubmit() {
    this.submitted = true;

    this.alertService.clear();

    if (this.alterarSenhaForm.invalid) {
      return;
    }

    const dados = new UsuarioAlterarSenha();
    dados.Id = this.idUsuario;
    dados.Senha = this.f.senha.value;
    dados.ConfirmacaoSenha = this.f.confirmacaoSenha.value;

    this.loading = true;
    this.usuarioService.alterarSenha(dados)
      .subscribe(
        data => {
          this.alertService.success('Senha alterada com sucesso', { keepAfterRouteChange: true });
          this.router.navigate(['/usuario/login'], { relativeTo: this.route });
        },
        error => {
          this.alertService.error(error.message);
          this.loading = false;
        });
  }
}
