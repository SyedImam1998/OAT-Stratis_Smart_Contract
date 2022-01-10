using System;
using System.Collections.Generic;
using System.Text;

using Stratis.SmartContracts;
using System.Collections;

public class tenants : SmartContract
{
    


    public tenants(ISmartContractState smartContractState)
    : base(smartContractState)
    {

       


        

    }

   
    public void Givehouse(Address Oadd,Address tadd)
    {
        var tentD = getTenants(tadd);
        var ownerD = getOwner(Oadd);
        String[] counter = ownerD.tenants;
        var counterw = counter.Length;
    }
    
    

}