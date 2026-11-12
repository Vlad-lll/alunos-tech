# Alunos Tech: plataforma corporativa integrada (PIM VIII)

Código da parte técnica do PIM VIII (ADS, UNIP). A solução é uma API em C# com .NET 10, organizada em camadas, com banco SQL Server, Docker, Kubernetes e pipeline de CI.

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

## Pré-requisitos

.NET 10 SDK, Docker Desktop (com Kubernetes ativado, para a Figura 3) e VS Code com a extensão SQL Server (mssql), ou SSMS.

## Rodar e testar

```powershell
dotnet build
dotnet test
dotnet run --project src/AlunosTech.Api
```

Com a API no ar, abra `http://localhost:5000/swagger` e `http://localhost:5000/health`.

## Banco de dados local com Docker

```powershell
docker compose up -d sqlserver
```

Espere uns 30 segundos e crie o banco (a senha é só de desenvolvimento):

```powershell
foreach ($f in Get-ChildItem database/0[1-5]_*.sql) {
  docker compose exec sqlserver /opt/mssql-tools18/bin/sqlcmd -S localhost -U sa -P "Alunos@Tech123" -C -i /scripts/$($f.Name)
}
```

Para consultar, conecte no servidor `localhost,1433` com o usuário `sa` e use o `database/06_demonstracao.sql`.

## Kubernetes local

```powershell
docker build -t alunostech/api:latest .
kubectl apply -f k8s/deployment.yaml -f k8s/service.yaml
kubectl get pods
```

## Prints do PIM (parte técnica)

| Figura | O que mostrar | Como gerar |
| --- | --- | --- |
| 1 | CI verde | Aba Actions do GitHub |
| 2 | Contêiner rodando (opcional) | `docker run -d -p 8080:8080 alunostech/api:latest` e `docker ps` |
| 3 | Pods da API em Running | `kubectl get pods` |
| 4 | Estrutura de pastas (opcional) | VS Code com `src` e `tests` abertos |
| 5 | Testes aprovados | `dotnet test` |
| 6 | API no ar | `/swagger` no navegador |
| 7 | Código enviado ao GitHub (opcional) | `git push` |
| 8 | Diagrama do banco (opcional) | DER das tabelas de `database/01_schema.sql` |
| 9 | Procedure em execução | `EXEC sp_GerarFaturasMensais` e `SELECT * FROM Fatura` |
| 10 | Trigger de auditoria | `UPDATE` de status e `SELECT * FROM AuditoriaFatura` |
| 11 | Plano de execução (opcional) | Plano real da consulta de faturas abertas |

Os essenciais são as Figuras 1, 3, 5, 6, 9 e 10.

## O que ainda não está neste repositório

O PIM descreve como proposta, sem implementação aqui:

- infraestrutura real no Azure (AKS, Application Gateway, Key Vault). Os manifestos em `k8s/` funcionam em cluster local;
- monitoramento com Prometheus e Grafana;
- portal web, aplicativo desktop em WPF e Worker Service.

## Como colaborar

1. Atualize a `main`: `git pull`.
2. Crie uma branch para o que for fazer: `git checkout -b feature/nome-da-tarefa`.
3. Faça commits pequenos e com mensagens claras.
4. Abra um Pull Request no GitHub e peça revisão de outra pessoa do grupo. O CI precisa passar antes da mesclagem.
