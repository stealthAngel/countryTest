using Microsoft.EntityFrameworkCore;
using w4sd.Models;
using w4sd.Repository;

namespace w4sd.Repository
{
    public class AddressCountryRulesRepository : IAddressCountryRulesRepository
    {
        private readonly SuperContext _context;
        public AddressCountryRulesRepository(SuperContext context)
        {
            _context = context;
        }

        public AddressCountryRule? GetAddressCountryRuleByCountry(string country)
        {
            var x = _context.AdressCountryRules.Where(x => x.Country == country).FirstOrDefault();
            return x;
        }

        public async Task<AddressCountryRule> Create(AddressCountryRule entity)
        {
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<AddressCountryRule>> GetAll()
        {
            return await _context.AdressCountryRules.ToListAsync();
        }

        public Task<AddressCountryRule> Update(AddressCountryRule entity)
        {
            throw new NotImplementedException();
        }

        public Task<AddressCountryRule> GetById()
        {
            throw new NotImplementedException();
        }
    }
}
