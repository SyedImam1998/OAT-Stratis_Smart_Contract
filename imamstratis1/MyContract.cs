using Stratis.SmartContracts;
using System;
using System.Collections;

public class MyContract : SmartContract
{
    

    public struct Owner
    {
        public string name;
        public Address Addr;
        public string password;
        public string address;
        public uint rating;
        public string pnumber;
        

    }

    public string setOwner(string name,string password,string address,UInt32 rating, string pnumber, Address wAddress){

        Owner o1 = new Owner();
        o1 = getOwner(wAddress);
        if (o1.name == null)
        {
            Owner own = new Owner();
            own.Addr = wAddress;
            own.address = address;
            own.name = name;
            own.pnumber = pnumber;
            own.rating = rating;
            own.password = password;

            PersistentState.SetStruct($"owner:{wAddress}", own);
           
        }
        else
        {
            return "Owner Already Exists"; 
        }
        return "okay";


    }
    public Owner getOwner(Address wAddress)
    {
        return PersistentState.GetStruct<Owner>($"owner:{wAddress}");
    }
    public uint getIndexOfTenantsInThisOwner(Address add)
    {
        return PersistentState.GetUInt32($"{add}");
    }
    public void setIndexOfTenantsInThisOwner(Address add, uint index)
    {
        index = index + 1;
        PersistentState.SetUInt32($"{add}",index);
    }


    public void setTenantsListForOwner(Address ownerAdd,Address tenantAdd)
    {
        uint index = getIndexOfTenantsInThisOwner(ownerAdd);
        setIndexOfTenantsInThisOwner(ownerAdd, index);
        PersistentState.SetAddress($"{ownerAdd}:{index+1}", tenantAdd);
    }

    public Address getTenantsListForOwner(Address ownerAdd,uint index)
    {
        return PersistentState.GetAddress($"{ownerAdd}:{index}");
    }
   /// <summary>
   /// ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
   /// </summary>

    public struct tenant
    {
        public Address add;
        public string name;
        public uint rating;
        public string pnumber;
      
    }


    public string setTenant(string name,UInt32 rating, string phoneNo,Address add)
    {
        tenant t1 = new tenant();
        t1 = getTenants(add);
        if (t1.name == null)
        {
            tenant ten = new tenant();
            ten.add = add;
            ten.name = name;
            ten.pnumber = phoneNo;
            ten.rating = rating;

            PersistentState.SetStruct($"tenant:{add}", ten);
        }
        else
        {
            return "Tenant Already Exists";
        }

        return "okay"; 
    }
    public tenant getTenants(Address add)
    {
        return PersistentState.GetStruct<tenant>($"tenant:{add}");
    }
    public uint getIndexOfOwnersInThisTenant(Address add)
    {
        return PersistentState.GetUInt32($"{add}");
    }
    public void setIndexOfOwnersInThisTenant(Address add, uint index)
    {
        index = index + 1;
        PersistentState.SetUInt32($"{add}", index);
    }


    public void setOwnersListForTenant(Address ownerAdd, Address tenantAdd)
    {
        uint index = getIndexOfOwnersInThisTenant(tenantAdd);
        setIndexOfOwnersInThisTenant(tenantAdd, index);
        PersistentState.SetAddress($"{tenantAdd}:{index + 1}", ownerAdd);
    }

    public Address getOwnersListForTenant(Address tenantAdd, uint index)
    {
        return PersistentState.GetAddress($"{tenantAdd}:{index}");
    }




    public string name
    {
        get => PersistentState.GetString(nameof(name));
        private set
        {
            PersistentState.SetString("name", value);

        }
    }
    public MyContract(ISmartContractState smartContractState)
    : base(smartContractState)
    {

        setTenant("syedImam",5,"9908909245", Message.Sender);

    }
    /// <summary>
    /// //////////////////////////////////////////////////////////////////////////////////////////////////////////
 
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    public string Givehouse(Address Oadd,string password, Address tadd)
    {
        Owner o = new Owner();
        o = getOwner(Oadd);
       
            if (o.password == password)
            {
                setOwnersListForTenant(Oadd, tadd);
                setTenantsListForOwner(Oadd, tadd);
            }
            else
            {
                String msg="Owner Password Incorrect.";
                return msg;
            }


        return "All okay";

        

    }

    public Address[] getTenantsDetails(Address ownerAdd)///to get the past tenants details of an owner
    {
        uint TotalIndex = getIndexOfTenantsInThisOwner(ownerAdd);
        Address[] tenantsList = new Address[TotalIndex];
        if (TotalIndex > 0)
        {
            for (uint i = 0; i < TotalIndex; i++)
            {
                tenantsList[i] = getTenantsListForOwner(ownerAdd, i+1);
            }
        }
        
        return tenantsList;

    }
    public Address[] getOwnerDetails(Address tenantAdd)
    {
        uint TotalIndex = getIndexOfOwnersInThisTenant(tenantAdd);
        Address[] ownerList = new Address[TotalIndex];
        if (TotalIndex > 0)
        {
            for (uint i = 0; i < TotalIndex; i++)
            {
                ownerList[i] = getOwnersListForTenant(tenantAdd, i + 1);
            }
        }

        return ownerList;
        // return TotalIndex;
    }


}
