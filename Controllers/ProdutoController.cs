using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using WebAPI_MG.Models;
using WebAPI_MG.Repositories;
using WebAPI_MG.Services;
using WebAPI_MG.ViewModels;

namespace WebAPI_MG.Controllers
{
    public class ProdutoController : Controller
    {
        private ProdutoRepository _repository;

        public ProdutoController(ProdutoRepository repository)
        {
            _repository = repository;
        }

        [Route("V1/Produtos")]
        [HttpGet]
        [ResponseCache(Duration = 60)] //duração em minutos
        public IEnumerable<Produto> Get()
        {
            return _repository.Get();
        }

        [Route("V1/PagedProdutos")]
        [HttpGet]
        public IEnumerable<Produto> PagedGet([FromQuery] PageParameters pageParameters)
        {
            var pagedProduto = _repository.PagedGet(pageParameters); ;
            var metadata = new
            {
                pagedProduto.TotalCount,
                pagedProduto.PageSize,
                pagedProduto.CurrentPage,
                pagedProduto.TotalPages,
                pagedProduto.HasNext,
                pagedProduto.HasPrevious
            };

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

            return pagedProduto;
        }

        [Route("V1/Produtos/{id}")]
        [HttpGet]
        public Produto Get(int id)
        {
            return _repository.Get(id);
        }

        [Route("V1/Produtos")]
        [HttpPost]
        public ResultViewModel Post([FromBody] Produto produto)
        {
            return _repository.Insert(produto);
        }

        [Route("V1/Produtos")]
        [HttpPut]
        public ResultViewModel Put([FromBody] Produto produto)
        {
            return _repository.Update(produto);
        }

        [Route("V1/Produtos")]
        [HttpDelete]
        public ResultViewModel Delete([FromBody] Produto produto)
        {
            return _repository.Delete(produto);
        }
    }
}