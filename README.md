
# B3 Test
# Intenção
Este projeto é uma aplicação que executa a simulação do rendimento do CDB de um investimento inicial em relação a x tempos em meses.

## Inicialização do Projeto
### Docker
Na raiz do projeto, assim que feito o pull request, abra o terminal e execute o comando abaixo.   
```bash
docker-compose up -d
```
![PowerShell](start-all-docker.png)

Obs:Em toda reinicialização do docker, sempre e antes de nova inicialização **exclua o diretório DockerAppFiles** criado na raiz do projeto. 

#### Acesso
Por fim, acesso o swagger das aplicações, nos endereços:
* [Simulador de Investimento CDB](http://localhost:4200/)

### Inicialização Local
Abra a Solution do projeto Visual Studio, em seguida clique com botão direito sobre o projeto **B3.Test-FrontrEnd** navegue no menu até Open In Terminal (Abrir no Terminal). Irá inicializar o PowerShell do Desenvolvedor no Visual Studio. 
![PowerShell](init-frontend.png)

Digite o comando abaixo:
```bash
ng serve
```
Com isso a aplicação em Angular irá iniciar na porta 4200. Pronto o FrontEnd está inicializado, falta a WebApi.
Para a WebApi set o **B3.Test-WebApi** como Startup, inicie o projeto como Dock ou http. Agora sim tudo finalizado.
![WebApi](webapi-init-http.png)
![WebApi](webapi-init-dock.png)

#### Acesso
Por fim, acesso o swagger das aplicações, nos endereços:
* [Simulador de Investimento CDB](http://localhost:4200/)
* [Swagger UI](http://localhost:32805/swagger/index.html)

## Banco de Dados
* ElasticSearch

## Observabilidade
A observabilidade em aplicações refere-se à capacidade de compreender e monitorar efetivamente o comportamento interno e o desempenho de um sistema em tempo real. Para tal adaptei o código para atender a esses conceitos e utilizei de algumas ferramentas, sendo:
* Kibana
    * Visualização dos logs
    * Ao executar pela primeira vez, por mais que existam dados de log, a visualização destes só será possível por meio de index. Então ao ir em discover do Kibana, haverá um redirecionamento para a tela de criação.  O nome do index é **b3_test_index**, crie e assim a visualização do logs acontecerá.
    * [Home - Elastic](http://localhost:5601/app/home#/)
* Telemetria - Jaeger
    * [Jaeger UI](http://localhost:16686/search)

## Premissas
Seguindo os princípios do bom desenvolvimento de software, construí este projeto com ênfase na arquitetura da solução e da aplicação. A análise do código revela uma separação em camadas bem definida, onde a camada de domínio, responsável pelas regras de negócio, é acessada exclusivamente por meio de interfaces bem definidas, chamadas de portas e o componente que apoia a lógica de negócio se adapta a essas portas. Essa abordagem, conhecida como **arquitetura hexagonal**, promove a independência da lógica de negócio em relação às tecnologias e frameworks utilizados, facilitando testes, manutenabilidade e reuso de código. O projeto também **incorpora os princípios SOLID**, um conjunto de boas práticas que garantem a coesão e baixo acoplamento. Essa combinação resulta em um código mais robusto, flexível e fácil de entender. 
Para garantir a qualidade do código segui **conceitos do Clen Code** e **realizei testes unitários** com alta cobertura, como visto na imagem abaixo. Além disso, um **teste de integração** somente nas ACLs para validar as chamadas ao BC e Ipea para recuperação do CDI oficial.
![PowerShell](img/test-coverage.png)


## Considerações
Para apresentar esse projeto, tive pouco tempo e, por isso, não organizei algumas coisas que considero necessárias. Por exemplo, cada serviço deveria estar em uma solution separada. Além disso, as partes comuns deveriam estar em um gerenciador de pacotes interno da empresa. Nesse caso, seria interessante subir uma **imagem do NuGet no Docker** para essa finalidade. Infelizmente, do meu ponto de vista, a organização não atingiu um nível satisfatório.  


## Conteinerização
A aplicação foi projetada com orquestração de containers em mente, facilitando a implantação e o gerenciamento em ambientes escaláveis. A viabilidade de orquestração foi validada por meio de testes com o Docker Compose, demonstrando potencial para utilização em produção com ferramentas como o Kubernetes.