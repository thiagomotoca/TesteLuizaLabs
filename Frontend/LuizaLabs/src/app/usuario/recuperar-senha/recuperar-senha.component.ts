import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { first } from 'rxjs/operators';
import { AlertService, UsuarioService } from '../../_services';

@Component({
  selector: 'app-recuperar-senha',
  templateUrl: './recuperar-senha.component.html',
  styleUrls: ['./recuperar-senha.component.css']
})
export class RecuperarSenhaComponent implements OnInit {
  recuperacaoForm: FormGroup;
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
    this.recuperacaoForm = this.formBuilder.group({
      email: ['', Validators.required]
    });
  }

  get f() { return this.recuperacaoForm.controls; }

  onSubmit() {
    this.submitted = true;

    this.alertService.clear();

    if (this.recuperacaoForm.invalid) {
      return;
    }

    this.loading = true;
    this.usuarioService.recuperarSenha(this.f.email.value)
      .subscribe(
        data => {
          this.alertService.success('E-mail de recuperação enviado com sucesso!', { keepAfterRouteChange: true });
          this.router.navigate(['/usuario/login'], { relativeTo: this.route });
        },
        error => {
          this.alertService.error(error.message);
          this.loading = false;
        });
  }
}
