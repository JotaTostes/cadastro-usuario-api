using CadastroUsuario.Repository.Context;
using CadastroUsuario.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace CadastroUsuario.Repository.Repositories
{
    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        protected CadastroUsuarioContext db;

        public BaseRepository(CadastroUsuarioContext context)
        {
            db = context;
        }

        /// <summary>
        /// Método que insere um registro.
        /// </summary>
        /// <param name="obj"></param>
        public TEntity Add(TEntity obj)
        {
            using (var db = new CadastroUsuarioContext())
            {
                db.Set<TEntity>().Add(obj);
                db.SaveChanges();
                return obj;
            }
        }

        /// <summary>
        /// Método que busca um objeto pelo seu Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public TEntity GetById(object id)
        {
            using (var db = new CadastroUsuarioContext())
            {
                db.Set<TEntity>().AsNoTracking();
                return db.Set<TEntity>().Find(id);
            }
        }

        /// <summary>
        /// Método que retorna uma lista com todos registros de uma tabela.
        /// </summary>
        /// <returns></returns>
        public List<TEntity> GetAll()
        {
            using (var db = new CadastroUsuarioContext())
            {
                return db.Set<TEntity>().AsNoTracking().ToList();
            }
        }

        /// <summary>
        /// Realiza a atualização de um registro.
        /// </summary>
        /// <param name="obj"></param>
        public void Update(TEntity obj)
        {
            using (var db = new CadastroUsuarioContext())
            {
                db.Entry(obj).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        /// <summary>
        /// Remove um registro do DB.
        /// </summary>
        /// <param name="obj"></param>
        public void Remove(TEntity obj)
        {
            using (var db = new CadastroUsuarioContext())
            {
                db.Set<TEntity>().Remove(obj);
                db.SaveChanges();
            }
        }

        /// <summary>
        /// Remove varios registros
        /// </summary>
        /// <param name="predicate"></param>
        public void Remove(Func<TEntity, bool> predicate)
        {
            using (var db = new CadastroUsuarioContext())
            {
                db.Set<TEntity>().Where(predicate).ToList()
                    .ForEach(del => db.Set<TEntity>().Remove(del));
                db.SaveChanges();
            }
        }

        /// <summary>
        /// Retorna uma lista de acordo com o predicado informado.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> predicate)
        {
            return db.Set<TEntity>().AsNoTracking().Where(predicate.Compile()).ToList();
        }

        /// <summary>
        /// Realiza a cadastro de um registro recebemdo uma conexão com uma trasação aberta.
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        public TEntity AddWithTransaction(TEntity obj, CadastroUsuarioContext db)
        {
            db.Set<TEntity>().Add(obj);
            return obj;
        }

        /// <summary>
        /// Realiza a atualização de um registro recebemdo uma conexão com uma trasação aberta.
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="db"></param>
        public void UpdateWithTransaction(TEntity obj, CadastroUsuarioContext db)
        {
            db.Entry(obj).State = EntityState.Modified;
        }

        /// <summary>
        /// Commitar transação aberta.
        /// </summarydb
        /// <param name="db"></param>
        public void Commit(CadastroUsuarioContext db)
        {
            db.SaveChanges();
        }

        /// <summary>
        /// Cancela transação aberta.
        /// </summarydb
        /// <param name="db"></param>
        public void RollBack(CadastroUsuarioContext db)
        {
            db.Dispose();
            GC.SuppressFinalize(db);
        }
    }
}
