import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { first } from 'rxjs/operators';
import { MustMatch } from '../../_helpers';
import { AlertService, UsuarioService } from '../../_services';

@Component({
  selector: 'app-cadastro',
  templateUrl: './cadastro.component.html',
  styleUrls: ['./cadastro.component.css']
})
export class CadastroComponent implements OnInit {
  cadastroForm: FormGroup;
  loading = false;
  submitted = false;

  constructor(
    private formBuilder: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private usuarioService: UsuarioService,
    private alertService: AlertService
  ) { }

  ngOnInit() {
    this.cadastroForm = this.formBuilder.group({
      nome: ['', Validators.required],
      email: ['', Validators.required],
      senha: ['', Validators.required],
      confirmacaoSenha: ['', Validators.required]
    }, {
      validator: MustMatch('senha', 'confirmacaoSenha')
    });
  }

  get f() { return this.cadastroForm.controls; }

  onSubmit() {
    this.submitted = true;

    this.alertService.clear();

    if (this.cadastroForm.invalid) {
      return;
    }

    this.loading = true;
    this.usuarioService.cadastrar(this.cadastroForm.value)
      .subscribe(
        data => {
          this.alertService.success('Cadastrado com sucesso', { keepAfterRouteChange: true });
          this.router.navigate(['/usuario/login'], { relativeTo: this.route });
        },
        error => {
          this.alertService.error(error.message);
          this.loading = false;
        });
  }
}
