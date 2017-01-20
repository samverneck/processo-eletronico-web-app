/****************************************************************************************************************************************************************************/
/*CARREGA DADOS DA ORGANIZACAO*/

//$('#btnCarregarOrganizacao').on('click', carregaDadosOrgaoExecutivoEstadual);

$('#organizacaoPublica').on('change', carregaDadosOrgaoExecutivoEstadual);

function carregaDadosOrgaoExecutivoEstadual(event) {
    if ($('#organizacaoPublica')[0].value != '0') {
        var elemento = $('#organizacaoPublica')[0].value;
        limpaTabelasListasEmailContatoSite();
        ajaxCarregaDadosOrgaoExecutivoEstadual(elemento);
        ajaxCarregaUnidadesOrganizacao(elemento);
        //$('.panelPJ').show();
    }
}

function ajaxCarregaDadosOrgaoExecutivoEstadual(elemento) {
    $.ajax('/Autuacao/OrganizacaoPorGuid?guidOrganizacao=' + elemento)
      .done(function (dados) {
          //console.log(dados);          

          try {
              //Não envia contatos de organograma para processoeletronico
              interessadoPJProvisorio = new objetoInteressadoPJ(dados.razaoSocial, dados.cnpj, dados.sigla, '', '', null, dados.emails, dados.sites, dados.endereco.municipio.guid, dados.tipoOrganizacao.descricao);
          }
          catch (err) {
              //console.log(err);
              toastr["warning"]("Não foi possível realizar esta operação!");
          }
      })
      .fail(function () {
          toastr["warning"]("Não foi possível realizar esta operação!");
      });
}


/****************************************************************************************************************************************************************************/
/*CARREGA UNIDADES DA ORGANIZACAO*/

function ajaxCarregaUnidadesOrganizacao(elemento) {
    $.ajax({ url: '/Autuacao/unidadesPorOrganizacao?guidOrganizacao=' + elemento, async: false })
      .done(function (dados) {
          //Exclui itens do combo
          $('select#unidadeOrganizacaoPJ option:not([value=""])').remove()

          //Preenche combo com novo itens
          $.each(dados, function (i) {
              var optionhtml = '<option value="' + this.sigla + '">' + this.sigla + ' - ' + this.nome + '</option>';
              $('#unidadeOrganizacaoPJ').append(optionhtml);
          });
      })
      .fail(function () {
          toastr["warning"]("Não foi possível realizar esta operação!");
      });
}

/****************************************************************************************************************************************************************************/
/*LIMPA DADOS DAS TABELAS DE EMAIL, CONTATO E SITE*/

function limpaTabelasListasEmailContatoSite() {
    $('#tabelaListaContatosPJ tbody tr').remove();
    $('#tabelaListaEmailsPJ tbody tr').remove();
    $('#tabelaListaSitesPJ tbody tr').remove();
}

/****************************************************************************************************************************************************************************/
/*INCLUIR CONTATOS PESSOA JURIDICA*/

// Executa funcao prepareUpload ao selecionar arquivos
$('#btnIncluirContatoPJ').on('click', incluirContatoPJ);

$('body').on('focus', '#contatoPJ', function () {
    try {
        var digitos = $('#formPessoaJuridicaContatos input:checked').attr('data-digitos');

        switch (digitos) {
            case '10':
                $(this).mask('(99) 9999-9999');
                break;
            case '11':
                $(this).mask('(99) 99999-9999');
                break;
            case '14':
                $(this).mask('(99) 9999-9999/9999 ');
        }
    }
    catch (error) {
        //console.log(error);
    }
});

// Adiciona os arquivos selecionados ao array files[] e exibe-os na tabela de arquivos selecionados
function incluirContatoPJ(event) {
    if (!formPessoaJuridicaContatosValidate.form())
        return false;

    if ($('#contatoPJ').val() != '') {
        $('#tabelaListaContatosPJ tbody').append('<tr><td>' + $('#contatoPJ').val() + '</td><td data-value="' + $('#formPessoaJuridicaContatos input:checked').attr('data-id') + '">' + $('#formPessoaJuridicaContatos input:checked').val() + '</td><td class="text-center colunaExcluir"><button class="btn btn-xs btn-danger btn-excluir"><i class="fa fa-remove"></i></button></td></tr>');
        //Limpa campo contato
        $('#contatoPJ').val('');
    }
}

/****************************************************************************************************************************************************************************/
/*INCLUIR EMAILS PESSOA JURIDICA*/

// Executa funcao prepareUpload ao selecionar arquivos
$('#btnIncluirEmailPJ').on('click', incluirEmailPJ);

// Adiciona os arquivos selecionados ao array files[] e exibe-os na tabela de arquivos selecionados
function incluirEmailPJ(event) {
    if (!formPessoaJuridicaEmailsValidate.form())
        return false;

    if ($('#emailPJ').val() != '') {
        $('#tabelaListaEmailsPJ tbody').append('<tr><td>' + $('#emailPJ').val() + '</td><td class="text-center colunaExcluir"><button class="btn btn-xs btn-danger btn-excluir"><i class="fa fa-remove"></i></button></td></tr>');
        //Limpa campo email
        $('#emailPJ').val('');
    }
}

/****************************************************************************************************************************************************************************/
/*INCLUIR SITES PESSOA JURIDICA*/

// Executa funcao prepareUpload ao selecionar arquivos
$('#btnIncluirSitePJ').on('click', incluirSitePJ);

// Adiciona os arquivos selecionados ao array files[] e exibe-os na tabela de arquivos selecionados
function incluirSitePJ(event) {
    if ($('#sitePJ').val() != '') {
        $('#tabelaListaSitesPJ tbody').append('<tr><td>' + $('#sitePJ').val() + '</td><td class="text-center colunaExcluir"><button class="btn btn-xs btn-danger btn-excluir"><i class="fa fa-remove"></i></button></td></tr>');
        //Limpa campo site
        $('#sitePJ').val('');
    }
}

/****************************************************************************************************************************************************************************/
/*BLOQUEIA E DESBLOQUEIA EDICAO PESSOA JURIDICA*/

function desabilitaCamposPJ() {
    $("input#cnpj").prop("readonly", true);
    $("input#razaosocial").prop("readonly", true);
    $("input#nomefantasia").prop("readonly", true);
    $("input#sigla").prop("readonly", true);

    $("select#esfera").prop("disabled", true);
    $("select#poder").prop("disabled", true);

    $("#formPessoaJuridica .campo-uf").prop("readonly", true);
    $("#formPessoaJuridica .campo-municipio").prop("readonly", true);
    $("input#contatoPJ").prop("readonly", true);
    $("input#emailPJ").prop("readonly", true);
    $("input#sitePJ").prop("readonly", true);
    $("button#btnIncluirContatoPJ").prop("readonly", true);
    $("button#btnIncluirEmailPJ").prop("readonly", true);
    $("button#btnIncluirSitePJ").prop("readonly", true);

    $("select#selecaoSetorDestino").prop("disabled", true);
    $("select#selecaoSetorDestino").prop("disabled", true);

    $.each($("#formPessoaJuridica .colunaExcluir"), function (i) {
        $(this).hide();
    });
}

function habilitaCamposPJ() {
    $("input#cnpj").prop("readonly", false);
    $("input#razaosocial").prop("readonly", false);
    $("input#nomefantasia").prop("readonly", false);
    $("input#sigla").prop("readonly", false);

    $("select#esfera").prop("disabled", false);
    $("select#poder").prop("disabled", false);

    $("#formPessoaJuridica .campo-uf").prop("readonly", false);
    $("#formPessoaJuridica .campo-municipio").prop("readonly", false);
    $("input#contatoPJ").prop("readonly", false);
    $("input#emailPJ").prop("readonly", false);
    $("input#sitePJ").prop("readonly", false);
    $("button#btnIncluirContatoPJ").prop("readonly", false);
    $("button#btnIncluirEmailPJ").prop("readonly", false);
    $("button#btnIncluirSitePJ").prop("readonly", false);

    $("select#selecaoSetorDestino").prop("disabled", false);
    $("select#selecaoSetorDestino").prop("disabled", false);

    $.each($("#formPessoaJuridica .colunaExcluir"), function (i) {
        $(this).show();
    });
}

/****************************************************************************************************************************************************************************/
/*INCLUSAO PESSOA JURIDICA*/

$('#tipoPJ').on('change', selecaoTipoPJ);

var selecaoTipo;

function selecaoTipoPJ(event) {
    selecaoTipo = event;

    if (event.currentTarget.value == 0) {
        $('#selecaoOrgaosExecutivoEstadual').hide();
        $('#selecaoSetorDestino').hide();
        $('.panelPJ').hide();
    }
    else if (event.currentTarget.value == 1 || event.currentTarget.value == 2) {
        $('#selecaoOrgaosExecutivoEstadual').show();
        $('#selecaoSetorDestino').show();
        $('.panelPJ').hide();
    }
    else {
        $('#selecaoOrgaosExecutivoEstadual').hide();
        $('#selecaoSetorDestino').hide();
        $('.panelPJ').show();
        resetaForm();
    }
}

/****************************************************************************************************************************************************************************/
/*LIMPA DADOS DOS FORMULARIOS*/

function limparFormPessoaJuridica() {
    $('#selecaoOrgaosExecutivoEstadual').hide();
    $('.panelPJ').hide();
    $('#formPessoaJuridica .campo-municipio option:not([value="0"])').remove();
    $('#selecaoSetorDestino option:not([value=""])').remove();
    $('#selecaoSetorDestino').hide();

    formPessoaJuridicaEmailsValidate.resetForm();
    //formPessoaJuridicaContatosValidate.resetaForm();
}

function resetaForm() {
    $('#modalInteressados div.tab-content .active form')[0].reset();
    limpaTabelasListasEmailContatoSite();
    $('select#organizacaoPublica option:not([value=""])').remove()
}

function resetaFormSelecaoPJ() {
    $('#tipoPJ').prop('selectedIndex', 0);
    $('#organizacaoPublica').val('');
}