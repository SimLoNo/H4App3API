using H4App3API.Database;
using H4App3API.Models;
using Microsoft.EntityFrameworkCore;

namespace H4App3API.Repositories
{
    public interface IColumnRepository
    {
        Task<List<Column>> GetAllColumns();
        Task<Column> CreateColumn(Column column);
        Task<Column> UpdateColumn(int columnId, Column column);
        Task<Column> DeleteColumn(int columnId);
    }
    public class ColumnRepository : IColumnRepository
    {
        private readonly ScrumContext _context;
        public ColumnRepository(ScrumContext context)
        {
            _context = context;
        }

        public async Task<Column> CreateColumn(Column column)
        {
            _context.ColumnTable.Add(column);
            await _context.SaveChangesAsync();
            return column;
        }

        public async Task<Column> DeleteColumn(int columnId)
        {
            Column deleteColumn = await _context.ColumnTable.FirstOrDefaultAsync(c => c.ColumnId== columnId);
            if (deleteColumn is not null) 
            {
                _context.ColumnTable.Remove(deleteColumn);
                await _context.SaveChangesAsync();
                return deleteColumn;
            }
            return null;
        }

        public async Task<List<Column>> GetAllColumns()
        {
            return await _context.ColumnTable.ToListAsync();
        }

        public async Task<Column> UpdateColumn(int columnId, Column column)
        {
            Column updateColumn = await _context.ColumnTable.FirstOrDefaultAsync(c => c.ColumnId== columnId);
            if (updateColumn is not null) 
            {
                updateColumn.ColumnName = column.ColumnName;
                await _context.SaveChangesAsync();
                return updateColumn;
            }
            return null;
        }
    }
}
