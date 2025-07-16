

2 - Aplicação de padrões de projeto
Deve ser implementado pelo menos um padrão de projeto, como Singleton, Strategy ou Repository, mostrando o padrão foi usado e porque. Deve ser indicado no Readme.md o nome do código fonte e os números das linhas onde se encontra a implementação.
R:#Utilizamos o Repository em todas as entidades, dentro das camada de Infraestrutura estão os mesmos, Foram utilizados para Isolar a lógica de acesso a dados da lógica de negócios e para separação de responsabilidades. #

3 - Princípios SOLID em prática
Pelo menos um dos princípios do SOLID, como por exemplo o Single Responsibility ou Open/Closed. Deve ser indicado no Readme.md o nome do código fonte e os números das linhas onde se encontra a implementação.

### Princípio SOLID Aplicado

**SRP - Single Responsibility Principle**

A classe `ClienteService` possui uma única responsabilidade: gerenciar a lógica de negócio da entidade `Cliente`. Todas as outras responsabilidades (validação, persistência, mapeamento, etc.) estão separadas em suas próprias classes.

Arquivo:ClienteService.cs
Local:NexusGym.Application.Services
Linhas:Todo o codigo
Motivo: A separação clara entre camada de serviço, repositório, DTO e validação demonstra aplicação direta do princípio da responsabilidade única (SRP).

4 - Convenções de nomenclatura claras
Variáveis, métodos e propriedades em português, sem abreviações, que refletem o conteúdo (por exemplo, RepositorioUsuario, ServicoAutenticacao). Deve ser indicado no Readme.md o nome do código fonte e os números das linhas onde se encontra a implementação.

R:O projeto segue convenções de nomenclatura em português, utilizando nomes descritivos e sem abreviações, facilitando a compreensão do código,

Arquivo:ClienteRepository.cs
Local:NexusGym.Infrastructure.Repositories  
Linhas principais:** 17 a 49
Exemplos:ListarClientes, ObterClientePorId, AdicionarCliente, AtualizarCliente.

5 - Documentação mínima de código
Comentários objetivos em pontos estratégicos (como em métodos complexos) ajudam a entender a lógica sem detalhar linha a linha. Comentário inteligente. Deve ser indicado no Readme.md o nome do código fonte e os números das linhas onde se encontra a implementação.

R: O projeto possui comentários objetivos em pontos estratégicos para explicar validações e decisões de negócio, sem detalhar linha a linha.

Exemplo:
Arquivo: ClienteService.cs
Local: NexusGym.Application.Services
Linhas: 50 a 65
Comentário: Explica as etapas de validação antes de atualizar um cliente, como garantir que o CPF seja único e o cliente exista.


6 - Testes automatizados
Presença pelo menos um teste unitário usando xUnit. Deve ser indicado no Readme.md o nome do código fonte e os números das linhas onde se encontra a implementação.
R: Arquivo ClienteServiceTests no caminho Testes > Services 

7 - Refatorações evidentes
Trechos de código que passaram por refatoração, como extração de métodos (refatoração “extrair método”) para remover duplicação e melhorar legibilidade. Mostre o antes (trecho comentado) e o depois logo em seguida. Deve ser indicado no Readme.md o nome do código fonte e os números das linhas onde se encontra a implementação.
Foi aplicada a refatoração "extrair método" para remover duplicação de código e melhorar a legibilidade nos métodos AtivarCliente e InativarCliente.

Arquivo: ClienteService.cs  
Local: NexusGym.Application.Services  
Linhas afetadas: 72 a 90

Antes: Cada método buscava o cliente e fazia a validação de existência individualmente, duplicando código.

Depois: Foi criado o método privado ObterClienteOuExcecao(int id) para centralizar a busca e validação, reduzindo duplicação e aumentando a clareza.

Motivo: A extração de método melhora a legibilidade, a reutilização e facilita a manutenção do código.

8 - Tratamento de erros e exceções
Uso consistente de blocos try/catch e telas de erro padronizadas, evidenciando a preocupação com confiabilidade e segurança. Deve ser indicado no Readme.md o nome do código fonte e os números das linhas onde se encontra a implementação.

R: O tratamento de erros é realizado pelo lançamento consistente de exceções customizadas para sinalizar condições de erro específicas, como cliente não encontrado ou CPF duplicado. Foi utilizado um arquivo.resx para salvar as mensagens, evitando criar hard code

Arquivo: ClienteService.cs  
Local: NexusGym.Application.Services  
Linhas principais: 43, 60, 75

Motivo: As exceções são lançadas na camada de serviço e devem ser capturadas em um middleware global ou na camada de API, garantindo resposta padronizada e melhor controle da confiabilidade e segurança da aplicação.

Observação: Não são usados blocos try/catch no serviço para não mascarar erros, seguindo boas práticas.


9 - Exemplos de validação de entrada
Métodos ou filtros que checam parâmetros e garantem que dados inválidos não sejam processados, ou parâmetros adicionados a queries, evitando vulnerabilidades como SQL Injection ou XSS. Deve ser indicado no Readme.md o nome do código fonte e os números das linhas onde se encontra a implementação.

R: A validação de dados de entrada é feita utilizando FluentValidation para garantir que campos obrigatórios estejam preenchidos corretamente antes da execução da lógica de negócio.

Arquivo: ClienteValidator.cs  
Local: NexusGym.Application.Validators  
Linhas principais: No arquivo todo temos as validações 

Motivo: Essa validação evita que dados inválidos ou incompletos sejam processados, prevenindo vulnerabilidades como SQL Injection e mantendo a integridade dos dados.

# Correcao

| Item | Situacao | Pontos |
|------|----------|--------|
| 1. Organizacao em camadas | Aprovado | 1 |
| 2. Aplicacao de padroes de projeto | Aprovado | 1 |
| 3. Principios SOLID em pratica | Aprovado | 1 |
| 4. Convencoes de nomenclatura claras | Aprovado | 1 |
| 5. Documentacao minima de codigo | Aprovado | 1 |
| 6. Testes automatizados | Aprovado | 1 |
| 7. Refatoracoes evidentes | Aprovado | 1 |
| 8. Tratamento de erros e excecoes | Aprovado | 1 |
| 9. Exemplos de validacao de entrada | Aprovado | 1 |
| 10. Heuristicas de usabilidade no frontend | Aprovado | 1 |

**Total 10 / 10**
