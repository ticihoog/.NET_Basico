using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static List<Tarefa> tarefas = new List<Tarefa>();

    static void Main()
    {
        while (true)
        {
            MostrarMenu();

            string opcao = Console.ReadLine();

            switch (opcao)
            {
                case "1":
                    CriarTarefa();
                    break;
                case "2":
                    ListarTodasTarefas();
                    break;
                case "3":
                    MarcarComoConcluida();
                    break;
                case "4":
                    ListarTarefasPendentes();
                    break;
                case "5":
                    ListarTarefasConcluidas();
                    break;
                case "6":
                    ExcluirTarefa();
                    break;
                case "7":
                    PesquisarTarefasPorPalavraChave();
                    break;
                case "8":
                    ExibirEstatisticas();
                    break;
                case "9":
                    // Opção para sair do programa
                    return;
                default:
                    Console.WriteLine("Opção inválida. Tente novamente.");
                    break;
            }
        }
    }

    static void MostrarMenu()
    {
        Console.WriteLine("\n------ Menu ------");
        Console.WriteLine("1. Criar Tarefa");
        Console.WriteLine("2. Listar Todas as Tarefas");
        Console.WriteLine("3. Marcar Tarefa como Concluída");
        Console.WriteLine("4. Listar Tarefas Pendentes");
        Console.WriteLine("5. Listar Tarefas Concluídas");
        Console.WriteLine("6. Excluir Tarefa");
        Console.WriteLine("7. Pesquisar Tarefas por Palavra-chave");
        Console.WriteLine("8. Exibir Estatísticas");
        Console.WriteLine("9. Sair");
        Console.Write("Escolha uma opção: ");
    }

    static void CriarTarefa()
    {
        Console.Write("Digite o título da tarefa: ");
        string titulo = Console.ReadLine();

        Console.Write("Digite a descrição da tarefa: ");
        string descricao = Console.ReadLine();

        Console.Write("Digite a data de vencimento (formato: dd/mm/aaaa): ");
        if (DateTime.TryParse(Console.ReadLine(), out DateTime dataVencimento))
        {
            Tarefa novaTarefa = new Tarefa(titulo, descricao, dataVencimento);
            tarefas.Add(novaTarefa);

            Console.WriteLine("Tarefa criada com sucesso!");
        }
        else
        {
            Console.WriteLine("Formato de data inválido. Tarefa não criada.");
        }
    }

    static void ListarTodasTarefas()
    {
        Console.WriteLine("\n------ Todas as Tarefas ------");
        foreach (var tarefa in tarefas)
        {
            Console.WriteLine(tarefa);
        }
    }

    static void MarcarComoConcluida()
    {
        Console.Write("Digite o título da tarefa que deseja marcar como concluída: ");
        string titulo = Console.ReadLine();

        Tarefa tarefa = tarefas.FirstOrDefault(t => t.Titulo.Equals(titulo, StringComparison.OrdinalIgnoreCase));

        if (tarefa != null)
        {
            tarefa.Concluir();
            Console.WriteLine("Tarefa marcada como concluída!");
        }
        else
        {
            Console.WriteLine("Tarefa não encontrada.");
        }
    }

    static void ListarTarefasPendentes()
    {
        var tarefasPendentes = tarefas.Where(t => !t.Concluida).ToList();

        Console.WriteLine("\n------ Tarefas Pendentes ------");
        foreach (var tarefa in tarefasPendentes)
        {
            Console.WriteLine(tarefa);
        }
    }

    static void ListarTarefasConcluidas()
    {
        var tarefasConcluidas = tarefas.Where(t => t.Concluida).ToList();

        Console.WriteLine("\n------ Tarefas Concluídas ------");
        foreach (var tarefa in tarefasConcluidas)
        {
            Console.WriteLine(tarefa);
        }
    }

    static void ExcluirTarefa()
    {
        Console.Write("Digite o título da tarefa que deseja excluir: ");
        string titulo = Console.ReadLine();

        Tarefa tarefa = tarefas.FirstOrDefault(t => t.Titulo.Equals(titulo, StringComparison.OrdinalIgnoreCase));

        if (tarefa != null)
        {
            tarefas.Remove(tarefa);
            Console.WriteLine("Tarefa excluída com sucesso!");
        }
        else
        {
            Console.WriteLine("Tarefa não encontrada.");
        }
    }

    static void PesquisarTarefasPorPalavraChave()
    {
        Console.Write("Digite a palavra-chave para a pesquisa: ");
        string palavraChave = Console.ReadLine();

        var tarefasEncontradas = tarefas
            .Where(t => t.Titulo.Contains(palavraChave, StringComparison.OrdinalIgnoreCase) ||
                        t.Descricao.Contains(palavraChave, StringComparison.OrdinalIgnoreCase))
            .ToList();

        Console.WriteLine("\n------ Tarefas Encontradas ------");
        foreach (var tarefa in tarefasEncontradas)
        {
            Console.WriteLine(tarefa);
        }
    }

    static void ExibirEstatisticas()
    {
        int tarefasConcluidas = tarefas.Count(t => t.Concluida);
        int tarefasPendentes = tarefas.Count(t => !t.Concluida);

        var tarefaMaisAntiga = tarefas.OrderBy(t => t.DataVencimento).FirstOrDefault();
        var tarefaMaisRecente = tarefas.OrderByDescending(t => t.DataVencimento).FirstOrDefault();

        Console.WriteLine("\n------ Estatísticas ------");
        Console.WriteLine($"Número de Tarefas Concluídas: {tarefasConcluidas}");
        Console.WriteLine($"Número de Tarefas Pendentes: {tarefasPendentes}");

        if (tarefaMaisAntiga != null)
        {
            Console.WriteLine($"Tarefa Mais Antiga: {tarefaMaisAntiga}");
        }

        if (tarefaMaisRecente != null)
        {
            Console.WriteLine($"Tarefa Mais Recente: {tarefaMaisRecente}");
        }
    }
}

class Tarefa
{
    public string Titulo { get; set; }
    public string Descricao { get; set; }
    public DateTime DataVencimento { get; set; }
    public bool Concluida { get; private set; }

    public Tarefa(string titulo, string descricao, DateTime dataVencimento)
    {
        Titulo = titulo;
        Descricao = descricao;
        DataVencimento = dataVencimento;
        Concluida = false;
    }

    public void Concluir()
    {
        Concluida = true;
    }

    public override string ToString()
    {
        string status = Concluida ? "Concluída" : "Pendente";
        return $"{Titulo} - {Descricao} - Data de Vencimento: {DataVencimento.ToShortDateString()} - Status: {status}";
    }
}
