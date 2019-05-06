using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace src.Domain
{
    class PurchesPolicyData
    {
        private int type;
        private int id;
        private int productID;
        private int quntity;
        private int min;
        private int max;
        private int sumMin;
        private int sumMax;
        private LogicalConnections act;
        private String adress;
        private bool isregister;

        public PurchesPolicyData(int type, int id, int productID, int quntity, int min, int max, int sumMin, int sumMax, LogicalConnections act, string adress, bool isregister)
        {
            this.Type = type;
            this.Id = id;
            this.ProductID = productID;
            this.Quntity = quntity;
            this.Min = min;
            this.Max = max;
            this.SumMin = sumMin;
            this.SumMax = sumMax;
            this.Act = act;
            this.Adress = adress ?? throw new ArgumentNullException(nameof(adress));
            Isregister = isregister;
        }

        public int Type { get => type; set => type = value; }
        public int Id { get => id; set => id = value; }
        public int ProductID { get => productID; set => productID = value; }
        public int Quntity { get => quntity; set => quntity = value; }
        public int Min { get => min; set => min = value; }
        public int Max { get => max; set => max = value; }
        public int SumMin { get => sumMin; set => sumMin = value; }
        public int SumMax { get => sumMax; set => sumMax = value; }
        public string Adress { get => adress; set => adress = value; }
        public bool Isregister { get => isregister; set => isregister = value; }
        internal LogicalConnections Act { get => act; set => act = value; }
    }
}
