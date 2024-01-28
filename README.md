Projeto APIFinanceiro, API desenvolvida utilizando .Net, banco relacional SQL, autenticação JWT e testes usando lib XUnit com o intuito de um usuário realizar aplicações, resgates e consultar seus investimento.

Desenho relacional utilizando conceitos de DDD:
![image](https://github.com/GabrielaPSilva/ApiFinanceiro/assets/18128212/ee58e9e0-6043-4ae3-b733-764ae76d66fa)

![image](https://github.com/GabrielaPSilva/ApiFinanceiro/assets/18128212/ddc75a0b-03eb-470b-b24a-2541481a16ad)

![image](https://github.com/GabrielaPSilva/ApiFinanceiro/assets/18128212/96f4cfcf-80b3-49f7-ad67-087331e3dea7)


Modelagem relacional Banco de Dados SQL:
![image](https://github.com/GabrielaPSilva/ApiFinanceiro/assets/18128212/c364180c-b76c-4062-b2f3-4c4fa9d5189f)



Critérios de aceite para a APIFinanceiro

Usuário
--------

Cadastrar usuário
O usuário deve ser capaz de se cadastrar no sistema

Editar Usuário
O usuário deve ser capaz de se atualizar no sistema

Excluir Usuário
O usuário deve ser capaz de excluir seu registro no sistema

Os dados a serem manipulados devem ser os dados pessoais do usuário, como nome, e-mail, telefone, etc.

Listar Usuário
o usuário deve ser localizado pelo CPF.
O administrador deve ser capaz de listar todos os usuários cadastrados no sistema.


Segmentos
----------

Listar Segmentos
O administrador deve ser capaz de listar todos os segmentos cadastrados no sistema.

Cadastrar Segmentos
O administrador deve ser capaz de cadastrar novos segmentos no sistema.

Editar Segmentos
O administrador deve ser capaz de editar os dados de um segmento existente.

Excluir Segmento
O administrador deve ser capaz de excluir o segmento do sistema.

Consulta de segmento para investir
O usuário deve ser capaz de consultar os segmentos disponíveis para investimento.
Os segmentos devem ser listado por tipo de investimento, como renda fixa, renda variável, etc.


Graus de Risco do Investidor
-----------------------------

Listar Graus de riscos
O administrador e/ou usuário deve ser capaz de listar todos os graus de risco cadastrados no sistema.

Cadastrar Graus de riscos
O administrador deve ser capaz de cadastrar novos graus de risco no sistema.

Editar Graus de riscos
O administrador deve ser capaz de editar os dados de um grau de risco existente.

Excluir Graus de riscos
O administrador deve ser capaz de excluir um grau de risco existente.


Investimento
-------------

Realiza uma aplicação
O usuário deve ser capaz de realizar uma aplicação em um segmento de investimento.
A aplicação deve ser realizada com um tempo de expiração, taxa de rendimento e taxa de administração.
O usuário deve ser capaz de consultar uma lista de todas as suas aplicações, com seus respectivos saldos.

Solicita Resgate
O usuário deve ser capaz de solicitar o resgate de uma aplicação.
O resgate deve ser realizado com um valor mínimo e um valor máximo definido pelo sistema.
Administrador

Consulta de Investimentos
O usuário deve ser capaz de consultar os seus investimentos existentes.
A consulta deve ser realizada pelo seu CPF.


Cenários de testes XUnit do projeto

Usuário
-------

Cadastro de usuário: O usuário deve ser capaz de se cadastrar no sistema, fornecendo os dados pessoais necessários.
Validar os dados de entrada, como nome, e-mail e telefone, para garantir que sejam válidos.
Verificar se o usuário foi criado com sucesso.
Atualização de usuário: O usuário deve ser capaz de atualizar seus dados pessoais no sistema.
Validar os dados de entrada, como nome, e-mail e telefone, para garantir que sejam válidos.
Verificar se os dados foram atualizados com sucesso.
Exclusão de usuário: O usuário deve ser capaz de excluir seu registro no sistema.
Verificar se o usuário foi excluído com sucesso.

Segmentos
---------

Listagem de segmentos: O administrador deve ser capaz de listar todos os segmentos cadastrados no sistema.
Verificar se todos os segmentos são listados corretamente.
Cadastro de segmento: O administrador deve ser capaz de cadastrar novos segmentos no sistema.
Validar os dados de entrada, como nome e tipo, para garantir que sejam válidos.
Verificar se o segmento foi cadastrado com sucesso.
Edição de segmento: O administrador deve ser capaz de editar os dados de um segmento existente.
Validar os dados de entrada, como nome e tipo, para garantir que sejam válidos.
Verificar se os dados foram editados com sucesso.
Exclusão de segmento: O administrador deve ser capaz de excluir um segmento do sistema.
Verificar se o segmento foi excluído com sucesso.

Graus de risco do investidor
----------------------------

Listagem de graus de risco: O administrador e/ou usuário deve ser capaz de listar todos os graus de risco cadastrados no sistema.
Verificar se todos os graus de risco são listados corretamente.
Cadastro de grau de risco: O administrador deve ser capaz de cadastrar novos graus de risco no sistema.
Validar os dados de entrada, como nome e descrição, para garantir que sejam válidos.
Verificar se o grau de risco foi cadastrado com sucesso.
Edição de grau de risco: O administrador deve ser capaz de editar os dados de um grau de risco existente.
Validar os dados de entrada, como nome e descrição, para garantir que sejam válidos.
Verificar se os dados foram editados com sucesso.
Exclusão de grau de risco: O administrador deve ser capaz de excluir um grau de risco do sistema.
Verificar se o grau de risco foi excluído com sucesso.

Investimento
------------

Realização de aplicação: O usuário deve ser capaz de realizar uma aplicação em um segmento de investimento.
Validar os dados de entrada, como valor da aplicação, tempo de expiração e taxa de rendimento, para garantir que sejam válidos.
Verificar se a aplicação foi realizada com sucesso.
Consulta de investimentos: O usuário deve ser capaz de consultar uma lista de todas as suas aplicações, com seus respectivos saldos.
Verificar se todas as aplicações são listadas corretamente, com seus respectivos saldos.
Solicitação de resgate: O usuário deve ser capaz de solicitar o resgate de uma aplicação.
Validar os dados de entrada, como valor do resgate, para garantir que sejam válidos.
Verificar se o resgate foi solicitado com sucesso.
