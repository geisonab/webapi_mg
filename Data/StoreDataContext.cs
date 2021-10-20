using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace WebAPI_MG.Data
{
    public class StoreDataContext
    {
        private IConfiguration _config;
        public SqlConnection conexao { get;private set; }

        public StoreDataContext(IConfiguration configuration)
        {
            _config = configuration;
            SetarConexao();
        }
        
        public void SetarConexao()
        {
            conexao = new SqlConnection(_config.GetConnectionString("BaseWeb"));
        }
    }
}
