using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metas.Shared.Modelos.Modelos;

public class Meta(string nome)
{
    public int Id { get; set; }

    public virtual ICollection<Passos> Passos { get; set; } = [];

    public bool Status { get; set; } = false;  

    public required string Nome { get; set; } = nome;  

    public int? Tempo { get; set; }  

    public bool Continuo { get; set; }  

    public string? Descricao { get; set; }  
}
