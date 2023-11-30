using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main()
    {
        try
        {
            List<Pessoa> pessoas = new List<Pessoa>();


            pessoas.Add(new Advogado("Ricardo Teixeira", new DateTime(1990, 5, 15), "12345678901", "CNA123"));
            pessoas.Add(new Advogado("Advogado2", new DateTime(1985, 8, 20), "98765432101", "CNA456"));

            pessoas.Add(new Cliente("Rita Santos", new DateTime(1988, 4, 21), "02538968518", "Solteiro", "Professor"));
            pessoas.Add(new Cliente("Isabela Chaves", new DateTime(1986, 2, 4), "01518715078", "Casado", "Medica"));

            Console.WriteLine("Advogados com idade entre 30 e 40 anos:");
            List<Advogado> advogadosRelatorio1 = AdvogadosEntreIdades(pessoas.OfType<Advogado>(), 30, 40);
            ImprimirAdvogados(advogadosRelatorio1);

            Console.WriteLine("\nClientes com idade entre 25 e 35 anos:");
            List<Cliente> clientesRelatorio2 = ClientesEntreIdades(pessoas.OfType<Cliente>(), 25, 35);
            ImprimirClientes(clientesRelatorio2);

            Console.WriteLine("\nClientes solteiros:");
            List<Cliente> clientesRelatorio3 = ClientesPorEstadoCivil(pessoas.OfType<Cliente>(), "Solteiro");
            ImprimirClientes(clientesRelatorio3);

            Console.WriteLine("\nClientes em ordem alfabética:");
            List<Cliente> clientesRelatorio4 = ClientesOrdemAlfabetica(pessoas.OfType<Cliente>());
            ImprimirClientes(clientesRelatorio4);

            Console.WriteLine("\nDigite um texto para buscar clientes por profissão:");
            string textoBusca = Console.ReadLine();
            List<Cliente> clientesRelatorio5 = ClientesPorProfissao(pessoas.OfType<Cliente>(), textoBusca);
            ImprimirClientes(clientesRelatorio5);

            Console.WriteLine("\nDigite o mês para verificar aniversariantes:");
            int mesAniversario = int.Parse(Console.ReadLine());
            List<Pessoa> aniversariantes = AniversariantesDoMes(pessoas, mesAniversario);
            ImprimirAniversariantes(aniversariantes);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ocorreu um erro: {ex.Message}");
        }
    }


   static List<Advogado> AdvogadosEntreIdades(IEnumerable<Advogado> advogados, int idadeMin, int idadeMax)
    {
        return advogados.Where(a => CalculaIdade(a.DataNascimento) >= idadeMin && CalculaIdade(a.DataNascimento) <= idadeMax).ToList();
    }

    static List<Cliente> ClientesEntreIdades(IEnumerable<Cliente> clientes, int idadeMin, int idadeMax)
    {
        return clientes.Where(c => CalculaIdade(c.DataNascimento) >= idadeMin && CalculaIdade(c.DataNascimento) <= idadeMax).ToList();
    }

    static List<Cliente> ClientesPorEstadoCivil(IEnumerable<Cliente> clientes, string estadoCivil)
    {
        return clientes.Where(c => c.EstadoCivil.Equals(estadoCivil, StringComparison.OrdinalIgnoreCase)).ToList();
    }

    static List<Cliente> ClientesOrdemAlfabetica(IEnumerable<Cliente> clientes)
    {
        return clientes.OrderBy(c => c.Nome).ToList();
    }

    static List<Cliente> ClientesPorProfissao(IEnumerable<Cliente> clientes, string textoBusca)
    {
        return clientes.Where(c => c.Profissao.Contains(textoBusca, StringComparison.OrdinalIgnoreCase)).ToList();
    }

    static List<Pessoa> AniversariantesDoMes(IEnumerable<Pessoa> pessoas, int mes)
    {
        return pessoas.Where(p => p.DataNascimento.Month == mes).ToList();
    }

static int CalculaIdade(DateTime dataNascimento)
    {
        int idade = DateTime.Now.Year - dataNascimento.Year;
        if (DateTime.Now.DayOfYear < dataNascimento.DayOfYear)
            idade--;

        return idade;
    }

    static void ImprimirAdvogados(IEnumerable<Advogado> advogados)
    {
        foreach (var advogado in advogados)
        {
            Console.WriteLine($"Nome: {advogado.Nome}, Data de Nascimento: {advogado.DataNascimento.ToShortDateString()}, CPF: {advogado.CPF}, CNA: {advogado.CNA}");
        }
    }

    static void ImprimirClientes(IEnumerable<Cliente> clientes)
    {
        foreach (var cliente in clientes)
        {
            Console.WriteLine($"Nome: {cliente.Nome}, Data de Nascimento: {cliente.DataNascimento.ToShortDateString()}, CPF: {cliente.CPF}, Estado Civil: {cliente.EstadoCivil}, Profissão: {cliente.Profissao}");
        }
    }

    static void ImprimirAniversariantes(IEnumerable<Pessoa> aniversariantes)
    {
        foreach (var pessoa in aniversariantes)
        {
            Console.WriteLine($"Nome: {pessoa.Nome}, Data de Nascimento: {pessoa.DataNascimento.ToShortDateString()}");
        }
    }
}

interface IPessoa
{
    string Nome { get; set; }
    DateTime DataNascimento { get; set; }
    string CPF { get; set; }
}

abstract class Pessoa : IPessoa
{
    public string Nome { get; set; }
    public DateTime DataNascimento { get; set; }
    public string CPF { get; set; }
}

class Advogado : Pessoa
{
    public string CNA { get; set; }


    public Advogado(string nome, DateTime dataNascimento, string cpf, string cna)
    {
        if (!ValidarCPF(cpf))
            throw new ArgumentException("CPF inválido");

        Nome = nome;
        DataNascimento = dataNascimento;
        CPF = cpf;
        CNA = cna;
    }

 private bool ValidarCPF(string cpf)
    {
         return cpf.Length == 11 && cpf.All(char.IsDigit);
    }
}


class Cliente : Pessoa
{
    public string EstadoCivil { get; set; }
    public string Profissao { get; set; }

    public Cliente(string nome, DateTime dataNascimento, string cpf, string estadoCivil, string profissao)
    {
        if (!ValidarCPF(cpf))
            throw new ArgumentException("CPF inválido");

        Nome = nome;
        DataNascimento = dataNascimento;
        CPF = cpf;
        EstadoCivil = estadoCivil;
        Profissao = profissao;
    }

    private bool ValidarCPF(string cpf)
    {
        return true;
    }
}