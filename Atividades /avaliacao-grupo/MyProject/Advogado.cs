using System;

namespace Namespace
{
    public class Advogado : Pessoa
    {
        public string CNA { get; set; }

        public Advogado(string nome, DateTime dataNascimento, string cpf, string cna)
            : base(nome, dataNascimento, cpf)
        {
            CNA = cna;
        }

        public void IniciarCaso(CasoJuridico caso)
        {
            if (!caso.Advogados.Contains(this))
            {
                caso.Advogados.Add(this);
                Console.WriteLine($"Caso iniciado por {Nome}.");
            }
            else
            {
                Console.WriteLine($"Este advogado já está associado a este caso.");
            }
        }

        public void AtualizarCaso(CasoJuridico caso, string novoStatus, DateTime? novaDataConclusao = null)
        {
            if (caso.Advogados.Contains(this))
            {
                if (caso.Status == "Em aberto" && novoStatus != "Em aberto" && !novaDataConclusao.HasValue)
                {
                    Console.WriteLine("Para atualizar o status para 'Concluído' ou 'Arquivado', é necessário informar a data de conclusão.");
                    return;
                }

                caso.Status = novoStatus;

                if (novaDataConclusao.HasValue)
                {
                    if (novaDataConclusao.Value < caso.Abertura)
                    {
                        Console.WriteLine("A data de conclusão não pode ser anterior à data de abertura do caso.");
                        return;
                    }
                    caso.Encerramento = novaDataConclusao.Value;
                }

                Console.WriteLine($"Caso atualizado por {Nome}.");
            }
            else
            {
                Console.WriteLine("Este advogado não está associado a este caso.");
            }
        }
    }
}