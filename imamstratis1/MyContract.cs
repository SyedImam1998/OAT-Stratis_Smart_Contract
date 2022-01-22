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
    public string setOwnerForFrontend(string name, string password, string address, UInt32 rating, string pnumber, Address wAddress)/// /to set the owner from website...
    {
        return setOwner(name, password, address, rating, pnumber, wAddress, "1");
    }
    private string setOwner(string name,string password,string address,UInt32 rating, string pnumber, Address wAddress,string flag){/// this method is used to set the owner details

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
    private Owner getOwner(Address wAddress)/// this method gives all details of an Owner
    {
        return PersistentState.GetStruct<Owner>($"owner:{wAddress}");
    }
    public Owner getSecureOwnerDetails(Address add)/////gives out secure details of owner to website(front-end) by excluding password data.
    {
        Owner o1, o2 = new Owner();
        o1 = getOwner(add);
        o2.name = o1.name;
        o2.pnumber = o1.pnumber;
        o2.rating = o1.rating;
        return o2;
    }
    private uint getIndexOfTenantsForThisOwner(Address add)// this method returns number of tenants rented a particular owner house
    {
        return PersistentState.GetUInt32($"{add}");
    }
    private void setIndexOfTenantsInThisOwner(Address add, uint index)// used to set index of tenants rented a particular owner house
    {
        index = index + 1;
        PersistentState.SetUInt32($"{add}",index);
    }


    private void setTenantsListForOwner(Address ownerAdd,Address tenantAdd)// this method is used to store tenant address with owner address and with index.
    {
        uint index = getIndexOfTenantsForThisOwner(ownerAdd);
        setIndexOfTenantsInThisOwner(ownerAdd, index);
        PersistentState.SetAddress($"{ownerAdd}:{index+1}", tenantAdd);
    }

    private Address getTenantsListForOwner(Address ownerAdd,uint index)// this method will give out tenants address with the keys ie owner address and index.
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
        public string password;
        public uint rating;
        public string pnumber;
      
    }

    public string setTenantFromFront_end(string name, UInt32 rating, string phoneNo, Address add, string password)// to set the tenant from website

    {
        return setTenant(name, rating, phoneNo, add, "1", password);

    }
    private string setTenant(string name,UInt32 rating, string phoneNo,Address add,string flag,string password)/// used to set tenant details.
    {
        tenant t1 = new tenant();
        t1 = getTenants(add);
        if (t1.name == null|| flag=="2")
        {
            tenant ten = new tenant();
            ten.add = add;
            ten.name = name;
            ten.pnumber = phoneNo;
            ten.password = password;
            ten.rating = rating;

            PersistentState.SetStruct($"tenant:{add}", ten);
        }
        else
        {
            return "Tenant Already Exists";
        }

        return "okay"; 
    }
    private tenant getTenants(Address add)// this method is used to get all  details of a particular tenant.
    {
        return PersistentState.GetStruct<tenant>($"tenant:{add}");
    }

    public tenant getSecureTenantDetails(Address add)/////this method will provide details of a tenant excluding the password for frontend purpose.
    {
        tenant t1, t2 = new tenant();
        t1 = getTenants(add);
        t2.name = t1.name;
        t2.pnumber = t1.pnumber;
        t2.rating = t1.rating;
        return t2;
    }
    private uint getIndexOfOwnersForThisTenant(Address add)// this method will provide number of owners have rented their house to this tenant.
    {
        return PersistentState.GetUInt32($"{add}");
    }
    private void setIndexOfOwnersInThisTenant(Address add, uint index)/// this method will set index of onwers for a particluar tenant.
    {
        index = index + 1;
        PersistentState.SetUInt32($"{add}", index);
    }


    private void setOwnersListForTenant(Address ownerAdd, Address tenantAdd)// this method will save owner address for a particular tenant with index.
    {
        uint index = getIndexOfOwnersForThisTenant(tenantAdd);
        setIndexOfOwnersInThisTenant(tenantAdd, index);
        PersistentState.SetAddress($"{tenantAdd}:{index + 1}", ownerAdd);
    }

    private Address getOwnersListForTenant(Address tenantAdd, uint index)//this method will return owner address with keys ie tenant address and index.
    {
        return PersistentState.GetAddress($"{tenantAdd}:{index}");
    }


    public MyContract(ISmartContractState smartContractState)
    : base(smartContractState)
    {

        

    }
   
    public string Givehouse(Address Oadd,string password, Address tadd)// this method will will verify the owner passowrd and then it will add tenants address to owners tenant list and will add owner address to tenants owners list.
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

    public Address[] getTenantsDetails(Address ownerAdd)//this method will give array of tenant addrress of a particular owner address.
    {
        uint TotalIndex = getIndexOfTenantsForThisOwner(ownerAdd);
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
    public Address[] getOwnerDetails(Address tenantAdd)// this method will give array of owner addresses of particular tenant.
    {
        uint TotalIndex = getIndexOfOwnersForThisTenant(tenantAdd);
        Address[] ownerList = new Address[TotalIndex];
        if (TotalIndex > 0)
        {
            for (uint i = 0; i < TotalIndex; i++)
            {
                ownerList[i] = getOwnersListForTenant(tenantAdd, i + 1);
            }
        }

        return ownerList;
    }

  

    private uint getRatingForOwner(Address Oadd, Address Tadd)// this method is used for getting the rating of tenant that he gave to a owner
    {
        return PersistentState.GetUInt32($"{Oadd}:{Tadd}");
    }

    public string setRatingForOwner(Address Oadd,Address Tadd, string Tpassword, uint rating)// this method is used to set the rating of tenant to owner
    {
        tenant t = new tenant();
        t = getTenants(Tadd);
        if (t.password == Tpassword)
        {
            Address[] tenantslist = getTenantsDetails(Oadd);
            bool flag = false;
            Owner o1 = new Owner();
            o1 = getOwner(Oadd);
            for (uint i = 0; i < tenantslist.Length; i++)
            {
                if (tenantslist[i] == Tadd)
                {

                    PersistentState.SetUInt32($"{Oadd}:{Tadd}", rating);
                    flag = true;
                    uint ratingAddOwner = getTotalRatingOfOwner(Oadd);
                    setOwner(o1.name, o1.password, o1.address, ratingAddOwner, o1.pnumber, o1.Addr, "2");
                    return "rating saved";
                }

            }
            if (flag == false)
            {
                return "Please check the Owner Wallet Address.You have never rented this owner house.";
            }

        }
        else
        {
            return "Tenant Password Incorrect.";
        }
        
        return "rating saved";
    }

    private uint getTotalRatingOfOwner(Address Oadd)// this method will calculate all the previous ratings of the a particular owner and return the value
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
    
    private uint getRatingForTenant(Address Tadd, Address Oadd)// this method is used for getting the rating of owner that he gave to a tenant
    {
        return PersistentState.GetUInt32($"T{Tadd}:{Oadd}");
    }

    public string setRatingForTenant(Address Oadd, Address Tadd, string Opassword,uint rating)// this method is used to set the rating of owner to tenant

    {
        Owner oo = new Owner();
        oo = getOwner(Oadd);
        if(oo.password== Opassword)
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
                    setTenant(t1.name, ratingAddTenant, t1.pnumber, t1.add, "2", t1.password);
                    return "rating saved";
                }

            }
            if (flag == false)
            {
                return "Please check the Tenant Wallet Address.You have never given house to this tenant.";
            }
        }
        else
        {
            return "Owner Password Incorrect.";
        }
        
        return "rating saved";
    }

    private uint getTotalRatingOfTenant(Address Tadd)// this method will calculate all the previous ratings of the a particular tenant and return the value
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
