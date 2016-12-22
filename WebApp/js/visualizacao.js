/****************************************************************************************************************************************************************************/
/*CARREGA PROCESSO*/
var formSearch = $("#formConsultaProcesso").validate(
    {
        messages: {
            numeroProcesso: "Informe o número do processo."
        }
    });

$('#formConsultaProcesso').submit(function (event) {
    console.log(formSearch.checkForm());
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