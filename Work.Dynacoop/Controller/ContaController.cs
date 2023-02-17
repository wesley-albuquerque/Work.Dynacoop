using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Tooling.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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
            ExisteConta();


            Console.WriteLine("Escreva o CNPJ da conta");
            Conta.CNPJ = Console.ReadLine();
            if (!ValidaConta())
            {
                Console.WriteLine("Conta já existe no sistema. Pressione qualquer botão para reiniciar.");
                Console.ReadKey();
                Console.Clear();
                Program.Main();
                return false;
            }
            else
            {
                Console.WriteLine("Digite o código da Moeda");
                Conta.Moeda = Console.ReadLine();
  
                while(!ValidaMoeda())
                {
                    Console.WriteLine("Moeda não cadastrada no sistema, tente novamente");
                    Conta.Moeda = Console.ReadLine();
                }

                Console.WriteLine("Escreva o limite de crédito da conta");
                Conta.LimitedeCredito = new Money(decimal.Parse(Console.ReadLine()));

                Console.WriteLine("Digite a Classificação do cliente: 1- Interessado, 2 - Pouco Interessado ou 3 - Muito interessado");
                Conta.Classificacao = new OptionSetValue(int.Parse(Console.ReadLine()));
                Conta.CriarConta();
                Console.WriteLine("Conta criada com sucesso");
                return true;
            }
        }





        public bool ValidaConta()
        {
            Entity accounts = Conta.GetAccountByCNPJ();

            if (accounts != null)
                return false;

            else
            {
  
                return true;

            }


        }

        public void ExisteConta()
        {
            var colecao = Conta.GetAccountByName();
            if (colecao != null)
            {
                Console.WriteLine($"Encontrado conta de CNPJ:{colecao.Attributes["wes_cnpj"]}");

            }


        }

        public bool ValidaMoeda()
        {
            if (Conta.GetMoneyByName() == null)
            {
                return false;

            }
            else 
                return true;
        }





    }
}

