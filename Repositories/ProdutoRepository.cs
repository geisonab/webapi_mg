using System.Collections.Generic;
using Dapper;
using Dapper.Contrib.Extensions;
using WebAPI_MG.Data;
using WebAPI_MG.Models;
using WebAPI_MG.Services;
using WebAPI_MG.ViewModels;

namespace WebAPI_MG.Repositories
{
    public class ProdutoRepository
    {
        private StoreDataContext _context;

        public ProdutoRepository(StoreDataContext context)
        {
            _context = context;
        }

        public IEnumerable<Produto> Get()
        {
            return _context.conexao.Query<Produto>(
                @"SELECT [CPD] 
                    ,[PRODUTO]
                    ,[REFERENCIA]
                    ,[DESCRICAO]
                    ,[CLASS]
                    ,[FAMILIA]
                    ,[PRECO]
                FROM [Web].[dbo].[tbSLN_Mobile_PesquisaPreco_Produtos]");
        }

        public PagedList<Produto> PagedGet(PageParameters pageParameters)
        {
            return PagedList<Produto>.ToPagedList(
            _context.conexao.Query<Produto>(
                @"SELECT [CPD] 
                    ,[PRODUTO]
                    ,[REFERENCIA]
                    ,[DESCRICAO]
                    ,[CLASS]
                    ,[FAMILIA]
                    ,[PRECO]
                FROM [Web].[dbo].[tbSLN_Mobile_PesquisaPreco_Produtos]")
                ,pageParameters.PageNumber,pageParameters.PageSize);
            //todo: fazer a paginação direto na consulta de banco(
            //Order by CPD
            //OFFSET (@Pagina - 1) * @QtdPorPagina ROWS
            //FETCH NEXT @QtdPorPagina ROWS ONLY;
            //)
        }

        public Produto Get(int id)
        {
            return _context.conexao.QueryFirstOrDefault<Produto>(
                @"SELECT [CPD] 
                    ,[PRODUTO]
                    ,[REFERENCIA]
                    ,[DESCRICAO]
                    ,[CLASS]
                    ,[FAMILIA]
                    ,[PRECO]
                FROM [Web].[dbo].[tbSLN_Mobile_PesquisaPreco_Produtos]
                    WHERE [CPD] = @ID",
                new { ID = id });
        }

        public ResultViewModel Insert(Produto produto)
        {
            ViewModelProdutosEditor produtoeditor = new ViewModelProdutosEditor();
            produtoeditor.CPD = produto.CPD;
            produtoeditor.PRODUTO = produto.PRODUTO;
            produtoeditor.REFERENCIA = produto.REFERENCIA;
            produtoeditor.DESCRICAO = produto.DESCRICAO;
            produtoeditor.CLASS = produto.CLASS;
            produtoeditor.FAMILIA = produto.FAMILIA;
            produtoeditor.PRECO = produto.PRECO;
            
            produtoeditor.Validate();
            if(produtoeditor.Invalid)
                return new ResultViewModel(){
                    Success = false,
                    Message = "Não foi possível cadastrar o produto",
                    Data = produtoeditor.Notifications
                };

            _context.conexao.Insert<Produto>(produto);
            return new ResultViewModel{
                Success = true,
                Message = "Produto cadastrado com sucesso.",
                Data = produto
            };
        }

        public ResultViewModel Update(Produto produto)
        {
            ViewModelProdutosEditor produtoEditor = new ViewModelProdutosEditor();
            produtoEditor.CPD = produto.CPD;
            produtoEditor.PRODUTO = produto.PRODUTO;
            produtoEditor.REFERENCIA = produto.REFERENCIA;
            produtoEditor.DESCRICAO = produto.DESCRICAO;
            produtoEditor.CLASS = produto.CLASS;
            produtoEditor.FAMILIA = produto.FAMILIA;
            produtoEditor.PRECO = produto.PRECO;

            produtoEditor.Validate();
            if(produtoEditor.Invalid)
                return new ResultViewModel(){
                    Success = false,
                    Message = "Não foi possível cadastrar o produto",
                    Data = produtoEditor.Notifications
                };  

            _context.conexao.Update<Produto>(produto);
            return new ResultViewModel{
                Success = true,
                Message = "Produto alterado com sucesso.",
                Data = produto
            };
        }

        public ResultViewModel Delete(Produto produto)
        {
            ViewModelProdutosEditor produtoeditor = new ViewModelProdutosEditor();
            produtoeditor.CPD = produto.CPD;
            produtoeditor.PRODUTO = produto.PRODUTO;
            produtoeditor.REFERENCIA = produto.REFERENCIA;
            produtoeditor.DESCRICAO = produto.DESCRICAO;
            produtoeditor.CLASS = produto.CLASS;
            produtoeditor.FAMILIA = produto.FAMILIA;
            produtoeditor.PRECO = produto.PRECO;

            produtoeditor.Validate();
            if(produtoeditor.Invalid)
                return new ResultViewModel(){
                    Success = false,
                    Message = "Não foi possível cadastrar o produto",
                    Data = produtoeditor.Notifications
                }; 

            _context.conexao.Delete<Produto>(produto);
            return new ResultViewModel{
                Success = true,
                Message = "Produto excluído com sucesso."
            };
        }
    }
}