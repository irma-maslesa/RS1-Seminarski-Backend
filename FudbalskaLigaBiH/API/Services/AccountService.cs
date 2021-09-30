using AutoMapper;
using Data;
using Model = Data.Model;
using Entity = Data.EntityModel;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using FudbalskaLigaBiH.API.Filter;
using System.Collections.Generic;
using FudbalskaLigaBiH.API.JwtFeatures;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace API.Services
{
    public class AccountService : ReadService<Model.KorisnikResponse, Entity.Korisnik, object>, IAccountService
    {
        private readonly UserManager<Entity.Korisnik> userManager;
        private readonly SignInManager<Entity.Korisnik> signInManager;
        public readonly JwtHandler jwtHandler;

        public AccountService(ApplicationDbContext context, IMapper mapper, UserManager<Entity.Korisnik> userManager, SignInManager<Entity.Korisnik> signInManager, JwtHandler jwtHandler) : base(context, mapper)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.jwtHandler = jwtHandler;
        }

        public async Task<Model.KorisnikResponse> login(Model.KorisnikLoginRequest korisnik)
        {
            var user = await userManager.FindByEmailAsync(korisnik.Email);

            if (user != null && !user.EmailConfirmed && (await userManager.CheckPasswordAsync(user, korisnik.Password)))
            {
                throw new UserException("Email nije potvrđen.");
            }

            var result = await signInManager.PasswordSignInAsync(korisnik.Email, korisnik.Password, korisnik.RememberMe, false);

            if (result.Succeeded)
            {
                IList<string> roles = await userManager.GetRolesAsync(user).ConfigureAwait(false);
                var signingCredentials = jwtHandler.GetSigningCredentials();
                var claims = jwtHandler.GetClaims(user);
                var tokenOptions = jwtHandler.GenerateTokenOptions(signingCredentials, claims);
                var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

                Model.KorisnikResponse response = mapper.Map<Model.KorisnikResponse>(user);
                response.Uloga = roles[0];
                response.Token = token;
                response.ZapamtiMe = korisnik.RememberMe;

                return response;
            }

            throw new UserException("Neispravni podaci");
        }

        public async Task<string> logout()
        {
            await signInManager.SignOutAsync();
            return null;
        }
        public List<Model.UtakmicaResponse> getOmiljeneUtakmice(string id)
        {
            List<Entity.Utakmica> omiljeneUtakmice = context.KorisnikUtakmica
                .Where(e => e.Korisnik.Id == id)
                .Include(e => e.Utakmica.Liga).Include(e => e.Utakmica.KlubDomacin).Include(e => e.Utakmica.KlubGost)
                .Select(e => e.Utakmica)
                .OrderByDescending(e => e.DatumOdrzavanja).ToList();

            return mapper.Map<List<Model.UtakmicaResponse>>(omiljeneUtakmice);
        }

        public List<Model.UtakmicaResponse> setOmiljenaUtakmica(Model.OmiljenaUtakmicaRequest request)
        {
            Entity.KorisnikUtakmica entity = mapper.Map<Entity.KorisnikUtakmica>(request);
            context.KorisnikUtakmica.Add(entity);
            context.SaveChanges();

            List<Entity.Utakmica> omiljeneUtakmice = context.KorisnikUtakmica
                .Where(e => e.Korisnik.Id == request.KorisnikId)
                .Include(e => e.Utakmica.Liga).Include(e => e.Utakmica.KlubDomacin).Include(e => e.Utakmica.KlubGost)
                .Select(e => e.Utakmica)
                .ToList();

            return mapper.Map<List<Model.UtakmicaResponse>>(omiljeneUtakmice);
        }

        public List<Model.UtakmicaResponse> removeOmiljenaUtakmica(Model.OmiljenaUtakmicaRequest request)
        {
            Entity.KorisnikUtakmica entity = context.KorisnikUtakmica.Where(e => e.Korisnik.Id == request.KorisnikId && e.UtakmicaID == request.UtakmicaID).FirstOrDefault();
            context.Remove(entity);
            context.SaveChanges();

            List<Entity.Utakmica> omiljeneUtakmice = context.KorisnikUtakmica
                .Where(e => e.Korisnik.Id == request.KorisnikId)
                .Include(e => e.Utakmica.Liga).Include(e => e.Utakmica.KlubDomacin).Include(e => e.Utakmica.KlubGost)
                .Select(e => e.Utakmica)
                .ToList();

            return mapper.Map<List<Model.UtakmicaResponse>>(omiljeneUtakmice);
        }

    }
}
