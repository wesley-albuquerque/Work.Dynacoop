using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Tooling.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Work.Dynacoop.Model;

namespace Work.Dynacoop.Controller
{
    public class ContaController
    {

        public Conta Conta { get; set; }


        public ContaController(CrmServiceClient serviceClient)
        {

            Conta = new Conta(serviceClient);

        }


        public bool CriarConta()

        {
            Console.WriteLine("Por favor informe o nome da Conta");

            Conta.NomeConta = Console.ReadLine();

            Console.WriteLine("Escreva o CNPJ da conta");
            Conta.CNPJ = Console.ReadLine();

            Console.WriteLine("Digite o código da Moeda");
            Conta.Moeda = Console.ReadLine();

            Console.WriteLine("Escreva o limite de crédito da conta");
            Conta.LimitedeCredito = new Money(decimal.Parse(Console.ReadLine()));

            Console.WriteLine("Digite a Classificação do cliente: 1- Interessado, 2 - Pouco Interessado ou 3 - Muito interessado");
            Conta.Classificacao = new OptionSetValue(int.Parse(Console.ReadLine()));

            if (ValidaConta())
            {
                Conta.CriarConta();
                Console.WriteLine("Conta criada com sucesso");
                return true;
            }
            else
            {
                Console.WriteLine("Conta já existente no sistema");
                return false;
            }
        }




        public bool ValidaConta()
        {
            Entity accounts = Conta.GetAccountByCNPJ();

            if (accounts != null)                         
                return false;
            
            else
                return true;



        }

        



    }
}
