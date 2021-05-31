using CadastroUsuario.Repository.Context;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace CadastroUsuario.Repository.Interfaces
{
    /// <summary>
    /// Interface do repositório base.
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// Método que insere um registro.
        /// </summary>
        /// <param name="obj"></param>
        TEntity Add(TEntity obj);

        /// <summary>
        /// Método que busca um objeto pelo seu Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        TEntity GetById(object id);

        /// <summary>
        /// Método que retorna uma lista com todos registros de uma tabela.
        /// </summary>
        /// <returns></returns>
        List<TEntity> GetAll();

        /// <summary>
        /// Realiza a atualização de um registro.
        /// </summary>
        /// <param name="obj"></param>
        void Update(TEntity obj);

        /// <summary>
        /// Remove um registro do DB.
        /// </summary>
        /// <param name="obj"></param>
        void Remove(TEntity obj);

        /// <summary>
        /// Remove varios registros
        /// </summary>
        /// <param name="predicate"></param>
        void Remove(Func<TEntity, bool> predicate);

        /// <summary>
        /// Retorna uma lista de acordo com o predicado informado.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// Realiza a cadastro de um registro recebemdo uma conexão com uma trasação aberta.
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        TEntity AddWithTransaction(TEntity obj, CadastroUsuarioContext db);

        /// <summary>
        /// Realiza a atualização de um registro recebemdo uma conexão com uma trasação aberta.
        /// </summary>
        /// <param name="obj"></param>
        void UpdateWithTransaction(TEntity obj, CadastroUsuarioContext db);

        /// <summary>
        /// Commitar transação aberta.
        /// </summarydb
        /// <param name="db"></param>
        void Commit(CadastroUsuarioContext db);

        /// <summary>
        /// Cancela transação aberta.
        /// </summarydb
        /// <param name="db"></param>
        void RollBack(CadastroUsuarioContext db);
    }
}
