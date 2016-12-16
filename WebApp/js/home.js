/****************************************************************************************************************************************************************************/
/*FORMATA TABELA CAIXA DE ENTRADA*/
$(document).ready(function () {
    var caixaEntradaOrgao = $('#tabelaCaixaSetor').DataTable({
        "dom": '<"pull-left"l><"pull-right"f>rt<"pull-left"i><"pull-right"p>',
        "lengthMenu": [[5, 10, 25, 50, -1], [5, 10, 25, 50, "All"]],
        "sPaginationType": "full_numbers",
        "language": {
            "lengthMenu": " _MENU_ Processos por página",
            "zeroRecords": "Nenhuma processo encontado",
            "info": "Página _PAGE_ de _PAGES_",
            "infoEmpty": "",
            "infoFiltered": "(Registros filtrados do total de _MAX_ processos.)",
            "sSearch": "Filtrar: ",
            "paginate": {
                "previous": "‹",
                "next": "›",
                "first": "«",
                "last": "»"
            }
        },
        "ordering": false,
        "sort": false,
    });

    caixaEntradaOrgao.draw();
});

$('table').on('click', '.btn-processo-visualizar', function (event) {
    //$('#visualizarProcesso').html('');
    ajaxCarregaProcessoVisualizar(this.getAttribute('data-id'));
});