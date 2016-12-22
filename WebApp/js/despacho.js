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

//Despachar Processo
$('body').on('click', '#btnDespacharProcesso', function () {
    var form = $('#formDespacho')[0];

    var despacho = new objetoDespacho(form.idProcesso.value, form.textoDespacho.value, form.orgaoDestino.value, unidadeDestino.value, prepararAnexos(arrayAnexos));

    console.log(despacho);

    $.ajax({
        url: '/Home/DespacharProcessoPost',
        type: 'POST',
        data: JSON.stringify(despacho),
        dataType: 'json',
        contentType: "application/json; charset=utf-8"
        //contentType: "application/x-www-form-urlencoded"
    }).done(function (dados, textStatus, request) {

        console.log(dados);

        switch ($.trim(dados)) {
            case '400':
                toastr["warning"]("Não foi possível realizar esta operação.");
                break;
            case '404':
                toastr["warning"]("Não foi possível realizar esta operação.");
                break;
            case '500':
                toastr["error"]("Erro reportado pela API.");
                break;
            case '201': {
                toastr["success"]("Processo despachado com sucesso!");
                window.location.href = '/Home';
            }
        }

    }).fail(function () {
        toastr["warning"]("Não foi possível realizar esta operação!");        
    });

    return false;
});