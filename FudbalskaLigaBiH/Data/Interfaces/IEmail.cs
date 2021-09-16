using System.Threading.Tasks;
using FudbalskaLigaBiH.Data.DTOs;

namespace Data.Interfaces
{
    public interface IEmail
    {
        Task Send(string emailAddress, string body, EmailOptionsDTO options);
    }
}
