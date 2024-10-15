using Metas.API.Requests;
using Metas.API.Response;
using Metas.shared.Dados.Banco;
using Metas.Shared.Modelos.Modelos;
using Microsoft.AspNetCore.Mvc;

namespace Metas.API.EndPoints;

public static class PassoExtensions
{

    public static void AddEndPointsPassos(this
        WebApplication app)
    {

        var groupBuilder = app.MapGroup("passos")
       .RequireAuthorization()
       .WithTags("PASSOS");



        //groupBuilder.MapGet("", ([FromServices] DAL<Passos> dal) =>
        //{

        //    var listaDePassos = dal.Listar();

        //    if (listaDePassos is null)
        //    {
        //        return Results.NotFound();
        //    }
        //    var listaDePassosResponse = EntityListToResponseList(listaDePassos);


        //    return Results.Ok(listaDePassosResponse);


        //});

        groupBuilder.MapPut("{id}", async ([FromServices] DAL<Passos> dal, [FromBody] PassosRequestEdit requestEdit, int id) =>
        {
            var passo = await dal.RecuperarPorAsync(p => p.Id == id);

            if (passo == null)
            {
                return Results.NotFound("Passo não encontrado.");
            }

            if (requestEdit.Name is not null)
            {
                passo.Nome = requestEdit.Name;
            }

            if (requestEdit.Continuo.HasValue)
            {
                passo.Continuo = requestEdit.Continuo.Value;
            }

            if (requestEdit.Tempo.HasValue)
            {
                passo.Tempo = requestEdit.Tempo.Value;
            }

            if (requestEdit.Descricao is not null)
            {
                passo.Descricao = requestEdit.Descricao;
            }

            
            await dal.AtualizarAsync(passo);

           
            return Results.Ok("Passo atualizado com sucesso.");
        });

        groupBuilder.MapDelete("{id}", async ([FromServices] DAL<Passos> dal, int id) =>
        {
            var passo = await dal.RecuperarPorAsync(p => p.Id == id);

            if (passo == null)
            {
                return Results.NotFound("Passo não encontrado.");
            }

            await dal.DeletarAsync(passo);

            return Results.Ok("Passo deletado com sucesso.");
        });

        groupBuilder.MapPost("{metaID}", async ([FromServices] DAL<Passos> dalPasso, [FromServices] DAL<Meta> dalMeta, [FromBody] PassosRequest request, int metaID) =>
        {
            var meta = await dalMeta.RecuperarPorAsync(m => m.Id == metaID);

            if (meta == null)
            {
                return Results.NotFound("Meta associada ao Passo não foi encontrada.");
            }

            Passos novoPasso = new(request.Name)
            {
                Continuo = request.Continuo,
                Tempo = request.Tempo,
                Descricao = request.Descricao,
                MetaID = metaID 
            };

            await dalPasso.AdicionarAsync(novoPasso);

            return Results.Ok("Passo criado com sucesso.");
        });

        groupBuilder.MapPatch("check/{id}", async ([FromServices] DAL<Passos> dal, int id) =>
        {
            
            var passo = await dal.RecuperarPorAsync(p => p.Id == id);

            if (passo == null)
            {
                return Results.NotFound("Passo não encontrado.");
            }

            passo.Status = !passo.Status;

            await dal.AtualizarAsync(passo);

            return Results.Ok($"Status do passo atualizado para {(passo.Status ? "completo" : "incompleto")}.");
        });
    }

    #region FUNCOES AUXILIARES

    private static List<PassoResponse> EntityListToResponseList(IEnumerable<Passos> listaDepassos)
    {
        return listaDepassos.Select(a => EntityToResponse(a)).ToList();
    }

    private static PassoResponse EntityToResponse(Passos passo)
    {
        return new PassoResponse( passo.Nome, passo.Tempo, passo.Status, passo.Descricao);
    }


    #endregion

}
