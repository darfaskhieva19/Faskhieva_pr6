using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Фасхиева_ПР6
{
    public partial class Clients
    {
        public string NameClient
        {
            get
            {
                return surname + " " + name[0] + "." + " " + patronimyc[0] + ".";
            }
        }
    }
}
