using Metas.API.Response;
using Metas.shared.Dados.Banco;
using Metas.Shared.Modelos.Modelos;
using Microsoft.AspNetCore.Mvc;

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

    }

    #region FUNCOES AUXILIARES

    private static List<MetaResponse> EntityListToResponseList(IEnumerable<Meta> listaDeMeta)
    {
        return listaDeMeta.Select(a => EntityToResponse(a)).ToList();
    }

    private static MetaResponse EntityToResponse(Meta meta)
    {
        return new MetaResponse(meta.Nome, meta.Tempo, meta.Descricao, meta.Status);
    }


    #endregion




}