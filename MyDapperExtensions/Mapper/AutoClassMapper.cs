using System;

namespace DapperExtensions.Mapper
{
    /// <summary>
    /// Automatically maps an entity to a table using a combination of reflection and naming conventions for keys.
    /// </summary>
    public class AutoClassMapper<T> : ClassMapper<T>
    {
        public AutoClassMapper()
        {
            Table(typeof(T).Name);
            AutoMap();
        }
    }
}