using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static List<(int Codigo, string Nome, int Quantidade, double Preco)> estoque = new List<(int, string, int, double)>();

    static void Main()
    {
        while (true)
        {
            MostrarMenu();

            string opcao = Console.ReadLine();

            switch (opcao)
            {
                case "1":
                    CadastrarProduto();
                    break;
                case "2":
                    ConsultarProduto();
                    break;
                case "3":
                    AtualizarEstoque();
                    break;
                case "4":
                    GerarRelatorioQuantidadeMinima();
                    break;
                case "5":
                    GerarRelatorioValorEntreMinMax();
                    break;
                case "6":
                    GerarRelatorioValorTotalEstoque();
                    break;
                case "7":
                    return;
                default:
                    Console.WriteLine("Opção inválida. Por favor, ente novamente.");
                    break;
            }
        }
    }

    static void MostrarMenu()
    {
        Console.WriteLine("\n-------- Menu --------");
        Console.WriteLine("1. Cadastrar Produto");
        Console.WriteLine("2. Consultar Produto");
        Console.WriteLine("3. Atualizar Estoque");
        Console.WriteLine("4. Relatório: Produtos abaixo do limite determinado de quantidade");
        Console.WriteLine("5. Relatório: Produtos com valor entre um mínimo e um máximo determinado");
        Console.WriteLine("6. Relatório: Valor total em estoque & valor total por produto");
        Console.WriteLine("7. Sair");
        Console.Write("Escolha uma opção: ");
    }

    static void CadastrarProduto()
    {
        try
        {
            Console.Write("Digite o código do produto: ");
            int codigo = int.Parse(Console.ReadLine());

            Console.Write("Digite o nome do produto: ");
            string nome = Console.ReadLine();

            Console.Write("Digite a quantidade em estoque: ");
            int quantidade = int.Parse(Console.ReadLine());

            Console.Write("Digite o preço unitário: ");
            double preco = double.Parse(Console.ReadLine());

            estoque.Add((codigo, nome, quantidade, preco));
            Console.WriteLine("Produto cadastrado com sucesso!");
        }
        catch (FormatException)
        {
            Console.WriteLine("Erro: Formato inválido. Certifique-se de inserir um números corretos.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro: {ex.Message}");
        }
    }

    static void ConsultarProduto()
    {
        Console.Write("Digite o código do produto para consulta: ");
        int codigoConsulta = int.Parse(Console.ReadLine());

        var produto = estoque.FirstOrDefault(p => p.Codigo == codigoConsulta);

        if (produto.Equals(default(ValueTuple<int, string, int, double>)))
        {
            throw new ProdutoNaoEncontradoException($"Produto com código {codigoConsulta} não encontrado.");
        }
        else
        {
            Console.WriteLine($"Produto encontrado: {produto}");
        }
    }

    static void AtualizarEstoque()
    {
        try
        {
            Console.Write("Digite o código do produto para atualização de estoque: ");
            int codigoAtualizacao = int.Parse(Console.ReadLine());

            var produto = estoque.FirstOrDefault(p => p.Codigo == codigoAtualizacao);

            if (produto.Equals(default(ValueTuple<int, string, int, double>)))
            {
                throw new ProdutoNaoEncontradoException($"Produto com código {codigoAtualizacao} não encontrado.");
            }
            else
            {
                Console.Write("Digite a quantidade e a informaçõapara adição (+) ou remoção (-): ");
                int quantidadeAtualizacao = int.Parse(Console.ReadLine());

                if (produto.Quantidade + quantidadeAtualizacao < 0)
                {
                    throw new QuantidadeInsuficienteException("Quantidade insuficiente em estoque.");
                }

                produto.Quantidade += quantidadeAtualizacao;
                Console.WriteLine("Estoque atualizado com sucesso!");
            }
        }
        catch (FormatException)
        {
            Console.WriteLine("Erro: Formato inválido. Se certifique de inserir números corretos.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro: {ex.Message}");
        }
    }

    static void GerarRelatorioQuantidadeMinima()
    {
        try
        {
            Console.Write("Digite a quantidade mínima desejada: ");
            int quantidadeMinima = int.Parse(Console.ReadLine());

            var produtosAbaixoMinimo = estoque.Where(p => p.Quantidade < quantidadeMinima).ToList();

            if (produtosAbaixoMinimo.Count == 0)
            {
                Console.WriteLine("Nenhum produto encontrado abaixo do limite de quantidade especificada.");
            }
            else
            {
                Console.WriteLine("\n------ Produtos abaixo do limite de quantidade especificada ------");
                foreach (var produto in produtosAbaixoMinimo)
                {
                    Console.WriteLine(produto);
                }
            }
        }
        catch (FormatException)
        {
            Console.WriteLine("Erro: Formato inválido. Se certifique de inserir números corretos.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro: {ex.Message}");
        }
    }

    static void GerarRelatorioValorEntreMinMax()
    {
        try
        {
            Console.Write("Digite o valor mínimo desejado: ");
            double valorMinimo = double.Parse(Console.ReadLine());

            Console.Write("Digite o valor máximo desejado: ");
            double valorMaximo = double.Parse(Console.ReadLine());

            var produtosEntreMinMax = estoque.Where(p => p.Preco >= valorMinimo && p.Preco <= valorMaximo).ToList();

            if (produtosEntreMinMax.Count == 0)
            {
                Console.WriteLine("Nenhum produto encontrado dentro da faixa de preço desejada.");
            }
            else
            {
                Console.WriteLine("\n------ Produtos dentro da faixa de preço ------");
                foreach (var produto in produtosEntreMinMax)
                {
                    Console.WriteLine(produto);
                }
            }
        }
        catch (FormatException)
        {
            Console.WriteLine("Erro: Formato inválido. Se certifique de inserir números corretos.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro: {ex.Message}");
        }
    }

    static void GerarRelatorioValorTotalEstoque()
    {
        double valorTotalEstoque = estoque.Sum(p => p.Quantidade * p.Preco);

        if (valorTotalEstoque == 0)
        {
            Console.WriteLine("Estoque vazio. Nenhum valor a ser calculado.");
        }
        else
        {
            Console.WriteLine($"\nValor total do estoque: {valorTotalEstoque:C}");

            Console.WriteLine("\n------ Valor total de cada produto ------");
            foreach (var produto in estoque)
            {
                double valorProduto = produto.Quantidade * produto.Preco;
                Console.WriteLine($"{produto.Nome}: {valorProduto:C}");
            }
        }
    }
}

class ProdutoNaoEncontradoException : Exception
{
    public ProdutoNaoEncontradoException(string mensagem) : base(mensagem)
    {
    }
}

class QuantidadeInsuficienteException : Exception
{
    public QuantidadeInsuficienteException(string mensagem) : base(mensagem)
    {
    }
}
