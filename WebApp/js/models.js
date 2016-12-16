/****************************************************************************************************************************************************************************/
/*VARIÁVEIS GLOBAIS*/

//Lista de dados da autuacao
var arrayPJ = [];
var arrayPF = [];
var arrayAnexos = [];
var arrayMunicipios = [];
var arraySinalizacao = [];

//Lista de dados do interessado
var contatosPJ = [];
var emailsPJ = [];

var contatosPF = [];
var emailsPF = [];
//var sitesPJ = [];

//Objetos Provisorios
var interessadoPJProvisorio = null;
var interessadoPFProvisorio = null;

//Objeto Dados Iniciais Autuacao
var formAutuacao;


/****************************************************************************************************************************************************************************/
/*MODELOS OBJETOS*/

//Objeto Autuacao
function objetoAutuacao(idAtividade, resumo, interessadosPessoaFisica, interessadosPessoaJuridica,
    municipios, anexos, idSinalizacoes, idOrgaoAutuador, orgaoAutuador, siglaOrgaoAutuador, idUnidadeAutuadora,
    unidadeAutuadora, siglaUnidadeAutuadora, idUsuarioAutuador, usuarioAutuador) {
    this.idAtividade = idAtividade;
    this.resumo = resumo;
    this.interessadosPessoaFisica = interessadosPessoaFisica;
    this.interessadosPessoaJuridica = interessadosPessoaJuridica;
    this.municipios = municipios;
    this.anexos = anexos;
    this.idSinalizacoes = idSinalizacoes;
    this.idOrgaoAutuador = idOrgaoAutuador;
    this.nomeOrgaoAutuador = orgaoAutuador;
    this.siglaOrgaoAutuador = siglaOrgaoAutuador;
    this.idUnidadeAutuadora = idUnidadeAutuadora;
    this.nomeUnidadeAutuadora = unidadeAutuadora;
    this.siglaUnidadeAutuadora = siglaUnidadeAutuadora;
    this.idUsuarioAutuador = idUsuarioAutuador;
    this.nomeUsuarioAutuador = usuarioAutuador;
}

//Objeto Interessado Pessoa Juridica
function objetoInteressadoPJ(razaoSocial, cnpj, sigla, nomeUnidade, siglaUnidade, contatos, emails, guidMunicipio, tipo) {
    this.razaoSocial = razaoSocial;
    this.cnpj = cnpj;
    this.sigla = sigla;
    this.nomeUnidade = nomeUnidade;
    this.siglaUnidade = siglaUnidade;
    this.contatos = contatos;
    this.emails = emails;
    this.guidMunicipio = guidMunicipio;
    this.tipo = tipo;
}

//Objeto Interessado Pessoa Física
function objetoInteressadoPF(nome, cpf, contatos, emails, guidMunicipio) {
    this.nome = nome;
    this.cpf = cpf;
    this.contatos = contatos;
    this.emails = emails;
    this.guidMunicipio = guidMunicipio;
}


//Objeto Municipio
function objetoMunicipio(guidMunicipio, uf, municipio) {
    this.guidMunicipio = guidMunicipio;
    this.uf = uf;
    this.municipio = municipio;
};

//Objeto Anexo
function objetoAnexos(nome, conteudo, tipo) {
    this.nome = nome;
    this.conteudo = conteudo;
    this.tipo = tipo;
};

//Objeto Email
function objetoEmail(endereco) {
    this.endereco = endereco;
};

//Objeto Contato
function objetoContato(telefone, idTipoContato) {
    this.telefone = telefone;
    this.idTipoContato = 3;
};