<div align="center">
    <h1 align="center">
        <img height="120" width="120" alt=".NET Core" src="https://upload.wikimedia.org/wikipedia/commons/thumb/e/ee/.NET_Core_Logo.svg/2048px-.NET_Core_Logo.svg.png"/>
    </h1>
</div>

# Brazil Random API - Geração de Dados Brasileiros

Este projeto consiste em dois microserviços que geram dados de endereço e nomes no Brasil.

## Requisitos

Certifique-se de ter as seguintes ferramentas e componentes instalados em seu sistema:

- .NET Framework (versão compatível com o projeto)
- SQL Server (ou outro banco de dados compatível)

## Configuração do Banco de Dados

- Instale e configure o SQL Server em seu sistema, se ainda não estiver instalado.
- Crie um novo banco de dados vazio com o nome "Datasets".
- Execute o script SQL fornecido na raiz do projeto (scripts.sql) no banco de dados "Datasets" para criar as tabelas necessárias e realizar outras configurações específicas do banco de dados.
- Atualize as informações de conexão do banco de dados nos arquivos de configuração dos projetos Endereco.API e Pessoa.API.

## Executando os Microserviços

Siga as etapas abaixo para configurar e executar os microserviços:

### Microserviço 1: Endereco.API

- Abra o projeto Enderecos.API no Visual Studio ou na sua IDE de preferência.
- Certifique-se de que as dependências do projeto estejam instaladas corretamente. Caso contrário, use o NuGet Package Manager para restaurar as dependências necessárias.
- Compile o projeto para garantir que não haja erros de compilação.
- Execute o projeto Enderecos.API pressionando F5 ou usando a opção "Executar" na sua IDE.
- O microserviço Enderecos.API será executado em um servidor local e poderá ser acessado por meio do navegador no endereço https://localhost:44341/swagger/index.html.

### Microserviço 2: Pessoa.API

- Abra o projeto Pessoas.API no Visual Studio ou na sua IDE de preferência.
- Certifique-se de que as dependências do projeto estejam instaladas corretamente. Caso contrário, use o NuGet Package Manager para restaurar as dependências necessárias.
- Compile o projeto para garantir que não haja erros de compilação.
- Execute o projeto Pessoas.API pressionando F5 ou usando a opção "Executar" na sua IDE.
- O microserviço Pessoas.API será executado em um servidor local e poderá ser acessado por meio do navegador no endereço https://localhost:44398/swagger/index.html.
