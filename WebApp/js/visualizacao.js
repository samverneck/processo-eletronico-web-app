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
          $('#modalProcesso').modal('show');
      })
      .fail(function () {
          toastr["warning"]("Não foi possível realizar esta operação!");
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