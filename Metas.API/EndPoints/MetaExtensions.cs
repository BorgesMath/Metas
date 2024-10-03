using Metas.API.Response;
using Metas.shared.Dados.Banco;
using Metas.Shared.Modelos.Modelos;
using Microsoft.AspNetCore.Mvc;
using Metas.API.Requests;

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