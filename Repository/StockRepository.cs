using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Stock;
using api.Helpers;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class StockRepository : IStockRepository
    {
        private readonly ApplicationDBContext _context;
        public StockRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<Stock> CreateAsync(Stock stockModel)
        {
            await _context.Stocks.AddAsync(stockModel);
            await _context.SaveChangesAsync();
            return stockModel;
        }

        public async Task<Stock?> DeleteAsync(int id)
        {
            var stockModel = await _context.Stocks.FirstOrDefaultAsync(x => x.Id == id);
            if(stockModel == null){
                return null;
            }

            _context.Stocks.Remove(stockModel);
            await _context.SaveChangesAsync();
            return stockModel;
        }

        public async Task<List<Stock>> GetAllAsync(QueryObject query)
        {
            var stocks = _context.Stocks.Include(c => c.Comments).AsQueryable();

            if(!string.IsNullOrWhiteSpace(query.CompanyName)){
                stocks = stocks.Where(s => s.CompanyName.Contains(query.CompanyName));
            }

            if(!string.IsNullOrWhiteSpace(query.Symbol)){
                stocks = stocks.Where(s => s.Symbol.Contains(query.Symbol));
            }

            return await stocks.ToListAsync();
        }

        public async Task<Stock?> GetByIdAsync(int id)
        {
            return await _context.Stocks.Include(c => c.Comments).FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<bool> StockExists(int id)
        {
            // var stock = await _context.Stocks.FirstOrDefaultAsync(x => x.Id == id);

            // if(stock == null){
            //     return false;
            // }

            // return true;
            return await _context.Stocks.AnyAsync(x => x.Id == id);
        }

        public async Task<Stock?> UpdateAsync(int id, UpdateStockRequestDto stockModel)
        {
            var existingStock = await _context.Stocks.FirstOrDefaultAsync(x => x.Id == id);

            if(existingStock == null){
                return null;
            }

            existingStock.Symbol = stockModel.Symbol;
            existingStock.CompanyName = stockModel.CompanyName;
            existingStock.Purchase = stockModel.Purchase;
            existingStock.LastDiv = stockModel.LastDiv;
            existingStock.Industry = stockModel.Industry;
            existingStock.MarketCap = stockModel.MarketCap;

            await _context.SaveChangesAsync();
            return existingStock;
        }
    }
}