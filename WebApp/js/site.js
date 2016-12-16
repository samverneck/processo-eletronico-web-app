﻿/****************************************************************************************************************************************************************************/
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
          alert("error");
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
          alert("error");
      });
}

/****************************************************************************************************************************************************************************/
/*CARREGA TIPOS CONTATO*/

$(document).ready(function () {
    //ajaxCarregaTiposContato();
});

function ajaxCarregaTiposContato() {
    $.ajax({ url: '/Autuacao/TiposContato'})
      .done(function (dados) {
          $.each(dados, function (i) {
              var optionhtml = '<label class="radio-inline">';
              optionhtml += '<input type="radio" name="tipoTelefone" data-id="' + this.id + '" data-digitos="' + this.quantidadeDigitos + '" value="' + this.descricao + '"/>' + this.descricao;
              optionhtml += '</label>';

              $('.tiposContato').append(optionhtml);
          });

          $('#formPessoaFisicaContatos input[type="radio"]:first').prop('checked', true);
          $('#formPessoaJuridicaContatos input[type="radio"]:last').prop('checked', true);
      })
      .fail(function () {
          alert("error");
      });
}

/****************************************************************************************************************************************************************************/
/*SELECIONA TIPO CONTATO*/


/****************************************************************************************************************************************************************************/
/*CARREGA MUNICIPIOS*/

$('body').on('change', '.campo-uf', consultaMunicipios);

var consultaMunicipios = function (e) {    
    e.stopPropagation();
    var elemento = this;
    $(elemento).parents().find('form .campo-municipio option:not([value=""])').remove();

    if (elemento.value !== '') {
        ajaxCarregaMunicipios(elemento);
    }    
};

function ajaxCarregaMunicipios(elemento) {
    $.ajax({ url: '/Autuacao/MunicipiosPorUf?uf=' + elemento.value, async: false })
      .done(function (dados) {
          $.each(dados, function (i) {
              var optionhtml = '<option data-municipio="'+ this.nome +'" value="' + this.nome + '">' + this.nome + '</option>';
              $(elemento).parents().find('form .campo-municipio').append(optionhtml);
          });
          console.log(dados.length);
      })
      .fail(function () {
          alert("error");
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
                if (pj.cnpj == interessadoPJProvisorio.cnpj) {
                    alert('Interessado já existe no processo.')
                    pjExiste = true;
                }
            }
            else {
                if (pj.cnpj == form['cnpj'].value.replace(/\/|\.|\-/g, "")) {
                    alert('Interessado já existe no processo.')
                    pjExiste = true;
                }
            }
        });

        if (!pjExiste) {
            //Listas dados do interessado
            contatosPJ = serializeTable('tabelaListaContatosPJ');
            emailsPJ = serializeTable('tabelaListaEmailsPJ');

            if (interessadoPJProvisorio != null) {
                if ($('#unidadeOrganizacaoPJ').val() != '0') {
                    interessadoPJProvisorio.nomeUnidade = $('#unidadeOrganizacaoPJ option:selected').text();
                    interessadoPJProvisorio.siglaUnidade = $('#unidadeOrganizacaoPJ').val();
                }
                arrayPJ.push(interessadoPJProvisorio);
            }
            else {

                if (!formPessoaJuridicaValidate.form()) {
                    return false;
                }

                arrayPJ.push(new objetoInteressadoPJ(form['razaosocial'].value, form['cnpj'].value.replace(/\/|\.|\-/g, ""), form['sigla'].value, '', '', contatosPJ, emailsPJ, form['municipio'].value));
            }

            //Reseta formulario
            resetaForm();
            resetaFormSelecaoPJ();
            limparFormPessoaJuridica();
        }

        //Limpa Objeto Provisorio
        interessadoPJProvisorio = null;

        //Recarrega Tabela Interessados
        carregaTabelaInteressados();
    }

    //Inclusao de Pessoa Fisica
    if ($(form).prop('id') == 'formPessoaFisica') {

        if (!formPessoaFisicaValidate.form()) {
            return false;
        }

        var pfExiste = false;

        $.each(arrayPF, function (i, pf) {
            if (pf.cpf == form['cpf'].value) {
                alert('Interessado já existe no processo.')
                pfExiste = true;
            }
        });

        if (!pfExiste) {
            //Listas dados do interessado PF
            contatosPF = serializeTable('tabelaListaContatosPF');
            emailsPF = serializeTable('tabelaListaEmailsPF');

            //Adiciona interessado a lista de interessados PF
            arrayPF.push(new objetoInteressadoPF(form['nome'].value, form['cpf'].value, contatosPF, emailsPF, form['uf'].value, form['municipio'].value));

            //Limpa Objeto Provisorio
            interessadoPJProvisorio = null;

            //Reseta formulario
            limparFormPessoaFisica();

            //Recarrega Tabela Interessados
            carregaTabelaInteressados();
        }
    }    

});

function carregaTabelaInteressados() {
    $('#tabelaInteressados tbody tr').remove();

    $.each(arrayPF, function (i, interessado) {
        $('#tabelaInteressados tbody').append('<tr><td>' + interessado.nome + '</td><td>' + interessado.cpf + '</td><td class="text-center colunaExcluir"><button data-id="' + i + '" class="btn btn-xs btn-danger btn-excluir btn-excluir-interessado-pf"><i class="fa fa-remove"></i></button></td></tr>');
    });

    $.each(arrayPJ, function (i, interessado) {
        console.log(interessado);
        $('#tabelaInteressados tbody').append('<tr><td>' + interessado.razaoSocial + '</td><td>' + interessado.cnpj + '</td><td class="text-center colunaExcluir"><button data-id="' + i + '" class="btn btn-xs btn-danger btn-excluir btn-excluir-interessado-pj"><i class="fa fa-remove"></i></button></td></tr>');
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
/*CARREGA ORGÃOS DO EXECUTIVO ESTADUAL*/

$('#tipoPJ').on('change', carregaOrgaosExecutivoEstadual);

function carregaOrgaosExecutivoEstadual(event) {
    var elemento = event.currentTarget;

    if (elemento.value == '1') {
        //$('#organizacaoPublica').children().remove();
        $('select#organizacaoPublica option:not([value="0"])').remove()
        ajaxCarregaOrgaosExecutivoEstadual();
    }
    else if (elemento.value == '2') {
        //$('#organizacaoPublica').children().remove();
        $('select#organizacaoPublica option:not([value="0"])').remove()
        ajaxCarregaOutrosOrgaosPublicos();
    }
    else {
        habilitaCamposPJ();
    }
}

//Carrega orgaos publicos do executivo estadual ES para o combo
function ajaxCarregaOrgaosExecutivoEstadual() {
    $.ajax('/Autuacao/OrganizacoesPorEsferaPoderUf?esfera=estadual&poder=executivo&uf=es')
      .done(function (dados) {

          $.each(dados, function (i) {
              var optionhtml = '<option value="' + this.guid + '">' + this.sigla + ' - ' + this.razaoSocial + '</option>';
              $('#organizacaoPublica').append(optionhtml);
          });

          console.log(dados.length);

      })
      .fail(function () {
          alert("error");
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

          console.log(dados.length);

      })
      .fail(function () {
          alert("error");
      });
}

/****************************************************************************************************************************************************************************/
/*INCLUIR MUNICIPIOS*/

var municipiosArray = [];

// Executa funcao prepareUpload ao selecionar arquivos
$('#btnIncluirMunicipio').on('click', prepareMunicipios);

// Adiciona os arquivos selecionados ao array files[] e exibe-os na tabela de arquivos selecionados
function prepareMunicipios(event) {
    if (!formAutuacaoMunicipioValidate.form()) {
        return false;
    }

    if ($('#municipio').val() != '' && $('#uf').val() != '') {
        arrayMunicipios.push(new objetoMunicipio($('#municipio').val(), $('#uf').val(), $('#municipio').attr('data-municipio')));
        $('#municipio').prop("selectedIndex", 0);

        carregaTabelaMunicipios();
    }
}

function carregaTabelaMunicipios() {
    $('#tabelaMunicipios tbody tr').remove();
    $.each(arrayMunicipios, function (i, municipio) {
        $('#tabelaMunicipios tbody').append('<tr><td>' + municipio.uf.toUpperCase() + '</td><td>' + municipio.nome + '</td><td class="text-center colunaExcluir"><button id="' + municipio.nome + '" class="btn btn-xs btn-danger btn-excluir btn-excluir-municipio"><i class="fa fa-remove"></i></button></td></tr>');
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
$('input[type=file]').on('change', prepareUpload);

// Adiciona os arquivos selecionados ao array files[] e exibe-os na tabela de arquivos selecionados
function prepareUpload(event) {
    $.each(event.target.files, function (i, obj) {
        arrayAnexos.push(new objetoAnexos(obj.name, obj, obj.name.split('.').pop()));
    });

    carregaTabelaAnexos()
}

function carregaTabelaAnexos() {
    $('#tabelaAnexos tbody tr').remove();

    $.each(arrayAnexos, function (i, file) {
        $('#tabelaAnexos tbody').append('<tr><td>' + file.nome + '</td><td>' + bytesToSize(file.conteudo.size) + '</td><td class="text-center colunaExcluir"><button data-id="' + i + '" class="btn btn-xs btn-danger btn-excluir btn-excluir-anexo"><i class="fa fa-remove"></i></button></td></tr>');
    });
}

//Exclui elemento da tabela e do arrayAnexos
$('tbody').on('click', '.btn-excluir-anexo', function () {
    arrayAnexos.splice($(this).attr('data-id'), 1);
    carregaTabelaAnexos()
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

$('body').on('click', '.btn-input-file', function () {
    $(this).closest($(".input-file").click());
});

/****************************************************************************************************************************************************************************/
/*ENVIAR AUTUACAO*/

$('#btnAutuar').on('click', function (e) {

    var validaFormResumo = formResumoSinalizacaoValidate.checkForm();
    var validaResponsavel = formAutuacaoResponsavelValidate.checkForm();

    //Valida preenchimento dos campos das abas de Resumo e Sinalização e Responsavel e Interessados
    if (!validaFormResumo || !validaResponsavel) {
        return false;
    }

    //Verifica se algum municipio foi selecionado na aba Abrangencia
    if (arrayMunicipios.length == 0) {
        return false;
    }

    //Verifica se algum interessado foi informado na aba Responsavel e Interessados
    if (arrayPJ.length == 0 && arrayPF.length == 0) {
        return false;
    }


    var formResumo = $('#formResumoSinalizacao')[0];
    var formResponsavel = $('#formAutuacaoResponsavel')[0];
    var formMunicipio = $('#formAutuacaoMunicipio')[0];
    var formAnexos = $('#formAutuacaoAnexos')[0];


    arraySinalizacao = null;

    $("input:checkbox[name=sinalizacao]:checked").each(function () {
        arraySinalizacao.push($(this).val());
    });

    //Cria objeto autuacao com os dados dos formularios
    var autuacao = new objetoAutuacao(formResumo.atividade.value, formResumo.resumo.value, arrayPF, arrayPJ, arrayMunicipios, [], arraySinalizacao, formAutuacao.idOrgaoAutuador, formAutuacao.nomeOrgaoAutuador, formAutuacao.siglaOrgaoAutuador,
        formAutuacao.idUnidadeAutuadora, formAutuacao.nomeUnidadeAutuadora, formAutuacao.siglaUnidadeAutuadora, formAutuacao.idUsuarioAutuador, formAutuacao.nomeUsuarioAutuador);

    console.log(JSON.stringify(autuacao));

    $.ajax({
        url: '/Autuacao/Autuar?autuacao',
        type: 'POST',
        data: JSON.stringify(autuacao),
        dataType: 'json',
        contentType: "application/json; charset=utf-8"
    }).done(function (dados) {
        console.log(dados);
        alert(dados);
    }).fail(function () {
        alert("error");
    });

    return false;

});

/****************************************************************************************************************************************************************************/
/*ENVIAR ARQUIVOS*/

//$('form').on('submit', uploadFiles);

// Catch the form submit and upload the files
function uploadFiles(event) {
    event.stopPropagation(); // Stop stuff happening
    event.preventDefault(); // Totally stop stuff happening

    // START A LOADING SPINNER HERE

    // Create a formdata object and add the files
    var data = new FormData();
    $.each(files, function (key, value) {
        data.append(key, value);
    });

    $.ajax({
        url: 'submit.php?files',
        type: 'POST',
        data: data,
        cache: false,
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        traditional: true,
        success: function (data, textStatus, jqXHR) {
            if (typeof data.error === 'undefined') {
                // Success so call function to process the form
                submitForm(event, data);
            }
            else {
                // Handle errors here
                console.log('ERRORS: ' + data.error);
            }
        },
        error: function (jqXHR, textStatus, errorThrown) {
            // Handle errors here
            console.log('ERRORS: ' + textStatus);
            // STOP LOADING SPINNER
        }
    });
}

/****************************************************************************************************************************************************************************/
/*MODAL WAITING*/
$(document).ajaxStart(function () {
    $('#modalWaiting').modal('show');
});

$(document).ajaxStop(function () {
    $('#modalWaiting').modal('hide');
});