using System.Threading.Tasks;
using Dapper;
using WebAPI_MG.Data;
using WebAPI_MG.Models;

namespace WebAPI_MG.Repositories
{
    public class UserRepository
    {
        private StoreDataContext _context;

        public UserRepository(StoreDataContext context)
        {
            _context = context;
        }

        public async Task<User> GetUser(string username, string password)
        {
            var parameters = new DynamicParameters(); 
                parameters.Add("USER",username); 
                parameters.Add("PASS",password);
            
            return await _context.conexao.QueryFirstOrDefaultAsync<User>(
                @"SELECT [ID] 
                    ,[USERNAME]
                    ,[PASSWORD]
                    ,[ROLE]
                FROM [Web].[dbo].[tbWEB_WEBAPIMG_USER]
                WHERE [USERNAME] = @USER AND [PASSWORD] = @PASS",parameters);
        }
    }
}