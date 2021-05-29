using System;
using System.Collections.Generic;
using System.Text;

namespace CadastroUsuario.Service.Interfaces
{
    /// <summary>
    /// Interface de serviços genéricos.
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IServiceBase<TEntity> where TEntity : class
    {
        /// <summary>
        /// Adiciona um novo registro.
        /// </summary>
        /// <param name="obj"></param>
        TEntity Add(TEntity obj);

        /// <summary>
        /// Retorna um registro específico.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        TEntity GetById(int id);

        /// <summary>
        /// Busca todos os registros.
        /// </summary>
        /// <returns></returns>
        IEnumerable<TEntity> GetAll();

        /// <summary>
        /// Atualiza um registro.
        /// </summary>
        /// <param name="obj"></param>
        void Update(TEntity obj);

        /// <summary>
        /// Remove um registro.
        /// </summary>
        /// <param name="obj"></param>
        void Remove(TEntity obj);

        /// <summary>
        /// Remove varios registros
        /// </summary>
        /// <param name="predicate"></param>
        void Remove(Func<TEntity, bool> predicate);

        /// <summary>
        /// Remove logicamente um registro.
        /// </summary>
        /// <param name="obj"></param>
        void RemoverLogico(TEntity obj);
    }
}
