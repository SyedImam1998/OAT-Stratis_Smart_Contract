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

    public string setOwner(string name,string password,string address,UInt32 rating, string pnumber, Address wAddress,string flag){

        Owner o1 = new Owner();
        o1 = getOwner(wAddress);
        if (o1.name == null|| flag=="2")
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


    public string setTenant(string name,UInt32 rating, string phoneNo,Address add,string flag)
    {
        tenant t1 = new tenant();
        t1 = getTenants(add);
        if (t1.name == null|| flag=="2")
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

   /* public uint getRatingIndexOfOwner(Address Oadd)
    {
        return PersistentState.GetUInt32($"{Oadd}");
    }
    public void setRatingIndexOfOwner(Address Oadd, uint index)
    {
        index = index + 1;
        PersistentState.SetUInt32($"{Oadd}", index);
    }*/

    public uint getRatingForOwner(Address Oadd, Address Tadd)
    {
        return PersistentState.GetUInt32($"{Oadd}:{Tadd}");
    }

    public string setRatingForOwner(Address Oadd,Address Tadd,uint rating)
    {
        Address[] tenantslist=getTenantsDetails(Oadd);
        bool flag = false;
        Owner o1 = new Owner();
        o1 = getOwner(Oadd);
        for (uint i = 0; i < tenantslist.Length; i++)
        {
            if (tenantslist[i] == Tadd)
            {
                
                PersistentState.SetUInt32($"{Oadd}:{Tadd}",rating);
                flag = true;
                uint ratingAddOwner=getTotalRatingOfOwner(Oadd);
                setOwner(o1.name, o1.password, o1.address, ratingAddOwner, o1.pnumber, o1.Addr,"2");
                return "rating Data saved";
            }
            
        }
        if (flag == false)
        {
            return "rating not saved";
        }
        return "rating saved";
    }

    public uint getTotalRatingOfOwner(Address Oadd)
    {
        Address[] tenantlist = getTenantsDetails(Oadd);
        uint rating = 0;
        uint totalRaters = 0;
        uint totalRating = 0;
        for(uint i = 0; i < tenantlist.Length; i++)
        {
           rating=rating+ getRatingForOwner(Oadd, tenantlist[i]);
            if (getRatingForOwner(Oadd, tenantlist[i]) > 0)
            {
                totalRaters = totalRaters + 1;
            }
        }
        totalRating = rating / totalRaters;

        return totalRating;

    }
    /// <summary>
    /// ////////////////////////////////////////////////////////////////////////////////////////////////////////
    /// </summary>
    /// <param name="Tadd"></param>
    /// <param name="Oadd"></param>
    /// <returns></returns>
    public uint getRatingForTenant(Address Tadd, Address Oadd )
    {
        return PersistentState.GetUInt32($"T{Tadd}:{Oadd}");
    }

    public string setRatingForTenant(Address Oadd, Address Tadd, uint rating)
    {
        Address[] ownerlist = getOwnerDetails(Tadd);
        bool flag = false;
        tenant t1 = new tenant();
        t1 = getTenants(Tadd);
        for (uint i = 0; i < ownerlist.Length; i++)
        {
            if (ownerlist[i] == Oadd)
            {

                PersistentState.SetUInt32($"T{Tadd}:{Oadd}", rating);
                flag = true;
                uint ratingAddTenant = getTotalRatingOfTenant(Tadd);
                setTenant(t1.name,ratingAddTenant,t1.pnumber,t1.add, "2");
                return "rating Data saved";
            }

        }
        if (flag == false)
        {
            return "rating not saved";
        }
        return "rating saved";
    }

    public uint getTotalRatingOfTenant(Address Tadd)
    {
        Address[] ownerlist = getOwnerDetails(Tadd);
        uint rating = 0;
        uint totalRaters = 0;
        uint totalRating = 0;
        for (uint i = 0; i < ownerlist.Length; i++)
        {
            rating = rating + getRatingForTenant(Tadd,ownerlist[i]);
            if (getRatingForTenant(Tadd,ownerlist[i]) > 0)
            {
                totalRaters = totalRaters + 1;
            }
        }
        totalRating = rating / totalRaters;

        return totalRating;

    }



}
