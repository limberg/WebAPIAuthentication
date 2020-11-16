using System.Threading.Tasks;
using WpfAppAuthentication.Models;

namespace WpfAppAuthentication.APIServices
{
    public interface IAPIHelper
    {
        Task<Token> Authentication(string username, string password);
    }
}