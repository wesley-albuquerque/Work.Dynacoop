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
    public class ContatoController
    {
        public Contato Contato { get; set; }

        public Conta Conta { get; set; }

        

        public ContatoController(CrmServiceClient serviceClient, Conta account)
        {
            Contato = new Contato(serviceClient);
            Conta = account;
            
            
        }


        public void CriarContato()
        {
            Console.WriteLine("Você deseja criar um contato para essa conta? (S/N)");
            var input = Console.ReadLine();

            if (input.ToString().ToUpper() == "S")
            {
                Console.WriteLine("Digite o CPF:");
                Contato.CPF = Console.ReadLine();

                Console.WriteLine("Digite o nome");
                Contato.NomeContato = Console.ReadLine();

                Console.WriteLine("Digite o Sobrenome");
                Contato.Sobrenome =  Console.ReadLine();

                if (ValidaContato())
                {
                    Contato.CriaContato();
                    Conta.AtualizaContato(Contato.Contact.Id);
                    Console.WriteLine("Contato criado com sucesso e atribuído a nova conta");

                }
                else 
                {
                    Console.WriteLine("Contato já existente, deseja vincular a conta criada?[S/N]");
                    input = Console.ReadLine();
                    if (input.ToUpper() == "S")
                    {
                        Conta.AtualizaContato(Contato.Contact.Id);
                        Console.WriteLine("Contato existente atribuído com sucesso a nova conta");
                        Console.ReadKey();
                    }
                    

                }
            }

        }

        public bool ValidaContato()
        {
            Entity contacts = Contato.GetContactByCpf();

            if (contacts != null)
            {
                Contato.Contact.Id = contacts.Id;
                return false;
            }

            else
                return true;



        }

    }
}
