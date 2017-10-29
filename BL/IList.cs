using System.Collections.Generic;

namespace BL
{
    /// <summary>
    /// items search interface 
    /// </summary>
    /// <typeparam name="T">instance ef-class</typeparam>
    public interface IList<T> where T : class
    {
        /// <summary>
        /// items collection
        /// </summary>
        /// <param name="m">serching parametr</param>
        /// <returns>collection ef-objects</returns>
        IEnumerable<T> GetItems(SearchCarsModel m); 
    }
}
