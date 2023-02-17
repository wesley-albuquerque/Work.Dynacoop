using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Tooling.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Work.Dynacoop.Model
{
    public class Contato
    {
        public Entity Contact = new Entity("contact");

        public string LogicalName = "contact";
        public string NomeContato { get; set; }
        public string Sobrenome { get; set; }
        public string CPF { get; set; }

        public CrmServiceClient ServiceClient { get; set; }

        public Contato(CrmServiceClient serviceClient)
        {
            ServiceClient = serviceClient;
        }

        public Entity GetContactByCpf()
        {
            QueryExpression consulta = new QueryExpression(LogicalName);
            consulta.ColumnSet.AddColumns("contactid");
            consulta.Criteria.AddCondition("wes_cpf",ConditionOperator.Equal, CPF);
            EntityCollection contacts = ServiceClient.RetrieveMultiple(consulta);

            return contacts.Entities.FirstOrDefault();
        }

        public void CriaContato()
        {
            Contact.Attributes["wes_cpf"] = CPF;
            Contact.Attributes["firstname"] = NomeContato;
            Contact.Attributes["lastname"] = Sobrenome;
            Contact.Id = ServiceClient.Create(Contact);
            

        }

       
    }
}
