namespace DAL
{
    /// <summary>
    /// helper interface 
    /// </summary>
    /// <typeparam name="T"> model object instance </typeparam>
    public  interface IEntityRep<T>
         where T : class
    {   
        /// <summary>
        /// get row by id
        /// </summary>
        /// <param name="id">id of object</param>
        /// <returns> model object</returns>
        T GetById(int id);
    }
}
