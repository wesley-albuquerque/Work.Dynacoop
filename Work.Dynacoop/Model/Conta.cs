using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xrm.Tooling.Connector;
using Work.Dynacoop.Controller;
using Microsoft.Crm.Sdk.Messages;

namespace Work.Dynacoop.Model
{
    public class Conta
    {

        public string NomeConta { get; set; }
        public string CNPJ { get; set; }
        public string Moeda { get; set; }
        public Money LimitedeCredito { get; set; }
        public OptionSetValue Classificacao { get; set; }

        public Entity Account = new Entity("account");

        public string LogicalName = "account";

        public CrmServiceClient ServiceClient { get; set; }

        public Conta(CrmServiceClient serviceClient)
        {
            ServiceClient = serviceClient;
        }


        public Entity GetAccountByCNPJ()
        {
            QueryExpression consulta = new QueryExpression(LogicalName);
            consulta.ColumnSet.AddColumn("wes_cnpj");
            consulta.Criteria.AddCondition("wes_cnpj", ConditionOperator.Equal, CNPJ);
            EntityCollection accounts = ServiceClient.RetrieveMultiple(consulta);

            return accounts.Entities.FirstOrDefault();
        }

        public Entity GetMoneyByName()
        {
            QueryExpression consulta = new QueryExpression("transactioncurrency");
            consulta.ColumnSet.AddColumns("transactioncurrencyid");
            consulta.Criteria.AddCondition("isocurrencycode", ConditionOperator.Equal, Moeda);
            EntityCollection currency = ServiceClient.RetrieveMultiple(consulta);
            return currency.Entities.FirstOrDefault();

        }

        public Entity GetAccountByName()
        {
            QueryExpression consulta = new QueryExpression(LogicalName);
            consulta.ColumnSet.AddColumns("name", "wes_cnpj", "transactioncurrencyid", "wes_limitecredito", "wes_classificacao");
            consulta.Criteria.AddCondition("name", ConditionOperator.Equal, NomeConta);
            EntityCollection accounts = ServiceClient.RetrieveMultiple(consulta);
            return accounts.Entities.FirstOrDefault();
        }

        public void CriarConta()
        {
            Account.Attributes["name"] = NomeConta;
            Account.Attributes["wes_cnpj"] = CNPJ;
            Account.Attributes["transactioncurrencyid"] = new EntityReference("transactioncurrency", GetMoneyByName().Id);
            Account.Attributes["wes_limitecredito"] = LimitedeCredito;
            Account.Attributes["wes_classificacao"] = Classificacao;
            Account.Id = ServiceClient.Create(Account);
        }

        public void AtualizaContato(Guid contactId)
        {

            Account.Attributes["primarycontactid"] = new EntityReference("contact", contactId);
            ServiceClient.Update(Account);
        }
    }
}
