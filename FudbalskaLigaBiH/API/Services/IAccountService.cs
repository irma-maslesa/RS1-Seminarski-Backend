using System.Collections.Generic;
using Model = Data.Model;
using Entity = Data.EntityModel;
using System.Threading.Tasks;

namespace API.Services
{
    public interface IAccountService : IReadService<Model.KorisnikResponse, object>

    {
        public Task<Model.KorisnikResponse> login(Model.KorisnikLoginRequest korisnik);
        public Task<string> logout();
        public List<Model.UtakmicaResponse> getOmiljeneUtakmice(string id);

        public List<Model.UtakmicaResponse> setOmiljenaUtakmica(Model.OmiljenaUtakmicaRequest request);

        public List<Model.UtakmicaResponse> removeOmiljenaUtakmica(Model.OmiljenaUtakmicaRequest request);
    }
}
