# Clube da Leitura

![Status Finalizado](https://img.shields.io/badge/Status-Finalizado-green?color=Green)

## Demonstração
### Cadastros
> ![gif de cadastro](https://i.imgur.com/ZFoUmwT.gif)

### Empréstimos
> ![gif de emprestimo](https://i.imgur.com/jEgZnS6.gif)

## Índice

- [Introdução](#introdução)
- [Tecnologias usadas](#tecnologias-usadas)
- [Funcionalidades](#funcionalidades)
- [Estrutura do Projeto](#estrutura-do-projeto)
- [Como Usar](#como-usar)
- [Sobre o Projeto](#sobre-o-projeto)
- [Autor](#autor)

## Introdução

Clube da Leitura é uma aplicação de console desenvolvida em C# com o propósito de gerenciar um clube de leitura. Através de uma interface simples via terminal, o usuário pode cadastrar livros, revistas, amigos, controlar empréstimos e visualizar os registros organizados em listas.

## Tecnologias usadas

[![Tecnologias](https://skillicons.dev/icons?i=git,github,cs,dotnet,visualstudio)](https://skillicons.dev)

## Funcionalidades

- **Cadastro de Livros e Revistas**: Armazene e consulte informações de obras literárias e periódicos.
- **Cadastro de Amigos**: Registre os membros do clube que podem realizar empréstimos.
- **Gestão de Empréstimos**: Empréstimos e devoluções com controle de datas.
- **Interface de Menu Interativo**: Navegação por menus via terminal com opções claras.
- **Validações de Entrada**: Prevenção de erros por meio de verificações e feedbacks amigáveis.

## Estrutura do Projeto

- `Entidades` – Representações de objetos principais como Livro, Revista, Amigo e Empréstimo.
- `Telas` – Menus responsáveis pela interação com o usuário.
- `Utilitarios` – Métodos auxiliares para exibição, entrada e validação.
- `Program.cs` – Ponto de entrada principal da aplicação, onde o menu principal é exibido.

## Como Usar

### Requisitos

- .NET SDK (recomendado .NET 8.0 ou superior) para compilação e execução do projeto.

### 1. Clone o repositório.
 
```
git clone https://github.com/AgathaSates/Clube-da-Leitura.git
```
### 2. Abra o terminal ou o prompt de comando e navegue até a pasta raiz

```
cd Clube-da-Leitura
```

### 3. Utilize o comando abaixo para restaurar as dependências do projeto.

```
dotnet restore
```

### 4. Navegue até a pasta do projeto

```
cd Clube-da-Leitura.ConsoleApp
```

### 5. Execute o projeto

```
dotnet run
```

## Sobre o Projeto
Desenvolvido durante o curso Fullstack da [Academia do Programador](https://academiadoprogramador.net) 2025

## Autor

- [Agatha Sates](https://github.com/AgathaSates) – Estudante de Desenvolvimento fullstack na [Academia do Programador](https://academiadoprogramador.net)