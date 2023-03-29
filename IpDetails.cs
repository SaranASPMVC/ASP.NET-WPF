using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WPFWatermarking
{
    class IpDetails
    {
        string ipname = "", servername = "";
        int slno = 0;
        public string getIpname()
        {
            return ipname;
        }
        public void setIpname(string _ipname)
        {
            ipname = _ipname;
        }
        public string getServername()
        {
            return servername;
        }
        public void setServername(string _servername)
        {
            servername = _servername;
        }
        public int getSlno()
        {
            return slno;
        }
        public void setSlno(int _slno)
        {
            slno = _slno;
        }
    }
}
