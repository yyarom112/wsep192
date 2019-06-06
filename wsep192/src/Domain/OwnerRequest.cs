using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace src.Domain
{
    public class OwnerRequest
    {

        public string user { get; set; }
        public string owner { get; set; }
        public string store { get; set; }
        public string message { get; set; }
        public int id { get; set; }
        public int responsesCounter { get; set; }
        public bool result { get; set; }
        public int storeOwnersCount { get; set; }

        public OwnerRequest(string message , int id, string user, string store, string owner,int storeOwnersCount) {
            this.message = message;
            this.id = id;
            this.user = user;
            this.store = store;
            this.owner = owner;
            this.responsesCounter = 0;
            this.result = true;
            this.storeOwnersCount = storeOwnersCount;
        }
        

    }
}
