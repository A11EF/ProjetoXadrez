# Projeto Xadrez

## Abrir no celular

O jogo funciona no navegador do celular enquanto o computador que o hospeda estiver ligado.

1. No computador, abra o terminal na pasta do projeto e execute:

   ```powershell
   dotnet run --project ProjetoXadrez\ProjetoXadrez.csproj --urls http://0.0.0.0:5080
   ```

2. Deixe o terminal aberto e conecte o celular à mesma rede Wi-Fi do computador.
3. No computador, execute `ipconfig` e localize o endereço IPv4 da conexão Wi-Fi.
4. No navegador do celular, acesse `http://ENDERECO-IP-DO-PC:5080`, substituindo pelo endereço encontrado.
5. Se o Windows pedir autorização no firewall, permita o acesso em redes privadas.

O servidor e as partidas ficam apenas no computador; uma partida é encerrada se o servidor for reiniciado. Não exponha esta porta diretamente à internet.
