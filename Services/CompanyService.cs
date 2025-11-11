using Microsoft.EntityFrameworkCore;
using WebCart.Data;
using WebCart.Models;

namespace WebCart.Services;

public class CompanyService
{
    private readonly AppDbContext _context;

    public CompanyService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Company?> GetCompany(int companyId)
    {
        return await _context.Companies.FindAsync(companyId);
    }

    public async Task<List<Company>> GetCompanies()
    {
        return await _context.Companies.ToListAsync();
    }

    public async Task<Company> AddCompany(Company company)
    {
        _context.Companies.Add(company);
        await _context.SaveChangesAsync();
        return company;
    }

    public async Task<Company> UpdateCompany(Company company)
    {
        _context.Companies.Update(company);
        await _context.SaveChangesAsync();
        return company;
    }
}