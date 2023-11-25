using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMAPI
{
    /// <summary>
    /// Attribute that tells PMAPI mod id for use in extended IDs (EID)
    /// </summary>
    [AttributeUsage(AttributeTargets.Assembly, Inherited = false, AllowMultiple = false)]
    public sealed class PMAPIModAttribute : Attribute
    {
        public readonly string id;

        /// <summary>
        /// Attribute that tells PMAPI mod id for use in extended IDs (EID)
        /// </summary>
        /// <param name="id">Mod id</param>
        public PMAPIModAttribute(string id)
        {
            this.id = id;
        }
    }
}
