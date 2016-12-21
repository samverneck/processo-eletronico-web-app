/****************************************************************************************************************************************************************************/
/*CARREGA PROCESSO*/

$('#search-btn').click(function (event) {    
    ajaxCarregaProcessoVisualizar($('#numeroProcesso').val());
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