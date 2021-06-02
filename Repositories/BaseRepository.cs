using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Repositories;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using ThinkBridge.DTOs;

namespace ThinkBridge.Repository
{
    public static class EntityExtensions
    {
        public static IQueryable<T> IncludeMany<T>(this IQueryable<T> query, params string[] includes) where T : class
        {
            if (includes != null)
            {
                query = includes.Aggregate(query, (current, include) => current.Include(include));
            }
            return query;
        }
    }
    public class BaseRepository<TEntity> where TEntity : class
    {
        public IConfiguration Configuration { get; set; }
        private readonly Context _context;

        public BaseRepository(Context context)
        {
            _context = context;
            _context.ChangeTracker.AutoDetectChangesEnabled = false;
        }

        #region Add Methods
        public virtual Response<TEntity> Add(TEntity entityToAdd)
        {
            var result = new Response<TEntity>();
            try
            {
                _context.Add(entityToAdd);
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                result.Message = $"Failed to add record in database due to error: {e.Message}";
                return result;
            }

            result.Success = true;
            result.Data = entityToAdd;
            return result;
        }

        public virtual Response<List<TEntity>> Add(List<TEntity> entityToAdd)
        {
            var result = new Response<List<TEntity>>();
            try
            {
                foreach (var entity in entityToAdd)
                {
                    _context.Add(entity);
                }
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                result.Message = $"Failed to add records in database due to error: {e.Message}";
                return result;
            }
            result.Success = true;
            result.Data = entityToAdd;
            return result;
        }
        #endregion

        #region Get Methods
        public virtual Response<List<TEntity>> GetAll()
        {
            var result = new Response<List<TEntity>>();

            try
            {
                result.Data = _context.Set<TEntity>().ToList();
            }
            catch (Exception e)
            {
                result.Message = $"Failed to get records from database due to error: {e.Message}";
                return result;
            }

            result.Success = true;
            return result;
        }

        public virtual Response<TEntity> Get(Expression<Func<TEntity, bool>> predicate)
        {
            var result = new Response<TEntity>();
            try
            {
                result.Data = _context.Set<TEntity>().FirstOrDefault(predicate);
            }
            catch (Exception e)
            {
                result.Message = $"Failed to get record from database due to error: {e.Message}";
                return result;
            }

            result.Success = true;
            return result;
        }

        public virtual Response<List<TEntity>> GetAll(Expression<Func<TEntity, bool>> predicate)
        {
            var result = new Response<List<TEntity>>();
            try
            {
                result.Data = _context.Set<TEntity>().Where(predicate).ToList();
            }
            catch (Exception e)
            {
                result.Message = $"Failed to get records from database due to error: {e.Message}";
                return result;
            }

            result.Success = true;
            return result;
        }

        public virtual Response<List<TEntity2>> GetAll<TEntity2>(Expression<Func<TEntity, TEntity2>> selectPredicate)
        {
            var result = new Response<List<TEntity2>>();
            try
            {
                result.Data = _context.Set<TEntity>().Select(selectPredicate).ToList();
            }
            catch (Exception e)
            {
                result.Message = $"Failed to get records from database due to error: {e.Message}";
                return result;
            }

            result.Success = true;
            return result;
        }

        public virtual Response<List<TEntity2>> GetAll<TEntity2>(Expression<Func<TEntity, bool>> wherePredicate, Expression<Func<TEntity, TEntity2>> selectPredicate, params string[] includes)
        {
            var result = new Response<List<TEntity2>>() { Success = false, Data = null };
            try
            {
                result.Data = _context.Set<TEntity>().IncludeMany(includes).Where(wherePredicate).Select(selectPredicate).ToList();
            }
            catch (Exception e)
            {
                result.Message = $"Failed to get records from database due to error: {e.Message}";
                return result;
            }

            result.Success = true;
            return result;
        }
        #endregion

        #region Update Methods
        public virtual Response<TEntity> Update(TEntity entityToUpdate)
        {
            var result = new Response<TEntity>();
            
            try
            {
                _context.Update(entityToUpdate);
                _context.SaveChanges();
                result.Data = entityToUpdate;
            }
            catch (Exception e)
            {
                result.Message = $"Failed to update record in database due to error: {e.Message}";
                return result;
            }

            result.Success = true;
            return result;
        }

        public virtual Response<List<TEntity>> Update(List<TEntity> entitiesToUpdate)
        {
            var result = new Response<List<TEntity>>();

            try
            {
                _context.UpdateRange(entitiesToUpdate);
                _context.SaveChanges();
                result.Data = entitiesToUpdate;
            }
            catch (Exception e)
            {
                result.Message = $"Failed to update record in database due to error: {e.Message}";
                return result;
            }

            result.Success = true;
            return result;
        }
        #endregion

        #region Delete Methods
        public virtual Response<TEntity> Delete(TEntity entity)
        {
            var result = new Response<TEntity>();
            
            try
            {
                _context.Set<TEntity>().Remove(entity);
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                result.Message = $"Failed to remove record from database due to error: {e.Message}";
                return result;
            }

            result.Success = true;
            return result;
        }

        public virtual Response<List<TEntity>> Delete(List<TEntity> entitiesToDelete)
        {
            var result = new Response<List<TEntity>>();

            try
            {
                _context.RemoveRange(entitiesToDelete);
                _context.SaveChanges();
                result.Data = entitiesToDelete;
            }
            catch (Exception e)
            {
                result.Message = $"Failed to remove records from database due to error: {e.Message}";
                return result;
            }

            result.Success = true;
            return result;
        }

        #endregion
    }
}
