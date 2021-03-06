﻿using Bookinator_Data.FileHelpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookinator_Data
{
	public interface IDataContextFactory
	{
		IDataContext<T> Get<T>() where T : Entity;
	}

	public interface IDataContext<T> where T : Entity
	{
		List<T> GetItems();
		void Remove(T item);
		void Add(T item);
		void Save();
	}

	public class JsonDataContextFactory : IDataContextFactory
	{
		public JsonDataContextFactory()
		{
		}
		public IDataContext<T> Get<T>() where T : Entity
		{
			return new JsonDataContext<T>();
		}
	}

	public class JsonDataContext<T> : IDataContext<T> where T : Entity
	{
		private string location;
		private string dbLocation;
		private List<T> items;
		private int NextID = 1;
		private IDirectoryHelper directoryHelper;

		public JsonDataContext()
		{
			this.directoryHelper = new DirectoryHelper();
			location = @"C:\Users\pgathany\Desktop\Personal\Json";
			if (!directoryHelper.Exists(location))
			{
				directoryHelper.CreateDirectory(location);
			}
			this.dbLocation = location + @"\" + typeof(T).Name + ".json";
			if (!File.Exists(dbLocation))
			{
				File.Create(dbLocation).Close();
			}

			using (StreamReader r = new StreamReader(dbLocation))
			{
				string json = r.ReadToEnd();
				items = JsonConvert.DeserializeObject<List<T>>(json);
			}
			if(items == null)
			{
				items = new List<T>();
			}
			if(items.Any())
			{
				NextID = items.Select(b => (b as Entity).ID).Max() + 1;
			}
		}

		public T GetByID(int ID)
		{
			return items.SingleOrDefault(i => i.ID == ID);
		}

		public List<T> GetItems()
		{
			return items;
		}

		public void Remove(T item)
		{
			items.Remove(item);
		}

		public void Add(T item)
		{
			item.ID = NextID++;
			items.Add(item);
		}
		
		public void Save()
		{
			var json = JsonConvert.SerializeObject(items.ToArray());
			System.IO.File.WriteAllText(dbLocation, json);
		}
	}
}
