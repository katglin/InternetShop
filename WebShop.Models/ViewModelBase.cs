using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShop.Models
{
    public abstract class ViewModelBase<TKey> 
    {
        /// <summary>
        /// Gets or sets unique id of View Model
        /// </summary>
        public TKey Id { get; set; }

        /// <summary>
        ///  Compares input value and existed object id
        /// </summary>
        /// <param name="key"></param>
        /// <returns>Value indicating whether keys are equal</returns>
        public virtual bool IsKeyEqualsTo(object key)
        {
            return Id == null && key == null ? true : Id.Equals(key);
        }

        /// <summary>
        /// Gets a value indicating whether object is new
        /// </summary>
        public virtual bool IsNew { get { return IsKeyEqualsTo(default(TKey)); } }
    }
}
