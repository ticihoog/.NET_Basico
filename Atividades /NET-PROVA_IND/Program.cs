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
clientes.Add(new Cliente("Isabela Chaves", new DataTime(1986, 2 ,4), "0151871507", "casada" , "medica" ));
}
