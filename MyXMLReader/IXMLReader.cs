using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Graph;
using Company;

namespace MyXMLReader
{
    public interface IXMLReader
    {
        Company.Company Read();
    }
}
