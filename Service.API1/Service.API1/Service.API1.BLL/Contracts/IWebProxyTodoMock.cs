using Service.API1.BLL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.API1.BLL.Contracts
    {
    public interface ITodosMockProxyService
        {
        Task<IEnumerable<Todo>> GetTodos();
        Task<Todo> GetTodoById(int id);
        }
    }
