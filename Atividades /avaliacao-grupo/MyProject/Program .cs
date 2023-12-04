using System;
using System.Collections.Generic;
using System.Linq;

namespace Namespace
{
    public class Program
    {
        static Escritorio escritorio = new Escritorio();

        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("---- Menu Principal ---- \n" +
                                  "1. Menu de Cliente\n" +
                                  "2. Menu de Advogado\n" +
                                  "3. Menu de Caso Jurídico\n" +
                                  "4. Menu de Relatório\n" +
                                  "5. Sair\n");

                Console.Write("Escolha uma opção: ");
                string opcaoMenuPrincipal = Console.ReadLine() ?? "";

                Console.WriteLine();

                switch (opcaoMenuPrincipal)
                {
                    case "1":
                        MenuCliente();
                        break;
                    case "2":
                        MenuAdvogado();
                        break;
                    case "3":
                        MenuCasoJuridico();
                        break;
                    case "4":
                        MenuRelatorio();
                        break;
                    case "5":
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Opção inválida. Tente novamente.");
                        break;
                }
            }
        }
    static void MenuAdvogado()
    {
        while (true)
        {
            Console.WriteLine("---- Menu de Advogado ---- \n" +
            "1. Adicionar Advogado\n" +
            "2. Listar Advogados\n" +
            "3. Deletar Advogado\n" +
            "4. Voltar ao Menu Principal\n");

            Console.Write("Escolha uma opção: ");
            string opcaoMenuAdvogado = Console.ReadLine() ?? "";

            Console.WriteLine();

            switch (opcaoMenuAdvogado)
            {
                case "1":
                    AdicionarAdvogado();
                    Console.WriteLine();
                    break;
                case "2":
                    escritorio.ListarAdvogados();
                    Console.WriteLine();
                    break;
                case "3":
                    DeletarAdvogado(escritorio);
                    Console.WriteLine();
                    break;
                case "4":
                    return;
                default:
                    Console.WriteLine("Opção inválida. Tente novamente.");
                    break;
            }
        }
    }

    static void MenuRelatorio()
    {
        while (true)
        {
            Console.WriteLine("---- Menu de Relatório ---- \n" +
            "1. Relatório: Advogados por Idade\n" +
            "2. Relatório: Clientes por Idade\n" +
            "3. Relatório: Clientes por Estado Civil\n" +
            "4. Relatório: Clientes em Ordem Alfabética\n" +
            "5. Relatório: Clientes por Profissão\n" +
            "6. Relatório: Aniversariantes do Mês\n" +
            "7. Voltar ao Menu Principal\n");

            Console.Write("Escolha uma opção: ");
            string opcaoMenuRelatorio = Console.ReadLine() ?? "";

            Console.WriteLine();

            switch (opcaoMenuRelatorio)
            {
                case "1":
                    RelatorioAdvogadosPorIdade();
                    Console.WriteLine();
                    break;
                case "2":
                    RelatorioClientesPorIdade();
                    Console.WriteLine();
                    break;
                case "3":
                    RelatorioClientesPorEstadoCivil();
                    Console.WriteLine();
                    break;
                case "4":
                    RelatorioClientesOrdemAlfabetica();
                    Console.WriteLine();
                    break;
                case "5":
                    RelatorioClientesPorProfissao();
                    Console.WriteLine();
                    break;
                case "6":
                    RelatorioAniversariantesDoMes();
                    Console.WriteLine();
                    break;
                case "7":
                    return;
                default:
                    Console.WriteLine("Opção inválida. Tente novamente.");
                    break;
            }
        }
    }

    static void MenuCasoJuridico()
        {
            while (true)
            {
                Console.WriteLine("---- Menu de Casos Jurídicos ---- \n" +
                                  "1. Adicionar Caso Jurídico\n" +
                                  "2. Listar Casos Jurídicos\n" +
                                  "3. Deletar Caso Jurídico\n" +
                                  "4. Voltar ao Menu Principal\n");

                Console.Write("Escolha uma opção: ");
                string opcaoMenuCasoJuridico = Console.ReadLine() ?? "";

                Console.WriteLine();

                switch (opcaoMenuCasoJuridico)
                {
                    case "1":
                        AdicionarCasoJuridico();
                        Console.WriteLine();
                        break;
                    case "2":
                        escritorio.ListarCasosJuridicos();
                        Console.WriteLine();
                        break;
                    case "3":
                        DeletarCasoJuridico(escritorio);
                        Console.WriteLine();
                        break;
                    case "4":
                        return;
                    default:
                        Console.WriteLine("Opção inválida. Tente novamente.");
                        break;
                }
            }
        }

        static void AdicionarCasoJuridico()
        {
            Console.Write("Data de Abertura do Caso (DD/MM/AAAA): ");
            DateTime abertura;
            try
            {
                abertura = DateTime.ParseExact(Console.ReadLine() ?? "", "dd/MM/yyyy", null);
            }
            catch (FormatException)
            {
                Console.WriteLine("Formato de data inválido. Certifique-se de usar o formato DD/MM/AAAA.");
                return;
            }

            Console.Write("Probabilidade de Sucesso (0-100): ");
            float probabilidadeSucesso;
            if (!float.TryParse(Console.ReadLine(), out probabilidadeSucesso) || probabilidadeSucesso < 0 || probabilidadeSucesso > 100)
            {
                Console.WriteLine("Probabilidade inválida. Certifique-se de inserir um valor entre 0 e 100.");
                return;
            }

            List<Documento> documentos = AdicionarDocumentos();
            List<(float, string)> custos = AdicionarCustos();
            List<Advogado> advogadosAssociados = AdicionarAdvogadosAoCaso();
            Cliente clienteAssociado = AdicionarClienteAoCaso();

            Console.Write("Status do Caso: ");
            string status = Console.ReadLine() ?? "";

            CasoJuridico casoJuridico = new CasoJuridico(abertura, probabilidadeSucesso, documentos, custos, DateTime.MinValue, advogadosAssociados, clienteAssociado, status);
            escritorio.AdicionarCasoJuridico(casoJuridico);
            Console.WriteLine("Caso Jurídico adicionado com sucesso!");
            Console.WriteLine();
        }

        static List<Documento> AdicionarDocumentos()
        {
            List<Documento> documentos = new List<Documento>();

            while (true)
            {
                Console.Write("Deseja adicionar um documento ao caso? (S/N): ");
                string resposta = Console.ReadLine()?.ToUpper() ?? "";

                if (resposta != "S")
                {
                    break;
                }

                Console.Write("Data de Modificação do Documento (DD/MM/AAAA): ");
                DateTime dataModificacao;
                try
                {
                    dataModificacao = DateTime.ParseExact(Console.ReadLine() ?? "", "dd/MM/yyyy", null);
                }
                catch (FormatException)
                {
                    Console.WriteLine("Formato de data inválido. Certifique-se de usar o formato DD/MM/AAAA.");
                    continue;
                }

                Console.Write("Código do Documento: ");
                int codigo;
                if (!int.TryParse(Console.ReadLine(), out codigo))
                {
                    Console.WriteLine("Código inválido. Certifique-se de inserir um número inteiro.");
                    continue;
                }

                Console.Write("Tipo do Documento: ");
                string tipo = Console.ReadLine() ?? "";

                Console.Write("Descrição do Documento: ");
                string descricao = Console.ReadLine() ?? "";

                Documento documento = new Documento(dataModificacao, codigo, tipo, descricao);
                documentos.Add(documento);

                Console.WriteLine("Documento adicionado ao caso.");
            }

            return documentos;
        }

        static List<(float, string)> AdicionarCustos()
        {
            List<(float, string)> custos = new List<(float, string)>();

            while (true)
            {
                Console.Write("Deseja adicionar um custo ao caso? (S/N): ");
                string resposta = Console.ReadLine()?.ToUpper() ?? "";

                if (resposta != "S")
                {
                    break;
                }

                Console.Write("Valor do Custo: ");
                float valor;
                if (!float.TryParse(Console.ReadLine(), out valor) || valor < 0)
                {
                    Console.WriteLine("Valor inválido. Certifique-se de inserir um número válido.");
                    continue;
                }

                Console.Write("Descrição do Custo: ");
                string descricao = Console.ReadLine() ?? "";

                custos.Add((valor, descricao));

                Console.WriteLine("Custo adicionado ao caso.");
            }

            return custos;
        }

        static List<Advogado> AdicionarAdvogadosAoCaso()
        {
            escritorio.ListarAdvogados();

            List<Advogado> advogadosAssociados = new List<Advogado>();

            while (true)
            {
                Console.Write("Digite o CPF do advogado para associar ao caso (ou '0' para sair): ");
                string cpfAdvogado = Console.ReadLine() ?? "";

                if (cpfAdvogado == "0")
                {
                    break;
                }

                Advogado advogado = escritorio.Advogados.FirstOrDefault(a => a.CPF == cpfAdvogado);

                if (advogado != null)
                {
                    advogadosAssociados.Add(advogado);
                    Console.WriteLine("Advogado associado ao caso.");
                }
                else
                {
                    Console.WriteLine("Advogado não encontrado. Tente novamente.");
                }
            }

            return advogadosAssociados;
        }

        static Cliente AdicionarClienteAoCaso()
        {
            escritorio.ListarClientes();

            Console.Write("Digite o CPF do cliente para associar ao caso: ");
            string cpfCliente = Console.ReadLine() ?? "";

            Cliente clienteAssociado = escritorio.Clientes.FirstOrDefault(c => c.CPF == cpfCliente);

            if (clienteAssociado != null)
            {
                Console.WriteLine("Cliente associado ao caso.");
            }
            else
            {
                Console.WriteLine("Cliente não encontrado ou CPF inválido. Certifique-se de inserir um CPF válido.");
            }

            return clienteAssociado;
        }

        static void DeletarCasoJuridico(Escritorio escritorio)
        {
            Console.Write("Digite a data de abertura do caso a ser deletado (DD/MM/AAAA): ");
            DateTime dataAbertura;
            try
            {
                dataAbertura = DateTime.ParseExact(Console.ReadLine() ?? "", "dd/MM/yyyy", null);
            }
            catch (FormatException)
            {
                Console.WriteLine("Formato de data inválido. Certifique-se de usar o formato DD/MM/AAAA.");
                return;
            }

            escritorio.DeletarCasoJuridico(dataAbertura);
            Console.WriteLine("Caso Jurídico deletado com sucesso!");
            Console.WriteLine();
        }
    }
}