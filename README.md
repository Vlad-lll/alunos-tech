# Alunos Tech: plataforma corporativa integrada (PIM VIII)

Código da parte técnica do PIM VIII (ADS, UNIP). A solução é uma API em C# com .NET 8, organizada em camadas, com banco SQL Server, Docker, Kubernetes e pipeline de CI.

## Estrutura

```
src/
  AlunosTech.Domain/          entidades, regras de negócio e interfaces
  AlunosTech.Application/     casos de uso (Factory de regras de cobrança, gerador de faturas)
  AlunosTech.Infrastructure/  Entity Framework Core, Repository, injeção de dependência
  AlunosTech.Api/             API ASP.NET Core (endpoints /health e /faturas/abertas)
tests/
  AlunosTech.Tests/           testes unitários e de integração (xUnit)
database/                     scripts SQL: schema, procedure, function, trigger, índices e dados de exemplo
k8s/                          manifestos Kubernetes (Deployment, Service, HPA)
pipelines/                    pipeline do Azure DevOps
.github/workflows/ci.yml      pipeline de CI no GitHub Actions
Dockerfile                    build em dois estágios
docker-compose.yml            API + SQL Server para uso local
```

## Como rodar e quais prints isso gera

Pré-requisitos: .NET 8 SDK, Docker Desktop e, para o Kubernetes, o Kubernetes do Docker Desktop (ou minikube/kind). Para o banco, SSMS ou Azure Data Studio.

| Figura | Comando ou ação |
| --- | --- |
| 1 (pipeline) | Subir o repositório no GitHub e abrir a aba Actions depois de um push |
| 2 (Docker) | `docker build -t alunostech/api:latest .` e depois `docker run -d -p 8080:8080 alunostech/api:latest` e `docker ps` |
| 3 (Kubernetes) | `kubectl apply -f k8s/deployment.yaml -f k8s/service.yaml` e depois `kubectl get pods` |
| 4 (Grafana) | Opcional. Não há configuração de Prometheus/Grafana neste repositório ainda |
| 5 e 6 (Kanban e backlog) | Trello, GitHub Projects ou Azure Boards |
| 7 (estrutura) | Abrir a pasta no VS Code e expandir `src` e `tests` |
| 8 (testes) | `dotnet test` |
| 9 (API) | `dotnet run --project src/AlunosTech.Api` e abrir `/swagger` ou `/health` |
| 10 (DER) | Gerar o diagrama a partir de `database/01_schema.sql` |
| 11 a 13 (banco) | Executar os scripts de `database/` na ordem e usar o `06_demonstracao.sql` |

### Banco de dados local com Docker

```bash
docker compose up -d sqlserver

# executa os scripts na ordem (a senha é só de desenvolvimento)
for f in database/0[1-5]_*.sql; do
  docker compose exec sqlserver /opt/mssql-tools18/bin/sqlcmd \
    -S localhost -U sa -P 'Alunos@Tech123' -C -i /scripts/$(basename $f)
done
```

Depois, `database/06_demonstracao.sql` pode ser aberto no SSMS ou no Azure Data Studio (servidor `localhost,1433`, usuário `sa`) para tirar os prints das Figuras 11, 12 e 13.

## O que ainda não está neste repositório

O texto do PIM cita itens que não foram implementados aqui. Ou o grupo implementa, ou o texto deve ser ajustado para não afirmar o que não existe:

- aplicativo desktop em WPF (só roda em Windows);
- Worker Service para o fechamento mensal;
- uso do Dapper nas consultas pesadas;
- configuração do Prometheus e do Grafana;
- infraestrutura real no Azure (AKS, Application Gateway, Key Vault). Os manifestos em `k8s/` funcionam em cluster local.

## Como colaborar

1. Atualize a `main`: `git pull`.
2. Crie uma branch para o que for fazer: `git checkout -b feature/nome-da-tarefa`.
3. Faça commits pequenos e com mensagens claras.
4. Abra um Pull Request no GitHub e peça revisão de outra pessoa do grupo. O CI precisa passar antes da mesclagem.
