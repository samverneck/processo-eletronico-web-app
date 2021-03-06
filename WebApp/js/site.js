﻿/****************************************************************************************************************************************************************************/
/*MENSAGEM*/
toastr.options = {
    "closeButton": true,
    "debug": false,
    "newestOnTop": false,
    "progressBar": false,
    "positionClass": "toast-top-center",
    "preventDuplicates": false,
    "onclick": null,
    "showDuration": "300",
    "hideDuration": "1000",
    "timeOut": "5000",
    "extendedTimeOut": "1000",
    "showEasing": "swing",
    "hideEasing": "linear",
    "showMethod": "fadeIn",
    "hideMethod": "fadeOut"
}

/****************************************************************************************************************************************************************************/
/*FORMATA VALOR CNPJ E CPF NA EXIBICAO DO VALOR*/
//$('.campoCpf').mask('999.999.999-99');
//$('.campoCnpj').mask('99.999.999/9999-99');

/****************************************************************************************************************************************************************************/
/*POPOVERS AJUDA*/

$('body').on('mouseover click', 'a[data-toggle="popover"]', function () {
    var e = $(this);
    e.popover('show');
});

/****************************************************************************************************************************************************************************/
/*WIZARD FORM*/
$(document).ready(function () {
    //Initialize tooltips
    $('.nav-tabs > li a[title]').tooltip();

    //Wizard
    $('a[data-toggle="tab"]').on('show.bs.tab', function (e) {
        var $target = $(e.target);
        if ($target.parent().hasClass('disabled')) {
            return false;
        }
    });

    $(".next-step").click(function (e) {
        var $active = $('.nav-tabs-custom .nav-tabs li.active');
        $active.next().removeClass('disabled');
        nextTab($active);
    });

    $(".prev-step").click(function (e) {
        var $active = $('.nav-tabs-custom .nav-tabs li.active');
        prevTab($active);
    });
});

function nextTab(elem) {
    $(elem).next().find('a[data-toggle="tab"]').click();
}
function prevTab(elem) {
    $(elem).prev().find('a[data-toggle="tab"]').click();
}

/****************************************************************************************************************************************************************************/
/*CARREGA FUNCAO*/

$('#planoClassificacao').on('change', carregaFuncoes);

function carregaFuncoes(event) {
    var elemento = event.currentTarget;

    if (elemento.value > 0) {
        $('select#funcao option:not([value=""])').remove()
        ajaxCarregaFuncoes(formAutuacao.idOrganizacaoPai, elemento.value);
    }
    else {
        $('select#funcao option:not([value=""])').remove()

    }
}

//Carrega orgaos publicos do executivo estadual ES para o combo
function ajaxCarregaFuncoes(id, idPlanoClassificacao) {
    $.ajax('/Autuacao/Funcoes?id=' + id + '&idPlanoClassificacao=' + idPlanoClassificacao)
      .done(function (dados) {
          $.each(dados, function (i) {
              var optionhtml = '<option value="' + this.id + '">' + this.descricao + '</option>';
              $('#funcao').append(optionhtml);
          });
      })
      .fail(function () {
          toastr["warning"]("Não foi possível realizar esta operação!");
      });
}

/****************************************************************************************************************************************************************************/
/*CARREGA ATIVIDADES*/

$('#funcao').on('change', carregaAtividades);

function carregaAtividades(event) {
    var elemento = event.currentTarget;

    if (elemento.value > 0) {
        $('select#atividade option:not([value=""])').remove()
        ajaxCarregaAtividades(formAutuacao.idOrganizacaoPai, elemento.value);
    }
    else {
        $('select#atividade option:not([value=""])').remove()

    }
}

//Carrega orgaos publicos do executivo estadual ES para o combo
function ajaxCarregaAtividades(id, idPlanoClassificacao) {
    $.ajax('/Autuacao/Atividades?id=' + id + '&idFuncao=' + idPlanoClassificacao)
      .done(function (dados) {
          $.each(dados, function (i) {
              var optionhtml = '<option value="' + this.id + '">' + this.descricao + '</option>';
              $('#atividade').append(optionhtml);
          });
      })
      .fail(function () {
          toastr["warning"]("Não foi possível realizar esta operação!");
      });
}

/****************************************************************************************************************************************************************************/
/*CARREGA TIPOS CONTATO*/

$(document).ready(function () {
    ajaxCarregaTiposContato();
});

function ajaxCarregaTiposContato() {
    $.ajax({ url: '/Autuacao/TiposContato' })
      .done(function (dados) {

          //console.log("Tipo Contato");
          //console.log(dados);

          $.each(dados, function (i) {
              var optionhtml = '<label class="radio-inline">';
              optionhtml += '<input type="radio" name="tipotelefone" data-id="' + this.id + '" data-digitos="' + this.quantidadeDigitos + '" value="' + this.descricao + '"/>' + this.descricao;
              optionhtml += '</label>';

              $('.tiposContato').append(optionhtml);
          });

          $('#formPessoaFisicaContatos input[type="radio"]:first').prop('checked', true);
          $('#formPessoaJuridicaContatos input[type="radio"]:first').prop('checked', true);
      })
      .fail(function () {
          toastr["warning"]("Não foi possível realizar esta operação!");
      });
}

/****************************************************************************************************************************************************************************/
/*SELECIONA TIPO CONTATO*/


/****************************************************************************************************************************************************************************/
/*CARREGA MUNICIPIOS*/
var consultaMunicipios = function (e) {
    e.preventDefault();
    var elemento = this;
    $(elemento).parents().find('form .campo-municipio option:not([value=""])').remove();
    if (elemento.value !== '') {
        ajaxCarregaMunicipios(elemento);
    }
};

$('body').on('change', '.campo-uf', consultaMunicipios);

function ajaxCarregaMunicipios(elemento) {
    $.ajax({ url: '/Autuacao/MunicipiosPorUf?uf=' + elemento.value, async: false })
      .done(function (dados) {
          $.each(dados, function (i) {
              var optionhtml = '<option value="' + this.guid + '">' + this.nome + '</option>';
              $(elemento).parents().find('form .campo-municipio').append(optionhtml);
          });
          //console.log(dados);
      })
      .fail(function () {
          toastr["warning"]("Não foi possível realizar esta operação!");
      });
}

/****************************************************************************************************************************************************************************/
/*INCLUSAO INTERESSADOS*/

var formSerializado;

$('#btnIncluirInteressado').on('click', function () {

    var form = $('#modalInteressados div.tab-content .active form')[0];

    //Inclusao de Pessoa Juridica
    if ($(form).prop('id') == 'formPessoaJuridica') {

        var pjExiste = false;

        $.each(arrayPJ, function (i, pj) {
            if (interessadoPJProvisorio != null) {
                if (pj.razaoSocial == interessadoPJProvisorio.razaoSocial && pj.siglaUnidade == $('#unidadeOrganizacaoPJ').val()) {
                    toastr["warning"]("Interessado Pessoa Jurídica já está relacionado.", "Atenção!");
                    pjExiste = true;
                }
            }
            else {
                if (pj.razaoSocial == form['razaosocial'].value && pj.siglaUnidade == form['sigla'].value) {
                    toastr["warning"]("Interessado Pessoa Jurídica já está relacionado.", "Atenção!");
                    pjExiste = true;
                }
            }
        });

        if (!pjExiste) {
            //Listas dados do interessado
            contatosPJ = serializeTable('tabelaListaContatosPJ');
            //limpaTelefone(contatosPJ);

            emailsPJ = serializeTable('tabelaListaEmailsPJ');

            if (interessadoPJProvisorio != null) {
                try {

                    if ($('#unidadeOrganizacaoPJ').val() != '') {
                        interessadoPJProvisorio.nomeUnidade = $('#unidadeOrganizacaoPJ option:selected').text();
                        interessadoPJProvisorio.siglaUnidade = $('#unidadeOrganizacaoPJ').val();
                    }
                    arrayPJ.push(interessadoPJProvisorio);
                    toastr["success"]("Interessado Pessoa Jurídica incluído com sucesso.", "Sucesso!");
                }
                catch (err) {
                    //console.log(err);
                    toastr["warning"]("Não foi possível incluir interessado Pessoa Jurídica!");
                }
            }
            else {

                if (!formPessoaJuridicaValidate.form()) {
                    return false;
                }

                try {

                    //arrayPJ.push(new objetoInteressadoPJ(form['razaosocial'].value, form['cnpj'].value.replace(/\/|\.|\-/g, ""), form['sigla'].value, '', '', '', emailsPJ, form['municipio'].value));
                    arrayPJ.push(new objetoInteressadoPJ(form['razaosocial'].value, form['cnpj'].value, form['sigla'].value, '', '', contatosPJ, emailsPJ, null, form['municipio'].value));
                    toastr["success"]("Interessado Pessoa Jurídica incluído com sucesso.", "Sucesso!");
                }
                catch (err) {
                    //console.log(err);
                    toastr["warning"]("Não foi possível incluir interessado Pessoa Física!");
                }
            }

            //Reseta formulario
            resetaForm();
            resetaFormSelecaoPJ();
            limparFormPessoaJuridica();

            //Limpa Objeto Provisorio
            interessadoPJProvisorio = null;

            //Recarrega Tabela Interessados
            carregaTabelaInteressados();
        }

           
    }

    //Inclusao de Pessoa Fisica
    if ($(form).prop('id') == 'formPessoaFisica') {

        if (!formPessoaFisicaValidate.form()) {
            return false;
        }

        var pfExiste = false;

        $.each(arrayPF, function (i, pf) {
            if (pf.cpf == form['cpf'].value.replace(/\/|\.|\-/g, "")) {
                toastr["warning"]("Interessado Pessoa Física já está relacionado.", "Atenção!");
                pfExiste = true;
            }
        });

        if (!pfExiste) {
            //Listas dados do interessado PF
            contatosPF = serializeTable('tabelaListaContatosPF');
            limpaTelefone(contatosPF);


            emailsPF = serializeTable('tabelaListaEmailsPF');

            //Adiciona interessado a lista de interessados PF
            //arrayPF.push(new objetoInteressadoPF(form['nome'].value, form['cpf'].value.replace(/\/|\.|\-/g, ""), contatosPF, emailsPF, form['municipio'].value));
            arrayPF.push(new objetoInteressadoPF(form['nome'].value, form['cpf'].value, contatosPF, emailsPF, form['municipio'].value));

            //Limpa Objeto Provisorio
            interessadoPJProvisorio = null;

            //Reseta formulario
            limparFormPessoaFisica();

            //Recarrega Tabela Interessados
            carregaTabelaInteressados();

            toastr["success"]("Interessado Pessoa Física incluído com sucesso.", "Sucesso!");
        }
    }

});

function carregaTabelaInteressados() {
    $('#tabelaInteressados tbody tr').remove();

    $.each(arrayPF, function (i, interessado) {
        $('#tabelaInteressados tbody').append('<tr><td>' + interessado.nome + '</td><td>' + interessado.cpf + '</td><td class="text-center colunaExcluir"><button data-id="' + i + '" class="btn btn-xs btn-danger btn-excluir btn-excluir-interessado-pf"><i class="fa fa-remove"></i></button></td></tr>');
    });

    $.each(arrayPJ, function (i, interessado) {
        //console.log(interessado);
        $('#tabelaInteressados tbody').append('<tr><td>' + interessado.razaoSocial + '</td><td>' + interessado.cnpj + '</td><td>' + interessado.siglaUnidade + '</td><td class="text-center colunaExcluir"><button data-id="' + i + '" class="btn btn-xs btn-danger btn-excluir btn-excluir-interessado-pj"><i class="fa fa-remove"></i></button></td></tr>');
    });
}

//Exclui elemento da tabela e do arrayPJ e arrayPF
$('tbody').on('click', '.btn-excluir-interessado-pf', function () {
    arrayPF.splice($(this).attr('data-id'), 1);
    carregaTabelaInteressados()
});

$('tbody').on('click', '.btn-excluir-interessado-pj', function () {
    arrayPJ.splice($(this).attr('data-id'), 1);
    carregaTabelaInteressados()
});

/****************************************************************************************************************************************************************************/
/*PREPARA TELEFONES PARA ENVIO*/

function limpaTelefone(lista) {
    $.each(lista, function (i, v) {
        v.telefone = v.telefone.replace(/\/|\.|\-|\(|\s|\)/g, "");

    });
}

/****************************************************************************************************************************************************************************/
/*CARREGA ORGÃOS DO EXECUTIVO ESTADUAL*/

$('#tipoPJ').on('change', carregaOrgaosExecutivoEstadual);

function carregaOrgaosExecutivoEstadual(event) {
    var elemento = event.currentTarget;

    if (elemento.value == '1') {
        //$('#organizacaoPublica').children().remove();
        $('select#organizacaoPublica option:not([value=""])').remove()
        ajaxCarregaOrgaosExecutivoEstadual();
    }
    else if (elemento.value == '2') {
        //$('#organizacaoPublica').children().remove();
        $('select#organizacaoPublica option:not([value=""])').remove()
        ajaxCarregaOutrosOrgaosPublicos();
    }
    else {
        habilitaCamposPJ();
    }
}

//Carrega orgaos publicos do executivo estadual ES para o combo
function ajaxCarregaOrgaosExecutivoEstadual() {
    //$.ajax('/Autuacao/OrganizacoesPorEsferaPoderUf?esfera=estadual&poder=executivo&uf=es')
    $.ajax('/Autuacao/OrganizacoesPorPatriarca')
      .done(function (dados) {
          $.each(dados, function (i) {
              var optionhtml = '<option value="' + this.guid + '">' + this.sigla + ' - ' + this.razaoSocial + '</option>';
              $('#organizacaoPublica').append(optionhtml);
          });

          ////console.log(dados.length);

      })
      .fail(function () {
          toastr["warning"]("Não foi possível realizar esta operação!");
      });
}

//Carrega outros orgaos publicos para o combo
function ajaxCarregaOutrosOrgaosPublicos() {
    $.ajax('/Autuacao/OrganizacoesOutrosOrgaos')
      .done(function (dados) {
          $.each(dados, function (i) {
              var optionhtml = '<option value="' + this.guid + '">' + this.sigla + ' - ' + this.razaoSocial + '</option>';
              $('#organizacaoPublica').append(optionhtml);
          });
          //console.log(dados.length);
      })
      .fail(function () {
          toastr["warning"]("Não foi possível realizar esta operação!");
      });
}

/****************************************************************************************************************************************************************************/
/*INCLUIR MUNICIPIOS*/

// Executa funcao prepareUpload ao selecionar arquivos
$('#btnIncluirMunicipio').on('click', prepareMunicipios);

// Adiciona os arquivos selecionados ao array files[] e exibe-os na tabela de arquivos selecionados
function prepareMunicipios(event) {
    //if (!formAutuacaoMunicipioValidate.form()) {
    //    return false;
    //}

    if ($('#municipio').val() != '' && $('#uf').val() != '') {
        arrayMunicipios.push(new objetoMunicipio($('#municipio').val(), $('#uf').val(), $('#municipio option:selected').text()));
        $('#municipio').prop("selectedIndex", 0);

        carregaTabelaMunicipios();
    }
}

function carregaTabelaMunicipios() {
    $('#tabelaMunicipios tbody tr').remove();
    $.each(arrayMunicipios, function (i, municipio) {
        $('#tabelaMunicipios tbody').append('<tr><td>' + municipio.uf.toUpperCase() + '</td><td>' + municipio.nome + '</td><td class="text-center colunaExcluir"><button id="' + municipio.guidMunicipio + '" class="btn btn-xs btn-danger btn-excluir btn-excluir-municipio"><i class="fa fa-remove"></i></button></td></tr>');
    });
}

// Exclui municipio selecionado
$('#tabelaMunicipios tbody').on('click', '.btn-excluir-municipio', function () {
    arrayMunicipios.splice($(this).attr('data-id'), 1);
    carregaTabelaMunicipios()
});

/****************************************************************************************************************************************************************************/
/*EXCLUIR ITEM DA TABELA*/

$('tbody').on('click', '.btn-excluir', function () {
    $(this).closest('tr').remove();
});

/****************************************************************************************************************************************************************************/
/*UPLOAD DE ARQUIVOS*/

// Executa funcao prepareUpload ao selecionar arquivos
//$('input[type=file]').on('change', prepareUpload);
$('body').on('change', 'input[type=file]', prepareUpload);

// Adiciona os arquivos selecionados ao array files[] e exibe-os na tabela de arquivos selecionados
function prepareUpload(event) {
    var cont = 0;

    $.each(event.target.files, function (i, obj) {
        $.each(arrayAnexos, function (i, anexo) {
            if (obj.name == anexo.nome) {
                cont = 1;
            }
        });

        if (cont == 0) {
            var reader = new FileReader();
            reader.onload = function (event) {
                arrayAnexos.push(new objetoAnexos(obj.name, event.target.result, obj.type, obj.size));
                carregaTabelaAnexos();
            };
            reader.readAsDataURL(obj);
        }
    });
}

function testeAnexos() {
    $.each($('#tabelaAnexos tbody tr'), function (i, linha) {
        console.log(linha);
    });
}

function carregaTabelaAnexos() {
    $('#tabelaAnexos tbody tr').remove();

    $.each(arrayAnexos, function (i, file) {
        $('#tabelaAnexos tbody').append('<tr><td>' + file.nome + '</td><td><select id="sel-' + i + '" class="form-control selectTipoDocumental">' + tiposDocumentais + '</select></td><td>' + bytesToSize(file.tamanho) + '</td><td><textarea id="text-' + i + '" class="form-control descricaoTipoDocumental">' + file.descricao + '</textarea></td><td class="text-center colunaExcluir"><button type="button" data-id="' + i + '" class="btn btn-xs btn-danger btn-excluir btn-excluir-arquivo"><i class="fa fa-remove"></i></button></td></tr>');
    });
}

//Exclui elemento da tabela e do arrayAnexos
$('tbody').on('click', '.btn-excluir-arquivo', function () {
    arrayAnexos.splice($(this).attr('data-id'), 1);
    carregaTabelaAnexos();
});

$('body').on('click', '.btn-excluir-arquivo', function () {
    arrayAnexos.splice($(this).attr('data-id'), 1);
    carregaTabelaAnexos();
});


/****************************************************************************************************************************************************************************/
/*SERIALIZAR DADOS TABELA*/
function serializeTable(nomeTabela) {
    var $table = $('#' + nomeTabela),
    rows = [],
    header = [];

    $table.find("thead th:not(:last)").each(function () {
        header.push($(this).attr('data-header'));
    });

    $table.find("tbody tr").each(function () {
        var row = {};

        $(this).find("td:not(:last)").each(function (i) {
            var key = header[i];
            var value;

            if ($(this).is('[data-value]')) {
                value = $(this).attr('data-value');
            }
            else {
                value = $(this).html();
            }

            row[key] = value;
        });

        rows.push(row);
    });

    return rows;
}

/****************************************************************************************************************************************************************************/
/*CONVERTE BYTES EM KB, MG, GB OU TB */

function bytesToSize(bytes) {
    var sizes = ['Bytes', 'KB', 'MB', 'GB', 'TB'];
    if (bytes === 0) return '0 Byte';
    var i = parseInt(Math.floor(Math.log(bytes) / Math.log(1024)));
    return Math.round(bytes / Math.pow(1024, i), 2) + ' ' + sizes[i];
};

/****************************************************************************************************************************************************************************/
/*FALSO INPUT FILE*/

//$(document).ready(function () {
//    $('.btn-input-file').click(function () {
//        alert('teste');
//        $(this).closest($(".input-file").click());
//    });
//});

var btnUpload = function (event) {
    event.stopPropagation();
    event.preventDefault();
    $(".input-file").trigger("click");
};

$('body').on('click', '.btn-input-file', btnUpload);


/****************************************************************************************************************************************************************************/
/*ENVIAR AUTUACAO*/
$('#btnAutuar').on('click', function (e) {

    //Atribui ao campo resumo o conteudo do ckeditor.
    $('#resumo').val(CKEDITOR.instances.resumo.getData());

    console.log($('#resumo').val());

    if (!validaForm('#formResumoSinalizacao')) {
        toastr["warning"]("Não foi possíve realizar a autuação! Verifique os campos obrigatórios e tente novamente!");
        return false;
    }

    //if (!formResumoSinalizacaoValidate.checkForm()) {
    //    return false;
    //}

    //Verifica se algum municipio foi selecionado na aba Abrangencia
    if (arrayMunicipios.length == 0) {
        toastr["warning"]("Não foi possíve realizar a autuação! Informe pelo menos um município na aba Abrangência.");
        return false;
    }

    //Verifica se algum interessado foi informado na aba Responsavel e Interessados
    if (arrayPJ.length == 0 && arrayPF.length == 0) {
        toastr["warning"]("Não foi possíve realizar a autuação! Informe pelo menos um interessado na aba Responsáveis e Interessados.");
        return false;
    }

    var formResumo = $('#formResumoSinalizacao')[0];
    var formResponsavel = $('#formAutuacaoResponsavel')[0];
    var formMunicipio = $('#formAutuacaoMunicipio')[0];
    var formAnexos = $('#formAutuacaoAnexos')[0];


    $.each(arrayAnexos, function (i, v) {
        if ($('select#sel-' + i).val() != 0) {
            v.idTipoDocumental = $('select#sel-' + i).val();
        }
        v.descricao = $('textarea#text-' + i).val();
    });

    arraySinalizacao = [];

    $("input:checkbox[name=sinalizacao]:checked").each(function () {
        //console.log($(this).val());
        arraySinalizacao.push($(this).val());
    });

    //Cria objeto autuacao com os dados dos formularios
    var autuacao = new objetoAutuacao(formResumo.atividade.value, formResumo.resumo.value, arrayPF, arrayPJ, arrayMunicipios, arrayAnexos, arraySinalizacao, formAutuacao.guidOrgao, $('#unidadeAutuadora').val());

    //console.log(autuacao);
    //console.log(JSON.stringify(autuacao));

    $.ajax({
        url: '/Autuacao/Autuar',
        type: 'POST',
        data: JSON.stringify(autuacao),
        dataType: 'json',
        contentType: "application/json; charset=utf-8"
        //contentType: "application/x-www-form-urlencoded"
    }).done(function (dados, textStatus, request) {
        
        //console.log(dados);
        ExibirMensagens();
        if (dados.IsSuccessStatusCode) {
            var delay = 3000;
            setTimeout(function () { window.location.href = '/Home'; }, delay);
        }

    }).fail(function () {
        toastr["warning"]("Não foi possível autuar o processo!");
    });

    return false;

});


/****************************************************************************************************************************************************************************/
/*PREPARANDO OBJETO ANEXO*/

function prepararAnexos(anexos) {
    $.each(anexos, function (i, v) {
        if ($('select#sel-' + i).val() != 0) {
            v.idTipoDocumental = $('select#sel-' + i).val();
        }
        v.descricao = $('textarea#text-' + i).val();
    });

    return anexos;
}


/****************************************************************************************************************************************************************************/
/*MODAL WAITING*/
$(document).ajaxStart(function () {
    $('#modalWaiting').modal('show');
});

$(document).ajaxStop(function () {
    $('#modalWaiting').modal('hide');
});


/****************************************************************************************************************************************************************************/
/*CONSULTA TIPO DOCUMENTAL*/
$('#atividade').on('change', carregaTipoDocumental);

function carregaTipoDocumentalDespacho(dados) {

    var optionhtml = '<option value="">Tipo Documental</option>';
    $.each(dados, function (i) {
        optionhtml += '<option value="' + this.id + '">' + this.codigo + ' - ' + this.descricao + '</option>';
    });

    tiposDocumentais = optionhtml;

    $('.selectTipoDocumental option').remove();
    $('.selectTipoDocumental').append(optionhtml);
}

/**/
function carregaTipoDocumental(event) {
    var elemento = event.currentTarget;

    if (elemento.value > 0) {
        tiposDocumentais = ajaxCarregaTipoDocumental(elemento.value);
    }
}

/**/
function ajaxCarregaTipoDocumental(idAtividade) {
    $.ajax('/Autuacao/TiposDocumentais?idAtividade=' + idAtividade)
      .done(function (dados) {
          var optionhtml = '<option value="">Tipo Documental</option>';
          $.each(dados, function (i) {
              optionhtml += '<option value="' + this.id + '">' + this.codigo + ' - ' + this.descricao + '</option>';
          });

          tiposDocumentais = optionhtml;

          $('.selectTipoDocumental option').remove();
          $('.selectTipoDocumental').append(optionhtml);

      })
      .fail(function () {
          toastr["warning"]("Não foi possível realizar esta operação!");
      });
}

/*SELECT2*/
$(document).ready(function () {
    $("#unidadeAutuadora").select2({ width: '100%' });
    $("#atividade").select2({ width: '100%' });
    $(".campo-uf").select2({ width: '100%' });
    $(".campo-municipio").select2({ width: '100%' });

    //Formulario de pessoa juridica
    $("#organizacaoPublica").select2({ width: '100%' });
    $("#unidadeOrganizacaoPJ").select2({ width: '100%' });
});

/****************************************************************************************************************************************************************************/
/*VALIDAR FORMULARIO*/
validaElemento('#formResumoSinalizacao');

function validaElemento(form) {
    $(form).on('blur', '[required="required"]', function () {
        verificaElemento(this);
    });
}

function validaForm(form) {
    var result = true;

    $.each($(form).find('[required="required"]'), function (i, e) {
        if (!verificaElemento(e)) {
            result = false;
        }
    });

    return result;
}

function verificaElemento(e) {
    var result = true;
    var elementError = '#' + $(e).prop('id') + '-error';

    if ($(elementError) != null) {
        $(elementError).remove();
    }

    if ($(e).val() == '' || $(e).val() == 0 || $(e).val() == null) {
        $(e).before('<label id="' + $(e).prop('id') + '-error" class="error" for="' + $(e).prop('id') + '">' + $(e).attr('title') + '</label>');
        result = false;
    }

    return result;
}