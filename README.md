# SuriWebhook

Desafio Desenvolvedor [Suri](https://www.chatbotmaker.io/). O desafio consiste em realizar uma integração entre o chatbot e um serviço fictício, simulando
uma situação comum em empresas que utilizam a Suri em seus atendimentos.

A aplicação foi deployada em uma [Máquina Virtual Azure](https://azure.microsoft.com/pt-br/products/virtual-machines/) e tem como objetivo principal, seguir o fluxo abaixo.


![image](https://github.com/AlexsandroLBS/SuriWebhook/assets/89843505/c24dc075-5d95-4ff7-bc07-fe79fbc4086b)


## Testes
Para finalidade de testes, foi adicionado apenas um dado ao banco, com CPF igual à 64315629090, valor este gerado aleatoriamente por um [Gerador de CPF](https://www.4devs.com.br/gerador_de_cpf).

Para executar a aplicação localmente via docker, vá para a raiz do projeto e utlize o comando [Docker Compose](https://docs.docker.com/compose/)
```
cd SuriWebhook
docker-compose up 
```
E caso queira puxar informações no banco da VM, as credenciais são as mesmas, com exceção do host, que será enviado por email.
## Estrutura
Para o desenvolvimento não foi adotado nenhum padrão de projeto específico, visto que o serviço tem apenas uma única função.

O container possui um banco de dados [Postgres](https://www.postgresql.org/) e um API simples em [.NET](https://dotnet.microsoft.com/pt-br/)

## Tecnologias utlizadas

- .NET 7.0
- Postgres
- Docker
- AzureVM
