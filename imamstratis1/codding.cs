using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace imamstratis1
{
    class codding
    {
        Hashtable AllOwners = new Hashtable();

        public struct Owner
        {
            public string id;
            public string name;
            public string address;
            public ArrayList tents;
            public int rating;


        }
        public static void main()
        {
            codding hw = new codding();
            Boolean value = hw.registerOwner("123", "imam", "vija", 5);
            Console.WriteLine(value);
            hw.GiveHouse("123", "234");
            hw.GiveHouse("123", "456");

        }
        public Boolean registerOwner(string id, string name, string address, int rating)
        {
            Owner own = new Owner();
            own.id = id;
            own.name = name;
            own.address = address;
            own.rating = rating;
            AllOwners.Add(id, own);
            return true;
        }
        public void GiveHouse(string OwnerId, string tenantsId)
        {
            if (AllOwners.ContainsKey(OwnerId))
            {

                Owner own = (Owner)AllOwners[OwnerId];
                foreach(var k in own.tents)
                {
                    Console.WriteLine(k);
                }
    
            }

        }
    }
}
