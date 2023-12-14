using Core.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
	public class Product : IEntity, IEnumerable
	{
		public int ProductId { get; set; }
		public string ProductName { get; set; }
		public int CategoryID { get; set; }
		public string QuantityPerUnit { get; set; }
		public decimal UnitPrice { get; set; }
		public short UnitsInStock { get; set; }

		public IEnumerator GetEnumerator()
		{
			throw new NotImplementedException();
		}
	}
}