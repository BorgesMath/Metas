using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metas.Shared.Modelos.Modelos;

public class Passos(string nome)
{
    public int Id { get; set; }  

    public int MetaID { get; set; }  

    public virtual Meta? Meta { get; set; }  

    public required string Nome { get; set; } = nome;  

    public bool Continuo { get; set; }  

    public int Tempo { get; set; }  

    public bool Status { get; set; }  

    public string? Descricao { get; set; }  
}
