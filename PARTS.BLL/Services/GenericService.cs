using AutoMapper;
using PARTS.BLL.Services.Interaces;
using PARTS.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PARTS.BLL.Services
{
    namespace ClientPartBLL.Services
    {
        public class GenericService<TEntity, TRequest, TResponse> : IGenericService<TEntity, TRequest, TResponse>
            where TEntity : class
            where TRequest : class
            where TResponse : class
        {
            private readonly IGenericRepository<TEntity> _repository;
            private readonly IMapper _mapper;

            public GenericService(IGenericRepository<TEntity> repository, IMapper mapper)
            {
                _repository = repository;
                _mapper = mapper;
            }

            public virtual async Task<IEnumerable<TResponse>> GetAllAsync()
            {
                try
                {
                    var entities = await _repository.GetAsync();
                    return _mapper.Map<IEnumerable<TEntity>, IEnumerable<TResponse>>(entities);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            public virtual async Task<TResponse> GetByIdAsync(Guid id)
            {
                try
                {
                    var entity = await _repository.GetByIdAsync(id);
                    return _mapper.Map<TEntity, TResponse>(entity);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            public virtual async Task PostAsync(TRequest request)
            {
                try
                {
                    var entity = _mapper.Map<TRequest, TEntity>(request);
                    await _repository.InsertAsync(entity);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            public virtual async Task UpdateAsync(TRequest request)
            {
                try
                {
                    var entity = _mapper.Map<TRequest, TEntity>(request);
                    await _repository.UpdateAsync(entity);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            public virtual async Task DeleteByIdAsync(Guid id)
            {
                try
                {
                    await _repository.DeleteAsync(id);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
    }
}
