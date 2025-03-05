using Microsoft.Data.SqlClient;

namespace Api.Repositories
{
    public interface IBaseRepository
        {
            SqlConnection GetConnection();
        }
    }


