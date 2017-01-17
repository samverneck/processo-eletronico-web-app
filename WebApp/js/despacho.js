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
          toastr["warning"]("Não foi possível realizar esta operação!")
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

          var optionhtml = "<option value='0'>Selecione uma Unidade</option>";

          $.each(dados, function (i) {
              optionhtml += '<option value="' + this.Value + '">' + this.Text + '</option>';
          });

          $("#unidadeDestino").html(optionhtml);
      })
      .fail(function () {
          toastr["warning"]("Não foi possível realizar esta operação!")
      });
}

//Despachar Processo
$('body').on('click', '#btnDespacharProcesso', function () {

    if (!validaForm('#formDespacho')) {
        toastr["warning"]("Não foi possíve realizar a autuação! Verifique os campos obrigatórios e tente novamente!");
        return false;
    }

    var form = $('#formDespacho')[0];

    var despacho = new objetoDespacho(form.idProcesso.value, form.textoDespacho.value, form.orgaoDestino.value, unidadeDestino.value, prepararAnexos(arrayAnexos));

    //console.log(despacho);

    $.ajax({
        url: '/Home/DespacharProcessoPost',
        type: 'POST',
        data: JSON.stringify(despacho),
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
        toastr["warning"]("Não foi possível realizar o despacho do processo!");        
    });

    return false;
});