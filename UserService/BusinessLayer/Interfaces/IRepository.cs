using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    interface IRepository<T, Y>
    {
        /// <summary>
        /// add something to the database
        /// </summary>
        /// <param name="obj"> the object that you want to add to the database</param>
        /// <returns></returns>
        public Task<T> Add(T obj);

        /// <summary>
        /// get a single object from the database 
        /// </summary>
        /// <param name="obj"> the thing you will use to retrive the object from the database Y indicates it can be whatever type you need</param>
        /// <returns></returns>
        public Task<T> Read(Y obj);

        /// <summary>
        /// read all items from a table and return a list of those items
        /// </summary>
        /// <returns></returns>
        public Task<List<T>> Read();
    }
}
