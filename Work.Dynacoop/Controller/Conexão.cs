using Microsoft.Xrm.Tooling.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Work.Dynacoop.Controller
{
    public class Conexão
    {
        public static CrmServiceClient GetService()
        {

            string url = "wesleywork";
            string clientId = "3e279d8a-7ffa-4f54-a65e-bc65d516b293";
            string clientSecret = "eo18Q~O4Ho_DYvtoNDFD~tJNVFwpOyMsW4U4Zbmb";
            string stringConnection = $@"AuthType=ClientSecret;
                                      url=https://{url}.crm2.dynamics.com;
                                      ClientId={clientId};
                                      ClientSecret={clientSecret};";
            CrmServiceClient serviceClient = new CrmServiceClient(stringConnection);
            
            if (serviceClient.CurrentAccessToken.Equals(null))
                Console.WriteLine("Erro na conexão");
            

            return serviceClient;
        }
    }
}
