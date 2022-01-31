# OAT-Stratis_Smart_Contract

Hi Welcome to OAT Smart Contract!!!!!!!!!!!!!!!!!
Let me be your guide for today. Let us enter the world of OAT.
 
1. Clone this repository or download this repository code.
2. Now open this project in Visual Studio 2019. You can go through the code, i have also provided comments which can help you to clearly understand this smart contract.
3. Now open the Stratis.FullNode.sln in Visual studio 2019 and go to solution explore right click on  Stratis.CirrusMinerD and click open developer terminal.
4. Run “dotnet run -devmode=miner” this command.
5. This will  run the blockchain on a local machine for development.
6. Now we need to generate the by bytecode of the smart contract. Let us generate that now.
7. Now open Stratis.SmartContracts.Tools.Sct.sln in visual studio 2019.
8. Now open developer terminal and run this command “dotnet run -- validate [Contract_PATH]” here you need to provide the contract path that we have downloaded from the github.
9. With this command you are going to validate the smart contract.
10. Now once validation is successful we can generate the bytecode of the smart contract. 
11. Now run this command “dotnet run -- validate [CONTRACT_PATH_HERE] -sb” with this you can generate a bytecode and we will use this bytecode going forward.
 12. If you can see in step 4 after running that command “http://localhost:38223/swagger/v1/swagger.json” files are generated in the web browser.
13. Now download this file and open it in the Postman application.
 14. Let us now generate the wallet address which we can use in the application.
15. Call this api in POSTMAN “/api/Wallet/addresses?WalletName=cirrusdev&AccountName=account 0&Segwit=true” here we can see different wallet addresses are generated. Pick 4-5 wallet address because we are going to usse them going forward.
16. Let us now generate the TransactionID with which we can generate the contract address.
17. Run this api “/api/SmartContractWallet/create” and add the bytecode that we have generated in step 11 add that in Contract code. Now run this you will generate the transaction ID.
18. Now take this Transaction Hash and now paste this as an argument to “}/api/SmartContracts/receipt?txHash={txHash}” api. Here we will get a new contract address Save this contract Address we will be using this in our front-end reactjs code.
 19. This the Url of OAT front-end code repository. Please download this repo (https://github.com/SyedImam1998/OAT-Front-end-Website)
