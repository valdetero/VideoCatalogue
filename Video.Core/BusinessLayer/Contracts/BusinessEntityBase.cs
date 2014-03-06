using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SQLite.Net.Attributes;

namespace Video.Core.BusinessLayer.Contracts
{
    public abstract class BusinessEntityBase : Interfaces.IBusinessEntity
    {
        [PrimaryKey]
        public int Id { get; set; }
    }
}
