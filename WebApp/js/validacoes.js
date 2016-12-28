
//var formResumoSinalizacaoValidate = $("#formResumoSinalizacao").validate();

//var formAutuacaoResponsavelValidate = $("#formAutuacaoResponsavel").validate();

//var formResumoSinalizacaoValidate = $("#formResumoSinalizacao").validate();

//var formAutuacaoResponsavelValidate = $("#formAutuacaoResponsavel");

//var formAutuacaoMunicipioValidate = $("#formAutuacaoMunicipio").validate({
//    errorLabelContainer: '.msgErrorMunicipio'
//});

//var formAutuacaoAnexosValidate = $("#formAutuacaoAnexos").validate();

/****************************************************************************************************************************************************************************/
/*VALIDAR FORMULARIO INTERESSADO PESSOA FISICA*/

var formPessoaFisicaValidate = $("#formPessoaFisica").validate(
    {
        ignore: "#contatoPF, #emailPF",
        debug: true
    }
);

var formPessoaFisicaContatosValidate = $("#formPessoaFisicaContatos").validate(
    {
        debug: true,
        errorLabelContainer: '#msgErrorContatosPF'
    }
);

var formPessoaFisicaEmailsValidate = $("#formPessoaFisicaEmails").validate(
    {
        debug: true,
        errorLabelContainer: '#msgErrorEmailsPF'
    }
);

/****************************************************************************************************************************************************************************/
/*VALIDAR FORMULARIO INTERESSADO PESSOA JURIDICA*/

var formPessoaJuridicaValidate = $("#formPessoaJuridica").validate(
    {
        ignore: "#contatoPJ, #emailPJ",
        debug: true
    }
);


var formPessoaJuridicaContatosValidate = $("#formPessoaJuridicaContatos").validate(
    {
        debug: true,
        errorLabelContainer: '#msgErrorContatosPJ'
    }
);

var formPessoaJuridicaEmailsValidate = $("#formPessoaJuridicaEmails").validate(
    {
        debug: true,
        errorLabelContainer: '#msgErrorEmailsPJ'
    }
);