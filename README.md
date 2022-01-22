# OAT-Stratis_Smart_Contract

Hi Welcome to OAT Smart Contract!!!!!!!!!!!!!!!!!
Let me be your guide for today. Let us enter the world of OAT.
Clone this Repository 
Clone this repository or download this repository code.
Now open this project in Visual Studio 2019. You can go through the code, i have also provided comments which can help you to clearly understand this smart contract.
Now open the Stratis.FullNode.sln in Visual studio 2019 and go to solution explore right click on  Stratis.CirrusMinerD and click open developer terminal.
Run “dotnet run -devmode=miner” this command.
This will  run the blockchain on a local machine for development.
Now we need to generate the by bytecode of the smart contract. Let us generate that now.
Now open Stratis.SmartContracts.Tools.Sct.sln in visual studio 2019.
Now open developer terminal and run this command “dotnet run -- validate [Contract_PATH]” here you need to provide the contract path that we have downloaded from the github.
With this command you are going to validate the smart contract.
Now once validation is successful we can generate the bytecode of the smart contract. 
Now run this command “dotnet run -- validate [CONTRACT_PATH_HERE] -sb” with this you can generate a bytecode and we will use this bytecode going forward.
 If you can see in step 4 after running that command “http://localhost:38223/swagger/v1/swagger.json” files are generated in the web browser.
Now download this file and open it in the Postman application.
 Let us now generate the wallet address which we can use in the application.
Call this api in POSTMAN “/api/Wallet/addresses?WalletName=cirrusdev&AccountName=account 0&Segwit=true” here we can see different wallet addresses are generated.
Let us now generate the TransactionID with which we can generate the contract address.
Run this api “/api/SmartContractWallet/create” and add the bytecode that we have generated in step 11 add that in Contract code. Now run this you will generate the transaction ID.
Now take this Transaction Hash and now paste this as an argument to “}/api/SmartContracts/receipt?txHash={txHash}” api. Here we will get a new contract address Save this contract Address we will be using this in our front-end reactjs code.
 This the Url of OAT front-end code repository. Please download this repo (https://github.com/SyedImam1998/OAT-Front-end-Website)
