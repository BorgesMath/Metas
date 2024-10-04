using Metas.API.Response;
using Metas.shared.Dados.Banco;
using Metas.Shared.Modelos.Modelos;
using Microsoft.AspNetCore.Mvc;
using Metas.API.Requests;
using Microsoft.AspNetCore.Builder;

namespace Metas.API.EndPoints;

public static class MetaExtensions
{

    public static void AddEndPointsMetas(this
        WebApplication app)
    {

     var groupBuilder = app.MapGroup("meta")
    .RequireAuthorization()
    .WithTags("METAS");


        groupBuilder.MapGet("", ([FromServices] DAL<Meta> dal) =>
        {

            var listaDeMetas = dal.Listar();

            if (listaDeMetas is null)
            {
                return Results.NotFound();
            }
            var listaDeMetasResponse = EntityListToResponseList(listaDeMetas);


            return Results.Ok(listaDeMetasResponse);
      

        });

        groupBuilder.MapPost("", ([FromServices] DAL  <Meta> dal, [FromBody] MetaRequest request) =>
        {

            Meta meta1 = new(request.Name)
            {
                Continuo = request.Continuo,
                Tempo = request.Tempo,
                Descricao = request.Descricao,

            };


            Task task = dal.AdicionarAsync(meta1);
            return Results.Ok();
        });

        groupBuilder.MapPatch("finish/{id}", async ([FromServices] DAL<Meta> dal, int id) =>
        {


            var meta = await dal.RecuperarPorAsync(a => a.Id.Equals(id));

            if (meta == null) return Results.NotFound(); 

            if (meta.Status) return Results.BadRequest("Meta já está finalizada.");

            meta.Status = true;

            await dal.AtualizarAsync(meta);

            return Results.Ok();
        });

        groupBuilder.MapDelete("{id}", async ([FromServices] DAL<Meta> dal, int id) =>
        {
           
            var meta = await dal.RecuperarPorAsync(a => a.Id.Equals(id));

            if (meta == null) return Results.NotFound("Meta não encontrada.");

            await dal.DeletarAsync(meta);

            return Results.Ok("Meta deletada com sucesso.");
        });

        groupBuilder.MapPut("{id}", async ([FromServices] DAL<Meta> dal, [FromBody] MetaRequestEdit requestEdit, int id) =>
        {
            var meta = await dal.RecuperarPorAsync(a => a.Id == id);

            if (meta == null)
            {
                return Results.NotFound("Meta não encontrada.");
            }

            if (requestEdit.Name is not null)
            {
                meta.Nome = requestEdit.Name;
            }

            if (requestEdit.Continuo.HasValue)
            {
                meta.Continuo = requestEdit.Continuo.Value;
            }

            if (requestEdit.Tempo.HasValue)
            {
                meta.Tempo = requestEdit.Tempo.Value;
            }

            if (requestEdit.Descricao is not null)
            {
                meta.Descricao = requestEdit.Descricao;
            }

            await dal.AtualizarAsync(meta);

            return Results.Ok("Meta atualizada com sucesso.");
        });


    }

    #region FUNCOES AUXILIARES

    private static List<MetaResponse> EntityListToResponseList(IEnumerable<Meta> listaDeMeta)
    {
        return listaDeMeta.Select(a => EntityToResponse(a)).ToList();
    }

    private static MetaResponse EntityToResponse(Meta meta)
    {

      var passoResponses = meta.Passos.Select(p => new PassoResponse(
      p.Nome,
      p.Tempo,
      p.Status,
      p.Descricao
        )).ToList();

        return new MetaResponse(meta.Nome, meta.Tempo, meta.Descricao, meta.Status, passoResponses);
    }


    #endregion




}