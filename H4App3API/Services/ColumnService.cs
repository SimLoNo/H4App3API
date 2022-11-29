using H4App3API.Database.DTO;
using H4App3API.Models;
using H4App3API.Repositories;

namespace H4App3API.Services
{
    public interface IColumnService
    {
        Task<List<Column>> GetAllColumns();
        Task<Column> CreateColumn(ColumnRequest column);
        Task<Column> UpdateColumn(int columnId, ColumnRequest column);
        Task<Column> DeleteColumn(int columnId);

    }
    public class ColumnService : IColumnService
    {
        private readonly IColumnRepository _repository;

        public ColumnService(IColumnRepository repository)
        {
            _repository = repository;
        }

        public async Task<Column> CreateColumn(ColumnRequest column)
        {
            Column newColumn= MapColumnRequestToColumn(column);
            return await _repository.CreateColumn(newColumn);
        }

        public async Task<Column> DeleteColumn(int columnId)
        {
            return await _repository.DeleteColumn(columnId);
        }

        public async Task<List<Column>> GetAllColumns()
        {
            return await _repository.GetAllColumns();
        }

        public async Task<Column> UpdateColumn(int columnId, ColumnRequest column)
        {
            Column updateColumn = MapColumnRequestToColumn(column);
            return await _repository.UpdateColumn(columnId, updateColumn);
        }

        private Column MapColumnRequestToColumn(ColumnRequest columnRequest)
        {
            return new()
            {
                ColumnName = columnRequest.ColumnName,
            };
        }
    }
}
