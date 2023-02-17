﻿using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Tooling.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Work.Dynacoop.Controller;

namespace Work.Dynacoop
{
    internal class Program
    {
        static void Main(string[] args)
        {
            CrmServiceClient serviceClient = Conexão.GetService();

            ContaController contaController = new ContaController(serviceClient);


            var existeConta = contaController.CriarConta();


            if (existeConta)
            {
                ContatoController contatoController = new ContatoController(serviceClient, contaController.Conta);
                contatoController.CriarContato();

            }

        }

    }
}
