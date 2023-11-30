using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main()
{
    List<Advogado> advogados = new List<Advogado>();
    List<Cliente> clientes = new List<Cliente>();

    advogados.Add(new Advogado("Ricardo Teixeira", new DateTime (1990, 5, 15), "12345678901", "CNA123"));
    advogados.Add(new Advogado("Dan Sampaio", new DateTime (1985, 8, 20), "98765432101", "CNA456"));

    clientes.Add(new Cliente("Rita Santos", new DataTime(1988, 4, 21), "02538968518", "solteria", "professora"));
    clientes.Add(new Cliente("Isabela Chaves", new DataTime(1986, 2 ,4), "01518715078", "casada" , "medica" ));

    Console.WriteLine("Advogados com idade entre 30 e 40 anos:");
    List<Advogado> advogadosRelatorio1 = AdvogadosEntreIdades(advogados, 30, 40);
    ImprimirAdvogados(advogadosRelatorio1);
    
    Console.WriteLine("\nClientes com idade entre 25 e 35 anos:");
    List<Cliente> clientesRelatorio2 = ClientesEntreIdades(clientes, 25, 35);
    ImprimirClientes(clientesRelatorio2);

    Console.WriteLine("\nClientes solteiros:");
    List<Cliente> clientesRelatorio3 = ClientesPorEstadoCivil(clientes, "Solteiro");
    ImprimirClientes(clientesRelatorio3);

    Console.WriteLine("\nClientes em ordem alfabética:");
    List<Cliente> clientesRelatorio4 = ClientesOrdemAlfabetica(clientes);
    ImprimirClientes(clientesRelatorio4);

    Console.WriteLine("\nDigite um texto para buscar clientes por profissão:");
    string textoBusca = Console.ReadLine();
    List<Cliente> clientesRelatorio5 = ClientesPorProfissao(clientes, textoBusca);
    ImprimirClientes(clientesRelatorio5);

    Console.WriteLine("\nDigite o mês para verificar aniversariantes:");
    int mesAniversario = int.Parse(Console.ReadLine());
    List<Pessoa> aniversariantes = AniversariantesDoMes(advogados, clientes, mesAniversario);
    ImprimirAniversariantes(aniversariantes);
  
}

    static List<Advogado> AdvogadosEntreIdades(List<Advogado> advogados, int idadeMin, int idadeMax)
    {
        return advogados.Where(a => CalculaIdade(a.DataNascimento) >= idadeMin && CalculaIdade(a.DataNascimento) <= idadeMax).ToList();
    }

    static List<Cliente> ClientesEntreIdades(List<Cliente> clientes, int idadeMin, int idadeMax)
    {
        return clientes.Where(c => CalculaIdade(c.DataNascimento) >= idadeMin && CalculaIdade(c.DataNascimento) <= idadeMax).ToList();
    }

    static List<Cliente> ClientesPorEstadoCivil(List<Cliente> clientes, string estadoCivil)
    {
        return clientes.Where(c => c.EstadoCivil.Equals(estadoCivil, StringComparison.OrdinalIgnoreCase)).ToList();
    }

    static List<Cliente> ClientesOrdemAlfabetica(List<Cliente> clientes)
    {
        return clientes.OrderBy(c => c.Nome).ToList();
    }
    static List<Cliente> ClientesPorProfissao(List<Cliente> clientes, string textoBusca)
    {
        return clientes.Where(c => c.Profissao.Contains(textoBusca, StringComparison.OrdinalIgnoreCase)).ToList();
    }
    static List<Pessoa> AniversariantesDoMes(List<Advogado> advogados, List<Cliente> clientes, int mesAniversario)
    {
         return advogados.Concat<Pessoa>(clientes).Where(p => p.DataNascimento.Month == mes).ToList();
    }

    static int CalculaIdade(DateTime dataNascimento)
    {
        int idade = DateTime.Now.Year - dataNascimento.Year;
        if (DateTime.Now.DayOfYear < dataNascimento.DayOfYear)
            idade--;

        return idade;
    }
        static void ImprimirAdvogados(List<Advogado> advogados)
   {
        foreach (var advogado in advogados)
        {
            Console.WriteLine($"Nome: {advogado.Nome}, Data de Nascimento: {advogado.DataNascimento.ToShortDateString()}, CPF: {advogado.CPF}, CNA: {advogado.CNA}");
        }
    }

    static void ImprimirClientes(List<Cliente> clientes)
    {
        foreach (var cliente in clientes)
        {
            Console.WriteLine($"Nome: {cliente.Nome}, Data de Nascimento: {cliente.DataNascimento.ToShortDateString()}, CPF: {cliente.CPF}, Estado Civil: {cliente.EstadoCivil}, Profissão: {cliente.Profissao}");
        }
    }

    static void ImprimirAniversariantes(List<Pessoa> aniversariantes)
    {
        foreach (var pessoa in aniversariantes)
        {
            Console.WriteLine($"Nome: {pessoa.Nome}, Data de Nascimento: {pessoa.DataNascimento.ToShortDateString()}");
        }
    }
}