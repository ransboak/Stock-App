using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ApplicationDBContext _context;
        public CommentRepository(ApplicationDBContext context)
        {
            _context = context;
        }
        public Task<Comment> CreateAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Comment?> DeleteAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<List<Comment>> GetAllAsync()
        {
            return await _context.Comments.ToListAsync();
        }

        public Task<Comment?> GetByIdAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Comment?> UpdateAsync()
        {
            throw new NotImplementedException();
        }
    }
}