using System;
using clase_25_4.Data;

namespace clase_25_4.Services
{
	public interface IEntityService<T>
	{
        List<T> GetAll();
		T GetById(int id);
    }
	
}

