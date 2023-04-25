
using System;
namespace clase_25_4.Models
{
	public class BaseEntity
	{
		public string Id { private set; get; } = Guid.NewGuid().ToString();

	}
}

