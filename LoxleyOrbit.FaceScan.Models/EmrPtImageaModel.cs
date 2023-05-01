using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoxleyOrbit.FaceScan.Models
{
    public class EmrPtImageaModel
    {
        private long HN;
        private string User;
        private string Password;
        private string IPAddress;

        public long hn { get => HN; set => HN = value; }
        public string user { get => User; set => User = value; }
        public string password { get => Password; set => Password = value; }
        public string ipaddress { get => IPAddress; set => IPAddress = value; }
    }
}
