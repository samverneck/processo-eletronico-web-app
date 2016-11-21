/****************************************************************************************************************************************************************************/
/*VARIÁVEIS GLOBAIS*/

var arrayPJ = [];
var arrayPF = [];
var arrayAnexos = [];
var arrayAbrangencia = [];

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
/*CARREGA ORGÃOS DO EXECUTIVO ESTADUAL*/
$('#tipoPJ').on('change', carregaOrgaosExecutivoEstadual);

function carregaOrgaosExecutivoEstadual(event) {
    var elemento = event.currentTarget;

    if (elemento.value == '1' && $('#orgaosExecutivoEstadual').children().length == 0) {        
        ajaxCarregaOrgaosExecutivoEstadual();
    }
    else {
        habilitaCamposPJ();
    }
}

function ajaxCarregaOrgaosExecutivoEstadual() {
    $.ajax('/Autuacao/OrganizacoesPorEsferaPoderUf?esfera=estadual&poder=&uf=es')
      .done(function (dados) {

          $.each(dados, function (i) {
              var optionhtml = '<option value="' + this.id + '">' + this.sigla + ' - ' + this.razaoSocial + '</option>';
              $('#orgaosExecutivoEstadual').append(optionhtml);
          });

          console.log(dados.length);

      })
      .fail(function () {
          alert("error");
      });
}

/****************************************************************************************************************************************************************************/
/*CARREGA DADOS DE ORGÃO DO EXECUTIVO ESTADUAL*/
//$('#orgaosExecutivoEstadual').on('change', carregaDadosOrgaoExecutivoEstadual);
$('#btnIncluirOrgaoExecutivoEstadual').on('click', carregaDadosOrgaoExecutivoEstadual);


function carregaDadosOrgaoExecutivoEstadual(event) {
    var elemento = $('#orgaosExecutivoEstadual');
    ajaxCarregaDadosOrgaoExecutivoEstadual(elemento[0]);
    $('#panelPJ').show();
}

function ajaxCarregaDadosOrgaoExecutivoEstadual(elemento) {
    $.ajax('/Autuacao/OrganizacaoPorId/' + elemento.value)
      .done(function (dados) {

          console.log(dados);

          $("input#cnpj").val(dados.cnpj);
          $("input#razaosocial").val(dados.razaoSocial);
          $("input#nomefantasia").val(dados.nomeFantasia);
          $("input#sigla").val(dados.sigla);
          $("select#esfera").val(dados.esfera.descricao);
          $("select#poder").val(dados.poder.descricao);
          $("#formPessoalJuridica .campo-uf").val(dados.endereco.municipio.uf.toLowerCase());

          /*Insere municipio*/
          $("#formPessoalJuridica .campo-uf").trigger("change");//Dispara evento change do campo uf
          $("#formPessoalJuridica .campo-municipio").val(dados.endereco.municipio.nome);

          /*Insere emails*/
          $.each(dados.emails, function (i, v) {
              $('#emailPJ').val(v.endereco);
              $('#btnIncluirEmailPJ').trigger("click");
          });

          /*Insere contatos*/
          $.each(dados.contatos, function (i, v) {
              $('#contatoPJ').val(v.telefone);
              $('#btnIncluirContatoPJ').trigger("click");
          });

          /*Insere sites*/
          $.each(dados.sites, function (i, v) {
              $('#sitePJ').val(v.url);
              $('#btnIncluirSitePJ').trigger("click");
          });          

          desabilitaCamposPJ();
      })
      .fail(function () {
          alert("error");
      });
}

/****************************************************************************************************************************************************************************/
/*INCLUIR CONTATOS PESSOA JURIDICA*/

var contatosPJ = [];

// Executa funcao prepareUpload ao selecionar arquivos
$('#btnIncluirContatoPJ').on('click', incluirContatoPJ);

// Adiciona os arquivos selecionados ao array files[] e exibe-os na tabela de arquivos selecionados
function incluirContatoPJ(event) {
    if ($('#contatoPJ').val() != '') {
        $('#tabelaListaContatosPJ tbody').append('<tr><td>' + $('#contatoPJ').val() + '</td><td class="text-center colunaExcluir"><button class="btn btn-xs btn-danger btn-excluir"><i class="fa fa-remove"></i></button></td></tr>');
        $('#contatoPJ').val('');
    }
}

/****************************************************************************************************************************************************************************/
/*INCLUIR EMAILS PESSOA JURIDICA*/

var emailsPJ = [];

// Executa funcao prepareUpload ao selecionar arquivos
$('#btnIncluirEmailPJ').on('click', incluirEmailPJ);

// Adiciona os arquivos selecionados ao array files[] e exibe-os na tabela de arquivos selecionados
function incluirEmailPJ(event) {
    if ($('#emailPJ').val() != '') {
        $('#tabelaListaEmailsPJ tbody').append('<tr><td>' + $('#emailPJ').val() + '</td><td class="text-center colunaExcluir"><button class="btn btn-xs btn-danger btn-excluir"><i class="fa fa-remove"></i></button></td></tr>');
        $('#emailPJ').val('');
    }
}

/****************************************************************************************************************************************************************************/
/*INCLUIR SITES PESSOA JURIDICA*/

var sitesPJ = [];

// Executa funcao prepareUpload ao selecionar arquivos
$('#btnIncluirSitePJ').on('click', incluirSitePJ);

// Adiciona os arquivos selecionados ao array files[] e exibe-os na tabela de arquivos selecionados
function incluirSitePJ(event) {
    if ($('#sitePJ').val() != '') {
        $('#tabelaListaSitesPJ tbody').append('<tr><td>' + $('#sitePJ').val() + '</td><td class="text-center colunaExcluir"><button class="btn btn-xs btn-danger btn-excluir"><i class="fa fa-remove"></i></button></td></tr>');
        $('#sitePJ').val('');
    }
}

/****************************************************************************************************************************************************************************/
/*BLOQUEIA E DESBLOQUEIA EDICAO PESSOA JURIDICA*/

function desabilitaCamposPJ() {
    $("input#cnpj").prop("disabled", true);
    $("input#razaosocial").prop("disabled", true);
    $("input#nomefantasia").prop("disabled", true);
    $("input#sigla").prop("disabled", true);
    $("select#esfera").prop("disabled", true);
    $("select#poder").prop("disabled", true);
    $("#formPessoalJuridica .campo-uf").prop("disabled", true);
    $("#formPessoalJuridica .campo-municipio").prop("disabled", true);
    $("input#contatoPJ").prop("disabled", true);
    $("input#emailPJ").prop("disabled", true);
    $("input#sitePJ").prop("disabled", true);
    $("button#btnIncluirContatoPJ").prop("disabled", true);
    $("button#btnIncluirEmailPJ").prop("disabled", true);
    $("button#btnIncluirSitePJ").prop("disabled", true);

    $.each($("#formPessoalJuridica .colunaExcluir"), function (i) {
        $(this).hide();
    });
}

function habilitaCamposPJ() {
    $("input#cnpj").prop("disabled", false);
    $("input#razaosocial").prop("disabled", false);
    $("input#nomefantasia").prop("disabled", false);
    $("input#sigla").prop("disabled", false);
    $("select#esfera").prop("disabled", false);
    $("select#poder").prop("disabled", false);
    $("#formPessoalJuridica .campo-uf").prop("disabled", false);
    $("#formPessoalJuridica .campo-municipio").prop("disabled", false);
    $("input#contatoPJ").prop("disabled", false);
    $("input#emailPJ").prop("disabled", false);
    $("input#sitePJ").prop("disabled", false);
    $("button#btnIncluirContatoPJ").prop("disabled", false);
    $("button#btnIncluirEmailPJ").prop("disabled", false);
    $("button#btnIncluirSitePJ").prop("disabled", false);

    $.each($("#formPessoalJuridica .colunaExcluir"), function (i) {
        $(this).show();
    });
}

/****************************************************************************************************************************************************************************/
/*CARREGA MUNICIPIOS*/
$('.campo-uf').on('change', carregaMunicipios);

function carregaMunicipios(event) {
    var elemento = event.currentTarget;

    $(elemento).closest('.row').find('.campo-municipio option:not([value="0"])').remove();

    if (elemento.value !== '0') {
        ajaxCarregaMunicipios(elemento);
    }
}

function ajaxCarregaMunicipios(elemento) {
    $.ajax({ url: '/Autuacao/MunicipiosPorUf?uf=' + elemento.value, async: false })
      .done(function (dados) {

          $.each(dados, function (i) {
              var optionhtml = '<option value="' + this.nome + '">' + this.nome + '</option>';
              $(elemento).closest('.row').find('.campo-municipio').append(optionhtml);
          });

          console.log(dados.length);

      })
      .fail(function () {
          alert("error");
      });
}

/****************************************************************************************************************************************************************************/
/*INCLUSAO INTERESSADOS*/

$('#btnIncluirInteressado').on('click', function () {
    $('#modalInteressados div.tab-content .active form')[0].reset();

    var form = $('#modalInteressados div.tab-content .active form')[0];

    if ($(form).prop('id') == 'formPessoalJuridica') {
        limparFormPessoaJuridica();
    }
    if ($(form).prop('id') == 'formPessoalFisica') {
        limparFormPessoaFisica();
    }
});

/****************************************************************************************************************************************************************************/
/*INCLUSAO PESSOA JURIDICA*/

$('#tipoPJ').on('change', selecaoTipoPJ);

var selecaoTipo;

function selecaoTipoPJ(event) {
    selecaoTipo = event;

    if (event.currentTarget.value == 0) {
        $('#selecaoOrgaosExecutivoEstadual').hide();
        $('#panelPJ').hide();
    }
    else if (event.currentTarget.value == 1) {
        $('#selecaoOrgaosExecutivoEstadual').show();
        $('#panelPJ').hide();
    }
    else {
        $('#selecaoOrgaosExecutivoEstadual').hide();
        $('#panelPJ').show();
    }
}

function carregaOrgaoExecutivoEstadual(event) {

}

function limparFormPessoaJuridica() {
    $('#selecaoOrgaosExecutivoEstadual').show();
    $('#panelPJ').hide();
    $('#formPessoalJuridica .campo-municipio option:not([value="0"])').remove();
}

/****************************************************************************************************************************************************************************/
/*INCLUSAO PESSOA FISICA*/

function limparFormPessoaFisica() {
    $('#formPessoalFisica .campo-municipio option:not([value="0"])').remove();
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

$(document).ready(function () {
    $('.btn-input-file').click(function () {
        $(this).closest($(".input-file").click());
    });
});

/****************************************************************************************************************************************************************************/
/*INCLUIR MUNICIPIOS*/

var municipiosArray = [];

// Executa funcao prepareUpload ao selecionar arquivos
$('#btnIncluirMunicipio').on('click', prepareMunicipios);

// Adiciona os arquivos selecionados ao array files[] e exibe-os na tabela de arquivos selecionados
function prepareMunicipios(event) {
    if ($('#municipio').val() != '0' && $('#uf').val() != '0') {
        $('#tabelaMunicipios tbody').append('<tr><td>' + $('#uf').val().toUpperCase() + '</td><td>' + $('#municipio').val() + '</td><td class="text-center colunaExcluir"><button id="' + $('#municipio').val() + '" class="btn btn-xs btn-danger btn-excluir"><i class="fa fa-remove"></i></button></td></tr>');

        $('#municipio').prop("selectedIndex", 0);
    }
}

// Exclui municipio selecionado
$('#tabelaMunicipios tbody').on('click', '.btn-excluir-municipio', function () {
    $(this).closest('tr').remove();
});

/****************************************************************************************************************************************************************************/
/*EXCLUIR ITEM DA TABELA*/

$('tbody').on('click', '.btn-excluir', function () {
    $(this).closest('tr').remove();
});

/****************************************************************************************************************************************************************************/
/*UPLOAD DE ARQUIVOS*/

var files = [];

// Executa funcao prepareUpload ao selecionar arquivos
$('input[type=file]').on('change', prepareUpload);

// Adiciona os arquivos selecionados ao array files[] e exibe-os na tabela de arquivos selecionados
function prepareUpload(event) {
    $('#tabelaAnexos tbody tr').remove();

    $.each(event.target.files, function (i, obj) {
        //console.log(obj);
        files.push(obj);
    });

    $.each(files, function (i, file) {
        $('#tabelaAnexos tbody').append('<tr><td>' + file.name + '</td><td>' + bytesToSize(file.size) + '</td><td class="text-center colunaExcluir"><button id="' + file.name + '" class="btn btn-xs btn-danger btn-excluir-anexo"><i class="fa fa-remove"></i></button></td></tr>');
    });
}

// Exclui or arquivo selecionado do array files[] e da tabela de arquivos
$('#tabelaAnexos tbody').on('click', '.btn-excluir-anexo', function () {
    files.splice($.inArray($(this).attr('id'), files), 1);
    $(this).closest('tr').remove();
    //$.each(files, function (i, file) {
    //    console.log(file.name);
    //});
});

/****************************************************************************************************************************************************************************/
/*ENVIAR ARQUIVOS*/

$('form').on('submit', uploadFiles);

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
        processData: false, // Don't process the files
        contentType: false, // Set content type to false as jQuery will tell the server its a query string request
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