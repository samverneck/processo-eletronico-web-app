/****************************************************************************************************************************************************************************/
/*CARREGA PROCESSO*/
$('#formConsultaProcesso').submit(function (event) {    
    ajaxCarregaProcessoVisualizar($('#numeroProcesso').val());
    return false;
});




//Carrega orgaos publicos do executivo estadual ES para o combo
function ajaxCarregaProcessoVisualizar(numeroProcesso) {
    $.ajax('/home/VisualizarProcesso?numeroProcesso=' + numeroProcesso)
      .done(function (dados) {          
          $('#visualizarProcesso').html(dados);
          $('#content-1').slideDown();
          $('#content-2').slideUp();
          $('#modalProcesso').modal('show');
          ExibirMensagens();
      })
      .fail(function () {
          ExibirMensagens();          
      });
}


$('body').on('click', '.btn-download', function () {
    $.ajax($(this).prop('href'))
      .done(function (dados) {

          //console.log(dados);

          var link = document.createElement('a');
          link.download = dados[1];
          link.href = dados[0];
          link.click();
      })
      .fail(function () {
          toastr["warning"]("Não foi possível realizar esta operação!");
      });
    return false;
});

/****************************************************************************************************************************************************************************/
/*CARREGA DESPACHO*/
$('body').on('click', '.btn-visualizar-despacho', function () {
    $.ajax($(this).prop('href'))
      .done(function (dados) {
          $('#visualizarDespacho').html(dados);          
          $('#content-1').slideUp();
          $('#content-2').slideDown();
      })
      .fail(function () {
          toastr["warning"]("Não foi possível exibir o despacho selecionado!");
      });
    return false;
});

$('body').on('click', '#btn-voltar-despacho', function () {    
    $('#content-1').slideDown();
    $('#content-2').slideUp();
    return false;
});


$('body').on('click', '#btn-imprimir-despacho', function () {
    printDiv("#visualizarDespacho");
});
$('body').on('click', '#btn-imprimir-processo', function () {
    printDiv("#visualizarProcesso");
});

//TELA DE IMPRESSSAO MONTADA A PARTIR DO CONTEUDO DO MODAL DE CONSULTA
function printDiv(divID) {
    var corpoInicio = '<!DOCTYPE html><html><head><link rel="stylesheet" href="/css/print.css" type="text/css" media="all"><meta name="viewport" content="width=device-width" /><title>Consulta Processo Web - Impressão</title></head><body><div class="row">';
    corpoInicio += '<div class="col-xs-12 form-group text-center hidden-print"><button type="button" class="btn btn-default" onclick="javascript:window.print();"><i class="glyphicon glyphicon-print"></i> Imprimir</button> <button type="button" class="btn btn-default" onclick="javascript:window.close();"><i class="glyphicon glyphicon-remove-circle"></i> Cancelar</button></div></div>';
    var cabecalho = '<div class="row"><div class="col-xs-6"><img class="logo-sep" src="/img/Brasao_Governo_horizontal_Black_left_small.png"/></div></div><hr/>';
    var conteudo = $(divID).clone();

    $.each(conteudo.find('#anexos-despacho tr, #anexos-processos tr'), function (i, tr) {
        $(tr).children('td,th').eq(3).remove();
    });

    $.each(conteudo.find('#tabela-despachos tr'), function (i, tr) {
        $(tr).children('td,th').eq(3).remove();
    });

    conteudo.find('.btn-box-tool').remove();
    conteudo.find('.box-body').show();
    conteudo.find('#historicoprocesso').removeClass();
    conteudo.find('#historicoprocesso').removeAttr('style');
    var corpoFim = '</body></html>';
    var win = window.open();
    win.document.write(corpoInicio + cabecalho + conteudo.html() + corpoFim);
}