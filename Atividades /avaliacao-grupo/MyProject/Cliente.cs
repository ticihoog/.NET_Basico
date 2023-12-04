using System;

namespace Namespace
{
    public class Cliente : Pessoa
    {
        public string EstadoCivil { get; set; }
        public string Profissao { get; set; }

        public Cliente(string nome, DateTime dataNascimento, string cpf, string estadoCivil, string profissao)
            : base(nome, dataNascimento, cpf)
        {
            EstadoCivil = estadoCivil;
            Profissao = profissao;
        }

        public void IniciarCaso(CasoJuridico caso)
        {
            if (caso.Cliente == null)
            {
                caso.Cliente = this;
                Console.WriteLine($"Caso iniciado para o cliente {Nome}.");
            }
            else
            {
                Console.WriteLine($"O cliente {Nome} já está associado a um caso.");
            }
        }
    }
}
