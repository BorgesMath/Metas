using Metas.Shared.Dados.Banco;
using Metas.Shared.Modelos.Modelos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Metas.shared.Dados.Banco;

public class DAL<T>(MetasContext context) where T : class
{
    private readonly MetasContext _context = context;

    public IEnumerable<T>? Listar()
    {
        if (typeof(T) == typeof(Meta))
        {
            return _context.Set<Meta>().Include(m => m.Passos).AsEnumerable() as IEnumerable<T>;
        }
        return _context.Set<T>().AsEnumerable();
    }

    public async Task AdicionarAsync(T objeto)
    {
        _context.Set<T>().Add(objeto);
        await _context.SaveChangesAsync();
    }

    public async Task AtualizarAsync(T objeto)
    {
        _context.Set<T>().Update(objeto);
        await _context.SaveChangesAsync();
    }

    public async Task DeletarAsync(T objeto)
    {
        _context.Set<T>().Remove(objeto);
        await _context.SaveChangesAsync();
    }

    public async Task<T?> RecuperarPorAsync(Expression<Func<T, bool>> condicao)
    {
        return await _context.Set<T>().FirstOrDefaultAsync(condicao);
    }

    public IEnumerable<T> ListarPor(Expression<Func<T, bool>> condicao)
    {
        return _context.Set<T>().Where(condicao).AsEnumerable();
    }

    public IEnumerable<T> ListarPaginado(int pageNumber, int pageSize)
    {
        return _context.Set<T>()
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .AsEnumerable();
    }
}
