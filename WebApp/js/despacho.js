/****************************************************************************************************************************************************************************/
/*DESPACHAR PROCESSO*/

$('table').on('click', '.btn-processo-despachar', function (event) {
    //$('#visualizarProcesso').html('');
    ajaxCarregaProcessoDespachar(this.getAttribute('data-id'));
});

//Carrega orgaos publicos do executivo estadual ES para o combo
function ajaxCarregaProcessoDespachar(numeroProcesso) {
    $.ajax('/home/DespacharProcesso?numeroProcesso=' + numeroProcesso)
      .done(function (dados) {

          $('#despachoProcesso').html(dados);
          $('#modalDespacho').modal('show');
      })
      .fail(function () {
          toastr["warning"]("Não foi possível realizar está operação!")
      });
}

/****************************************************************************************************************************************************************************/
/*CARREGA UNIDADES*/
function carregaUnidades(_orgao) {
    if (_orgao != 0) {
        ajaxCarregaUnidades(_orgao);
    }
}

//Carrega orgaos publicos do executivo estadual ES para o combo
function ajaxCarregaUnidades(guidOrganizacao) {
    $.ajax({ url: '/home/unidadesPorOrganizacao', type: 'POST', data: { 'guidOrganizacao': guidOrganizacao } })
      .done(function (dados) {

          arrayAnexos = [];

          var optionhtml = "<option value='0'>Selecione uma Unidade</option>";

          $.each(dados, function (i) {
              optionhtml += '<option value="' + this.Value + '">' + this.Text + '</option>';
          });

          $("#unidadeDestino").html(optionhtml);
      })
      .fail(function () {
          toastr["warning"]("Não foi possível realizar está operação!")
      });
}