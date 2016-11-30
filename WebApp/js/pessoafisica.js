/****************************************************************************************************************************************************************************/
/*INCLUIR CONTATOS PESSOA FISICA*/

// Executa funcao prepareUpload ao selecionar arquivos
$('#btnIncluirContatoPF').on('click', incluirContatoPJ);

// Adiciona os arquivos selecionados ao array files[] e exibe-os na tabela de arquivos selecionados
function incluirContatoPJ(event) {
    if (!formPessoaFisicaContatosValidate.form())
        return false;

    if ($('#contatoPF').val() != '') {
        $('#tabelaListaContatosPF tbody').append('<tr><td>' + $('#contatoPF').val() + '</td><td data-value="' + $('#formPessoaFisica input:checked').attr('data-id') + '">' + $('#formPessoaFisica input:checked').val() + '</td><td class="text-center colunaExcluir"><button class="btn btn-xs btn-danger btn-excluir"><i class="fa fa-remove"></i></button></td></tr>');
        //Limpa campo contato
        $('#contatoPF').val('');
    }
}

/****************************************************************************************************************************************************************************/
/*INCLUIR EMAILS PESSOA FISICA*/

// Executa funcao prepareUpload ao selecionar arquivos
$('#btnIncluirEmailPF').on('click', incluirEmailPJ);

// Adiciona os arquivos selecionados ao array files[] e exibe-os na tabela de arquivos selecionados
function incluirEmailPJ(event) {
    if (!formPessoaFisicaEmailsValidate.form())
        return false;

    if ($('#emailPF').val() != '') {
        $('#tabelaListaEmailsPF tbody').append('<tr><td>' + $('#emailPF').val() + '</td><td class="text-center colunaExcluir"><button class="btn btn-xs btn-danger btn-excluir"><i class="fa fa-remove"></i></button></td></tr>');
        //Limpa campo email
        $('#emailPF').val('');
    }
}

/****************************************************************************************************************************************************************************/
/*LIMPAR FORMULARIO PESSOA FISICA*/

function limparFormPessoaFisica() {
    $('#formPessoaFisica .campo-municipio option:not([value=""])').remove();
    $('#formPessoaFisica')[0].reset();

    limpaTabelasListasEmailContatoSitePF();

    formPessoaFisicaEmailsValidate.resetForm();
    formPessoaFisicaContatosValidate.resetForm();
}


/****************************************************************************************************************************************************************************/
/*LIMPA DADOS DAS TABELAS DE EMAIL, CONTATO E SITE*/

function limpaTabelasListasEmailContatoSitePF() {
    $('#tabelaListaContatosPF tbody tr').remove();
    $('#tabelaListaEmailsPF tbody tr').remove();
}