using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metas.Shared.Modelos.Modelos;

public class Meta
{
    // Construtor
    public Meta(string nome)
    {
        this.Nome = nome;

    }

    public int Id { get; set; }

    public virtual ICollection<Passos> Passos { get; set; } = [];

    public bool Status { get; set; } = false;

    public string Nome { get; set; }

    public int? Tempo { get; set; }

    public bool Continuo { get; set; }

    public string? Descricao { get; set; }


}

